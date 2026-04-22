using Game.Scripts.Context.Capabilities.Base;
using Game.Scripts.Context.Variables.Base;
using Game.Scripts.Context.Variables.Core;

namespace Game.Scripts.Context.Core;

public sealed class ScriptContext {
    private readonly CapabilityContainer _capabilityContainer;
    private readonly VariableTable _variableTable;

    public ScriptContext(CapabilityContainer capabilityContainer, VariableTable variableTable) 
    {
        _capabilityContainer = capabilityContainer;
        _variableTable = variableTable;
    }

    public T GetCapability<T>() where T : class, ICapability => _capabilityContainer.GetCapability<T>();

    public void SetCapability(Type type, ICapability capability) => _capabilityContainer.SetCapability(type, capability);

    public T GetVariable<T>(VariableKey<T> key) => _variableTable.GetVariable<T>(key);

    public void SetVariable<T>(VariableKey<T> key, T variable) => _variableTable.SetVariable(key, variable);
}