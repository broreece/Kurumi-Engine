using Data.Runtime.Entities.Base;
using Game.Scripts.Context.Capabilities.Base;

namespace Game.Scripts.Context.Capabilities.Interfaces.Entity;

public interface IHpMpActions : ICapability 
{
    public void ApplyHealthChange(EntityIndex user, EntityIndex target, bool reduceHp, bool canKo, string formula);
}