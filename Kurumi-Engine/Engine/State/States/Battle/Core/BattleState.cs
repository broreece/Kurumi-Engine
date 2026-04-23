using Data.Runtime.Formations.Core;
using Data.Runtime.Party.Core;

using Engine.Context.Core;
using Engine.State.Base;
using Engine.State.States.Battle.Base;
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

    // Renderers.
    private BattleRenderer? _battleRenderer;
    private EnemyRenderer? _enemyRenderer;
    private PartyBattleRenderer? _partyBattleRenderer;

    // List of actions to be executed in order.
    private PriorityQueue<BattleAction, int> _actions = new(Comparer<int>.Create((x, y) => y.CompareTo(x)));

    private bool WonBattle => _formation.IsDefeated();

    private bool LostBattle => _party.GetLeadersHp() == 0;

    public BattleState(GameContext gameContext, StateContext stateContext, Party party, Formation formation) 
    {
        _gameContext = gameContext;
        _stateContext = stateContext;
        _party = party;
        _formation = formation;
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
    }

    private void CacheDependencies() 
    {
        var assetRegistry = _gameContext.GameData.AssetRegistry;
        var renderSystem = _gameContext.GameServices.RenderSystem;

        var battleRendererFactory = new BattleRendererFactory(assetRegistry, renderSystem);
        _battleRenderer = battleRendererFactory.Create();

        var enemyRendererFactory = new EnemyRendererFactory(assetRegistry, renderSystem);
        _enemyRenderer = enemyRendererFactory.Create();

        var partyBattleRendererFactory = new PartyBattleRendererFactory(assetRegistry, renderSystem);
        _partyBattleRenderer = partyBattleRendererFactory.Create(_party);
    }

    private void HandleInput() 
    {
        var inputState = _gameContext.GameServices.InputMapper.BuildState();
        _stateContext.InputContextManager.Update(inputState);
    }
}