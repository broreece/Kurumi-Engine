namespace Data.Models.Maps;

public sealed class MapModel 
{
    public required string MachineName { get; set; }
    public required string Name { get; set; }
    public required string TileSheetName { get; set; }
    public required string BackgroundArtName { get; set; }

    public required int Width { get; set; }
    public required int Height { get; set; }
    
    public required IReadOnlyList<TileModel> Tiles { get; set; } = [];
    public required IReadOnlyList<ActorModel> Actors { get; set; } = [];
}