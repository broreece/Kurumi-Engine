using Data.Runtime.Formations.Base;
using Data.Runtime.Formations.Core;
using Data.Runtime.Formations.Factories;
using Data.Runtime.Party.Core;

using Engine.Context.Core;
using Engine.State.Base;
using Engine.State.States.Battle.Base;
using Engine.Systems.Camera;
using Engine.Systems.Rendering.Core;
using Engine.Systems.Rendering.Factories;
using Engine.UI.Elements;
using Engine.UI.Layout.Core;
using Engine.UI.Render;

using Game.UI.Views;

using Infrastructure.Rendering.Core;

namespace Engine.State.States.Battle.Core;

/// <summary>
/// Renders the enemy formation, parties and attacking animations. 
/// Contains input handler and systems relating to the battle state menus.
/// </summary>
public sealed class BattleState : IGameState 
{
    // Context.
    private readonly GameContext _gameContext;
    private readonly StateContext _stateContext;

    // Party.
    private readonly Party _party;

    // Enemy formation.
    private readonly Formation _formation;

    // Battle variable.
    private readonly BattleStartRequest _battle;

    // UI elements.
    private readonly UIElement _uiRoot;
    private readonly BattleView _view;

    // UI renderer.
    private readonly UIRenderSystem _uiRenderSystem;

    // Render system.
    private RenderSystem? _renderSystem;

    // Renderers.
    private BattleRenderer? _battleRenderer;
    private EnemyRenderer? _enemyRenderer;
    private PartyBattleRenderer? _partyBattleRenderer;

    // Camera.
    private Camera? _camera;

    // List of actions to be executed in order.
    private PriorityQueue<BattleAction, int> _actions = new(Comparer<int>.Create((x, y) => y.CompareTo(x)));

    private bool WonBattle => _formation.IsDefeated();

    private bool LostBattle => _party.GetLeadersHp() == 0;

    public BattleState(GameContext gameContext, StateContext stateContext, Party party, BattleStartRequest battle) 
    {
        _gameContext = gameContext;
        _stateContext = stateContext;
        _party = party;
        _battle = battle;

        var database = gameContext.GameData.GameDatabase;
        var formationRegistry = database.FormationRegistry;
        var saveData = gameContext.GameObjects.SaveData;

        var formationFactory = new FormationFactory(database.EnemyDefinitionRegistry, database.EntityDefinitionRegistry);
        var formationModel = saveData.Formations[battle.EnemyFormationId];
        var formationDefinition = formationRegistry.Get(battle.EnemyFormationId);

        _formation = formationFactory.Create(formationDefinition, formationModel);

        var configProvider = _gameContext.GameData.ConfigProvider;
        _view = new BattleView(_gameContext.GameData.AssetRegistry, configProvider.BattleWindowConfig);
        _uiRoot = _view.Build(party.Characters.Length);

        _uiRenderSystem = new UIRenderSystem(new UILayoutSystem());
    }

    public void OnEnter()
    {
        CacheDependencies();
    }

    public void OnExit() {}

    public void Update(float deltaTime)
    {
        // Handle requested interactions.
        if (_stateContext.InputContextManager.GetGameplayContext()!.InteractRequested) 
        {
            _stateContext.InputContextManager.GetGameplayContext()!.InteractRequested = false;
        }

        _battleRenderer!.Update(_camera!.View);
        _enemyRenderer!.Update(_camera.View);
        _partyBattleRenderer!.Update(_camera.View);

        // Update the UI then render the UI.
        _view.Update(_party.Characters);
        _uiRenderSystem.Render(_uiRoot, _renderSystem!, _stateContext.GameWindow.Size);
    }

    private void CacheDependencies() 
    {
        var assetRegistry = _gameContext.GameData.AssetRegistry;
        var renderSystem = _gameContext.GameServices.RenderSystem;
        var configProvider = _gameContext.GameData.ConfigProvider;
        var gameWindow = _stateContext.GameWindow;
        var windowSize = gameWindow.Size;

        _camera = new Camera(windowSize.X, windowSize.Y);
        gameWindow.SetView(_camera.View);

        var battleRendererFactory = new BattleRendererFactory(
            assetRegistry, 
            renderSystem, 
            configProvider.BattleBackgroundSpriteConfig,
            windowSize);
        _battleRenderer = battleRendererFactory.Create(_battle.BattleBackgroundArtName);

        var enemyRendererFactory = new EnemyRendererFactory(
            assetRegistry, 
            renderSystem, 
            configProvider.EnemyBattleSpriteConfig);
        _enemyRenderer = enemyRendererFactory.Create(_formation);

        var partyBattleRendererFactory = new PartyBattleRendererFactory(
            assetRegistry, 
            renderSystem,
            configProvider.CharacterBattleSpriteConfig
        );
        _partyBattleRenderer = partyBattleRendererFactory.Create(_party);

        // Renderer.
        _renderSystem = _gameContext.GameServices.RenderSystem;
    }
}