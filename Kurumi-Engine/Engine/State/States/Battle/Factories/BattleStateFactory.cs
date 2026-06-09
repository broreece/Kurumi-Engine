// Data.
using Data.Runtime.Formations.Base;
using Data.Runtime.Parties.Core;

// Engine.
using Engine.Context.Core;

using Engine.State.Base;
using Engine.State.States.Battle.Core;
using Engine.State.States.Battle.Text.Factories;

// Game.
using Game.UI.Views.Factories;

namespace Engine.State.States.Battle.Factories;

public sealed class BattleStateFactory
{
    // Context.
    private readonly GameContext _gameContext;
    private readonly StateContext _stateContext;

    // Party.
    private readonly Party _party;

    // Battle factories.
    private readonly BattleTextFactory _battleTextFactory;
    private readonly BattleViewFactory _battleViewFactory;

    public BattleStateFactory(
        GameContext gameContext, 
        StateContext stateContext, 
        Party party, 
        BattleTextFactory battleTextFactory, 
        BattleViewFactory battleViewFactory
    )
    {
        _gameContext = gameContext;
        _stateContext = stateContext;
        _party = party;
        _battleTextFactory = battleTextFactory;
        _battleViewFactory = battleViewFactory;
    }

    public BattleState Create(BattleStartRequest battle)
    {
        return new BattleState(_gameContext, _stateContext, _party, _battleTextFactory, _battleViewFactory, battle);
    }
}