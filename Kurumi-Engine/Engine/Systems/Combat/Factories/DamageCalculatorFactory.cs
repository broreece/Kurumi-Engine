// Engine.
using Engine.Systems.Combat.Core;

// Infrastructure.
using Infrastructure.Database.Base;

namespace Engine.Systems.Combat.Factories;

public sealed class DamageCalculatorFactory 
{
    private readonly Index<int> _statShortNameIndex;

    public DamageCalculatorFactory(Index<int> statShortNameIndex) 
    {
        _statShortNameIndex = statShortNameIndex;
    }

    public DamageCalculator Create() 
    {
        return new DamageCalculator(_statShortNameIndex);
    }
}