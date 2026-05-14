using Data.Runtime.Formations.Core;
using Data.Runtime.Party.Core;

using Engine.Context.Core;
using Engine.State.Base;

using Game.Scripts.Context.Builder.Base;
using Game.Scripts.Context.Capabilities.Base;
using Game.Scripts.Context.Capabilities.Implementations.Battle;
using Game.Scripts.Context.Capabilities.Implementations.Entity;
using Game.Scripts.Context.Capabilities.Interfaces.Battle;
using Game.Scripts.Context.Capabilities.Interfaces.Entity;
using Game.Scripts.Context.Core;
using Game.Scripts.Context.Variables.Core;

namespace Game.Scripts.Context.Builder.Core;

public sealed class BattleScriptContextBuilder : IScriptContextBuilder
{
    // Contexts.
    private readonly GameContext _gameContext;
    private readonly StateContext _stateContext;

    // Party.
    private readonly Party _party;

    // Enemy formation.
    private readonly Formation _formation;

    public BattleScriptContextBuilder(
        GameContext gameContext, 
        StateContext stateContext, 
        Party party, 
        Formation formation
    ) 
    {
        _gameContext = gameContext;
        _stateContext = stateContext;
        _party = party;
        _formation = formation;
    }

    public ScriptContext BuildScriptContext()
    {
        var capabilityContainer = new CapabilityContainer();
        var variableTable = new VariableTable();

        // Construct capability container.
        capabilityContainer.SetCapability(typeof(IActiveBattleActions), new ActiveBattleActions(_formation));
        capabilityContainer.SetCapability(typeof(IHpMpActions), new HpMpActions(
            _party, 
            _gameContext.GameServices.DamageCalculator, 
            _formation
        ));

        return new ScriptContext(capabilityContainer, variableTable);
    }
}