// Data.
using Data.Runtime.Formations.Core;
using Data.Runtime.Parties.Core;

// Engine.
using Engine.Context.Core;

// Game.
using Game.Scripts.Context.Builder.Base;
using Game.Scripts.Context.Capabilities.Base;
using Game.Scripts.Context.Capabilities.Implementations.Battle;
using Game.Scripts.Context.Capabilities.Implementations.Entity;
using Game.Scripts.Context.Capabilities.Implementations.Universal.Core;
using Game.Scripts.Context.Capabilities.Interfaces.Battle;
using Game.Scripts.Context.Capabilities.Interfaces.Entity;
using Game.Scripts.Context.Capabilities.Interfaces.Universal;
using Game.Scripts.Context.Core;
using Game.Scripts.Context.Variables.Core;

namespace Game.Scripts.Context.Builder.Core;

public sealed class BattleScriptContextBuilder : IScriptContextBuilder
{
    // Contexts.
    private readonly GameContext _gameContext;

    // Party.
    private readonly Party _party;

    // Enemy formation.
    private readonly Formation _formation;

    // Factories.
    private readonly UIActionsFactory _uiActionsFactory;

    internal BattleScriptContextBuilder(
        GameContext gameContext, 
        Party party, 
        UIActionsFactory uiActionsFactory, 
        Formation formation
    ) 
    {
        _gameContext = gameContext;
        _party = party;
        _uiActionsFactory = uiActionsFactory;
        _formation = formation;
    }

    public ScriptContext BuildScriptContext()
    {
        var capabilityContainer = new CapabilityContainer();
        var variableTable = new VariableTable();

        // Construct capability container.
        capabilityContainer.SetCapability(typeof(IUIActions), _uiActionsFactory.Create());
        capabilityContainer.SetCapability(typeof(IActiveBattleActions), new ActiveBattleActions(_formation));
        capabilityContainer.SetCapability(typeof(IHpMpActions), new HpMpActions(
            _party, 
            _gameContext.GameServices.DamageCalculator, 
            _formation
        ));

        return new ScriptContext(capabilityContainer, variableTable);
    }
}