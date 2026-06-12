// Game.
using Game.Scripts.Base;
using Game.Scripts.Context.Capabilities.Interfaces.Universal;
using Game.Scripts.Context.Core;

namespace Game.Scripts.Steps.Universal;

public sealed class AddItemFromPool : ScriptStep 
{
    private readonly int _poolId;

    public AddItemFromPool(int poolId) : base() 
    {
        _poolId = poolId;
    }

    public override void Activate(ScriptContext scriptContext) 
    {
        IItemActions itemActions = scriptContext.GetCapability<IItemActions>();
        itemActions.AddItemFromPool(_poolId);
    }
}
