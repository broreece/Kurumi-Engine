namespace Data.Definitions.Entities.Core;

public sealed class EntityDefinition 
{
    public required int Id { get; init; }
    public required int MaxHp { get; init; }

    public required string SpriteName { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }

    public required IReadOnlyList<int> Stats { get; init; }
    public required IReadOnlyList<int> ElementalResistances { get; init; }
    public required IReadOnlyList<int> StatusResistances { get; init; }

    public required IReadOnlyList<int> BaseAbilities { get; init; }
}
