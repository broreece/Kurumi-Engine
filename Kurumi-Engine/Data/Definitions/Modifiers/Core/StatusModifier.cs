using Data.Definitions.Modifiers.Base;

namespace Data.Definitions.Modifiers.Core;

public sealed class StatusModifier : IEntityModifier 
{
    public required IReadOnlyDictionary<int, int> Statuses { get; init; }
}