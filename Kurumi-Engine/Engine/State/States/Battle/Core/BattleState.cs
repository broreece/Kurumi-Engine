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

    // Renderers.
    private BattleRenderer? _battleRenderer;
    private EnemyRenderer? _enemyRenderer;
    private PartyBattleRenderer? _partyBattleRenderer;

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
    }

    public void OnEnter()
    {
        CacheDependencies();
    }

    public void OnExit() {}

    public void Update(float deltaTime)
    {
        HandleInput();

        // Handle requested interactions.
        if (_stateContext.InputContextManager.GetGameplayContext()!.InteractRequested) 
        {
            
        }

        _battleRenderer!.Update();
        _enemyRenderer!.Update();
        _partyBattleRenderer!.Update();
        _gameContext.GameServices.RenderSystem.Render();
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
    }

    private void HandleInput() 
    {
        var inputState = _gameContext.GameServices.InputMapper.BuildState();
        _stateContext.InputContextManager.Update(inputState);
    }
}