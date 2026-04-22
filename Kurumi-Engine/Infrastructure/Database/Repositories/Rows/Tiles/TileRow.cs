namespace Infrastructure.Database.Repositories.Rows.Tiles;

public sealed class TileRow 
{
    public required int Id { get; init; }
    public required int ArtId { get; init; }
    public required bool Animated { get; init; }
    public required bool Passable { get; init; }
}