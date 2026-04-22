using Engine.Systems.Combat.Core;

namespace Engine.Systems.Combat.Factories;

public sealed class DamageCalculatorFactory {
    private readonly IReadOnlyDictionary<string, int> _statShortNameIndex;

    public DamageCalculatorFactory(IReadOnlyDictionary<string, int> statShortNameIndex) {
        _statShortNameIndex = statShortNameIndex;
    }

    public DamageCalculator Create() {
        return new DamageCalculator(_statShortNameIndex);
    }
}