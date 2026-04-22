using Data.Definitions.Modifiers.Base;

namespace Data.Definitions.Entities.Status.Core;

public sealed class Status 
{
    public required int Id { get; init; }
    public required int MaxTurns { get; init; }
    public required int Priority { get; init; }

    public required string Name { get; init; }
    public required string Description { get; init; }
    public required string SpriteName { get; init; }

    public required bool CanAct { get; init; }
    public required bool CureAtEnd { get; init; }

    public required IReadOnlyDictionary<ModifierType, IEntityModifier> EntityModifiers { get; init; }
}