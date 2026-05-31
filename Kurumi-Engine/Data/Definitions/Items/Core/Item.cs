namespace Data.Definitions.Items.Core;

public sealed class Item 
{
    public required int Id { get; init; }
    public required int Price { get; init; }
    public required int Weight { get; init; }

    public required string SpriteName { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }

    public required string? ScriptName { get; init; }

    public required bool UseableInBattle { get; init; }
    public required bool UseableInMenu { get; init; }

    public required bool DefaultTargetParty { get; init; }
    public required bool RandomTarget { get; init; }
    public required bool TargetsAll { get; init; }

    public required bool ConsumeOnUse { get; init; }
}