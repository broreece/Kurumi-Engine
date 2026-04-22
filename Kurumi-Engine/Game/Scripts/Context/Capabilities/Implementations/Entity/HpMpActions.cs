using Data.Runtime.Entities.Base;
using Game.Scripts.Context.Capabilities.Interfaces.Entity;

namespace Game.Scripts.Context.Capabilities.Implementations.Entity;

public sealed class HpMpActions : IHpMpActions 
{
    public void ApplyHealthChange(EntityId user, EntityId target, bool reduceHp, bool canKo, string formula) 
    {
        // TODO: Implement here.
    }
}