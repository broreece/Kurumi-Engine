namespace Data.Runtime.Entities.Base;

public readonly record struct EntityId 
{
    public required int Id { get; init; }

    public required EntityType EntityType { get; init; }
}