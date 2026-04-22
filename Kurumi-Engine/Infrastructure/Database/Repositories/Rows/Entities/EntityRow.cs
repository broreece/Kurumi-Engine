namespace Infrastructure.Database.Repositories.Rows.Entities;

public sealed class EntityRow 
{
    public required int Id { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required int MaxHP { get; init; }
    public required string BattleSpriteName { get; init; }
}