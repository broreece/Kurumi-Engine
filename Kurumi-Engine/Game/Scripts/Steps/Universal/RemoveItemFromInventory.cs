// Game.
using Game.Scripts.Base;
using Game.Scripts.Context.Capabilities.Interfaces.Universal;
using Game.Scripts.Context.Core;

namespace Game.Scripts.Steps.Universal;

public sealed class RemoveItemFromInventory : ScriptStep 
{
    private readonly int _itemId;
    private readonly int _amount;

    public RemoveItemFromInventory(int itemId, int amount) : base() 
    {
        _itemId = itemId;
        _amount = amount;
    }

    public override void Activate(ScriptContext scriptContext) 
    {
        IItemActions itemActions = scriptContext.GetCapability<IItemActions>();
        itemActions.RemoveItemFromInventory(_itemId, _amount);
    }
}
