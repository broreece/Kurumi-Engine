using Data.Definitions.Modifiers.Base;

namespace Data.Definitions.Modifiers.Core;

public sealed class ElementModifier : IEntityModifier 
{
    public required IReadOnlyDictionary<int, int> Elements { get; init; }
}