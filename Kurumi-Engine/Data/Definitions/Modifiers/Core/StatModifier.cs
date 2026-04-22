using Data.Definitions.Modifiers.Base;

namespace Data.Definitions.Modifiers.Core;

public sealed class StatModifier : IEntityModifier 
{
    public required IReadOnlyDictionary<int, int> Stats { get; init; }
}