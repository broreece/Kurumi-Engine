namespace Data.Definitions.Maps.Core;

public sealed class Tile 
{
    public required int Id { get; init; }
    public required int ArtId { get; init; }

    public required bool Animated { get; init; }
    public required bool Passable { get; init; }
    public required bool SeeThrough { get; init; }
}
