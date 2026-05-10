namespace Data.Runtime.Entities.Base;

public readonly record struct EntityIndex 
{
    public required int Index { get; init; }

    public required EntityType EntityType { get; init; }
}