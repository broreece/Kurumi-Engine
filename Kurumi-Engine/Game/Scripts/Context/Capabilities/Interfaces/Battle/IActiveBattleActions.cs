using Game.Scripts.Context.Capabilities.Base;

namespace Game.Scripts.Context.Capabilities.Interfaces.Battle;

public interface IActiveBattleActions : ICapability 
{
    public void ActivateAbility(int abilityId);
    
    public void KillEnemy(int enemyIndex);
}