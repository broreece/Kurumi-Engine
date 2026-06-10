// Data.
using Data.Runtime.Formations.Core;
using Data.Runtime.Parties.Core;

// Engine.
using Engine.Systems.Combat.Core;

// Game.
using Game.Scripts.Context.Capabilities.Implementations.Entity.Core;

namespace Game.Scripts.Context.Capabilities.Implementations.Entity.Factories;

public sealed class HpMpActionsFactory
{
    private readonly Party _party;
    
    private readonly DamageCalculator _damageCalculator;

    public HpMpActionsFactory(Party party, DamageCalculator damageCalculator)
    {
        _party = party;
        _damageCalculator = damageCalculator;
    }

    public HpMpActions Create(Formation? formation)
    {
        return new HpMpActions(_party, _damageCalculator, formation);
    }
}