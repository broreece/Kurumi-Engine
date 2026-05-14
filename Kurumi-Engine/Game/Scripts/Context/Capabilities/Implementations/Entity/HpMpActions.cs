using Data.Runtime.Entities.Base;
using Data.Runtime.Formations.Core;
using Data.Runtime.Party.Core;

using Engine.Systems.Combat.Core;

using Game.Scripts.Context.Capabilities.Interfaces.Entity;

namespace Game.Scripts.Context.Capabilities.Implementations.Entity;

public sealed class HpMpActions : IHpMpActions 
{
    private readonly Party _party;
    private readonly DamageCalculator _damageCalculator;

    private readonly Formation? _formation;

    public HpMpActions(Party party, DamageCalculator damageCalculator, Formation? formation = null)
    {
        _party = party;
        _damageCalculator = damageCalculator;

        _formation = formation;
    }

    public void ApplyHealthChange(EntityIndex user, EntityIndex target, bool reduceHp, bool canKo, string formula) 
    {
        // TODO: Change '!' to question mark throw custom exception.
        IStats userStats = user.EntityType == EntityType.Character ? _party.Characters[user.Index] :
            _formation!.GetEntityAt(user.Index);
        IStats targetStats = target.EntityType == EntityType.Character ? _party.Characters[target.Index] :
            _formation!.GetEntityAt(target.Index);
        
        var value = _damageCalculator.Evaluate(formula, userStats.GetStats(), targetStats.GetStats());
        if (reduceHp && value > 0)
        {
            if (canKo)
            {
                targetStats.CurrentHP -= value;
                targetStats.CurrentHP = targetStats.CurrentHP < 0 ? 0 : targetStats.CurrentHP;
            }
            else
            {
                targetStats.CurrentHP -= value;
                targetStats.CurrentHP = targetStats.CurrentHP < 1 ? 1 : targetStats.CurrentHP;
            }
        }
        else if (value > 0)
        {
            targetStats.CurrentHP += value;
            // Ensure HP is not higher then max HP.
            targetStats.CurrentHP = targetStats.CurrentHP > targetStats.GetMaxHp() ? targetStats.GetMaxHp() :
                targetStats.CurrentHP;
        }
    }
}