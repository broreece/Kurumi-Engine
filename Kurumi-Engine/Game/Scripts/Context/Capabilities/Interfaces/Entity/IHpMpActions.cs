using Data.Runtime.Entities.Base;
using Game.Scripts.Context.Capabilities.Base;

namespace Game.Scripts.Context.Capabilities.Interfaces.Entity;

public interface IHpMpActions : ICapability 
{
    public void ApplyHealthChange(EntityId user, EntityId target, bool reduceHp, bool canKo, string formula);
}