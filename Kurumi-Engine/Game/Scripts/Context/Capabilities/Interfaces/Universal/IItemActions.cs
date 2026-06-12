// Game.
using Game.Scripts.Context.Capabilities.Base;

namespace Game.Scripts.Context.Capabilities.Interfaces.Universal;

public interface IItemActions : ICapability 
{
    public void AddItemFromPool(int poolId);
}