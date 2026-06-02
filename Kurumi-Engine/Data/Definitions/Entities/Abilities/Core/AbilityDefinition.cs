namespace Data.Definitions.Entities.Abilities.Core;

public sealed class AbilityDefinition
{
    public required int Id { get; init; }
    public required int ElementId { get; init; }
    public required int Cost { get; init; }

    public required string Name { get; init; }
    public required string Description { get; init; }
    public required string SpriteName { get; init; }

    public required string? ScriptName { get; init; }

    public required bool UsesMp { get; init; }
    public required bool UseableInMenu { get; init; }

    public required bool DefaultTargetParty { get; init; }
    public required bool RandomTarget { get; init; }
    public required bool TargetsAll { get; init; }
}
