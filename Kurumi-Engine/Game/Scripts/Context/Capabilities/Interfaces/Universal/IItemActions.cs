// Game.
using Game.Scripts.Context.Capabilities.Base;

namespace Game.Scripts.Context.Capabilities.Interfaces.Universal;

public interface IItemActions : ICapability 
{
    public string AddItemFromPool(int poolId);

    public bool ContainsMoreThenOfItem(int itemId, int amount);

    public bool ContainsLessThenOfItem(int itemId, int amount);

    public bool ContainsSameAmountOfItem(int itemId, int amount);

    public bool ContainsDifferentAmountOfItem(int itemId, int amount);
}