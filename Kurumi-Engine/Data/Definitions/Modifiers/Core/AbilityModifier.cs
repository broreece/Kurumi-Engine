using Data.Definitions.Modifiers.Base;

namespace Data.Definitions.Modifiers.Core;

public sealed class AbilityModifier : IEntityModifier 
{
    public required IReadOnlyList<int> SealedAbilitySets { get; init; }
    public required IReadOnlyList<int> SealedAbilities { get; init; }
    public required IReadOnlyList<int> AddedAbilities { get; init; }
}