using Engine.Context.Core;
using Engine.State.Base;

using Game.Scripts.Context.Builder.Base;
using Game.Scripts.Context.Capabilities.Base;
using Game.Scripts.Context.Capabilities.Implementations.Battle;
using Game.Scripts.Context.Capabilities.Interfaces.Battle;
using Game.Scripts.Context.Core;
using Game.Scripts.Context.Variables.Core;

namespace Game.Scripts.Context.Builder.Core;

public sealed class BattleScriptContextBuilder : IScriptContextBuilder
{
    private readonly GameContext _gameContext;
    private readonly StateContext _stateContext;

    public BattleScriptContextBuilder(GameContext gameContext, StateContext stateContext) 
    {
        _gameContext = gameContext;
        _stateContext = stateContext;
    }

    public ScriptContext BuildScriptContext()
    {
        var capabilityContainer = new CapabilityContainer();
        var variableTable = new VariableTable();

        // Construct capability container.
        capabilityContainer.SetCapability(typeof(IActiveBattleActions), new ActiveBattleActions());

        return new ScriptContext(capabilityContainer, variableTable);
    }
}