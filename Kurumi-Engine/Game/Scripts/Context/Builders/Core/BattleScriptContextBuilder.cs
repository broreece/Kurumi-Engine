// Data.
using Data.Runtime.Formations.Core;

// Game.
using Game.Scripts.Context.Builder.Base;
using Game.Scripts.Context.Capabilities.Base;
using Game.Scripts.Context.Capabilities.Implementations.Battle.Factories;
using Game.Scripts.Context.Capabilities.Implementations.Entity.Factories;
using Game.Scripts.Context.Capabilities.Implementations.Universal.Core;
using Game.Scripts.Context.Capabilities.Interfaces.Battle;
using Game.Scripts.Context.Capabilities.Interfaces.Entity;
using Game.Scripts.Context.Capabilities.Interfaces.Universal;
using Game.Scripts.Context.Core;
using Game.Scripts.Context.Variables.Core;

namespace Game.Scripts.Context.Builder.Core;

public sealed class BattleScriptContextBuilder : IScriptContextBuilder
{
    // Enemy formation.
    private readonly Formation _formation;

    // Factories.
    private readonly ActiveBattleActionsFactory _activeBattleActionsFactory;

    private readonly HpMpActionsFactory _hpMpActionsFactory;

    private readonly ItemActionsFactory _itemActionsFactory;
    private readonly UIActionsFactory _uiActionsFactory;

    internal BattleScriptContextBuilder(
        ActiveBattleActionsFactory activeBattleActionsFactory,
        HpMpActionsFactory hpMpActionsFactory, 
        ItemActionsFactory itemActionsFactory, 
        UIActionsFactory uiActionsFactory, 
        Formation formation
    ) 
    {
        _activeBattleActionsFactory = activeBattleActionsFactory;
        _hpMpActionsFactory = hpMpActionsFactory;
        _itemActionsFactory = itemActionsFactory;
        _uiActionsFactory = uiActionsFactory;
        _formation = formation;
    }

    public ScriptContext BuildScriptContext()
    {
        var capabilityContainer = new CapabilityContainer();
        var variableTable = new VariableTable();

        // Construct capability container.
        capabilityContainer.SetCapability(typeof(IActiveBattleActions), _activeBattleActionsFactory.Create(_formation));

        capabilityContainer.SetCapability(typeof(IHpMpActions), _hpMpActionsFactory.Create(_formation));

        capabilityContainer.SetCapability(typeof(IItemActions), _itemActionsFactory.Create());

        capabilityContainer.SetCapability(typeof(IUIActions), _uiActionsFactory.Create());

        return new ScriptContext(capabilityContainer, variableTable);
    }
}