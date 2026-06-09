// Data.
using Data.Runtime.Formations.Base;
using Data.Runtime.Parties.Core;

// Engine.
using Engine.Context.Core;

using Engine.State.Base;
using Engine.State.States.Battle.Core;

namespace Engine.State.States.Battle.Factories;

public sealed class BattleStateFactory
{
    // Context.
    private readonly GameContext _gameContext;
    private readonly StateContext _stateContext;

    // Party.
    private readonly Party _party;

    public BattleStateFactory(GameContext gameContext, StateContext stateContext, Party party)
    {
        _gameContext = gameContext;
        _stateContext = stateContext;
        _party = party;
    }

    public BattleState Create(BattleStartRequest battle)
    {
        return new BattleState(_gameContext, _stateContext, _party, battle);
    }
}