using Data.Runtime.Formations.Base;
using Data.Runtime.Formations.Core;
using Data.Runtime.Formations.Factories;
using Data.Runtime.Party.Core;

using Engine.Context.Core;
using Engine.Input.Context.Contexts;
using Engine.State.Base;
using Engine.State.States.Battle.Base;
using Engine.Systems.Camera;
using Engine.Systems.Rendering.Core;
using Engine.Systems.Rendering.Factories;
using Engine.UI.Elements;
using Engine.UI.Layout.Core;
using Engine.UI.Render;

using Game.Scripts.Context.Builder.Core;
using Game.Scripts.Context.Core;
using Game.UI.Views;

using Infrastructure.Rendering.Core;

namespace Engine.State.States.Battle.Core;

/// <summary>
/// Renders the enemy formation, parties and attacking animations. 
/// Contains input handler and systems relating to the battle state menus.
/// </summary>
public sealed class BattleState : IGameState, IBattleMenu 
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

    // Cached config.
    private readonly int _maxChoicesPerPage;

    // Current selection indexes.
    private int _currentCharacterIndex = 0;
    private int _currentSelectionIndex = 0;

    // Render system.
    private RenderSystem? _renderSystem;

    // Renderers.
    private BattleRenderer? _battleRenderer;
    private EnemyRenderer? _enemyRenderer;
    private PartyBattleRenderer? _partyBattleRenderer;

    // Camera.
    private Camera? _camera;

    // Script context.
    private ScriptContext? _battleScriptContext;

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

        var gameData = _gameContext.GameData;
        var database = gameData.GameDatabase;
        var formationRegistry = database.FormationRegistry;
        var saveData = gameContext.GameObjects.SaveData;

        var formationFactory = new FormationFactory(database.EnemyDefinitionRegistry, database.EntityDefinitionRegistry);
        var formationModel = saveData.Formations[battle.EnemyFormationId];
        var formationDefinition = formationRegistry.Get(battle.EnemyFormationId);

        _formation = formationFactory.Create(formationDefinition, formationModel);

        var battleWindowConfig = gameContext.GameData.ConfigProvider.BattleWindowConfig;
        _maxChoicesPerPage = battleWindowConfig.MaxChoicesPerPage;

        _view = new BattleView(
            gameData.AssetRegistry, 
            database.AbilityRegistry,
            database.AbilitySetRegistry,
            battleWindowConfig,
            party.Characters
        );
        _uiRoot = _view.UIElement;

        _uiRenderSystem = new UIRenderSystem(new UILayoutSystem());
    }

    public void OnEnter()
    {
        CacheDependencies();
        InitializeInput();
    }

    public void OnExit() {}

    public void Update(float deltaTime)
    {
        // Handle requested interactions.
        if (_stateContext.InputContextManager.GetGameplayContext()!.InteractRequested) 
        {
            _stateContext.InputContextManager.GetGameplayContext()!.InteractRequested = false;
            // TODO: Implement select here.
        }

        _battleRenderer!.Update(_camera!.View);
        _enemyRenderer!.Update(_camera.View);
        _partyBattleRenderer!.Update(_camera.View);

        // Update the UI then render the UI.
        _view.Update(_currentCharacterIndex, _currentSelectionIndex);
        _uiRenderSystem.Render(_uiRoot, _renderSystem!, _stateContext.GameWindow.Size);
    }

    public ScriptContext GetScriptContext() => _battleScriptContext!;

    public void MoveUp()
    {
        _currentSelectionIndex = _currentSelectionIndex == 0 ? _maxChoicesPerPage - 1: _currentSelectionIndex - 1;
    }

    public void MoveDown()
    {
        _currentSelectionIndex = _currentSelectionIndex == _maxChoicesPerPage - 1 ? 0 : _currentSelectionIndex + 1;
    }

    public void MoveRight()
    {
        // TODO: Implement here.
        throw new NotImplementedException();
    }

    public void MoveLeft()
    {
        // TODO: Implement here.
        throw new NotImplementedException();
    }

    public void Cancel()
    {
        // TODO: Implement here.
        throw new NotImplementedException();
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

        // Battle script context.
        var battleScriptContextBuilder = new BattleScriptContextBuilder(_gameContext, _stateContext);
        _battleScriptContext = battleScriptContextBuilder.BuildScriptContext();
    }

    private void InitializeInput()
    {
         _stateContext.InputContextManager.SetContext(new BattleInputContext(this));
    }
}