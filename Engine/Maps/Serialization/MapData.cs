namespace Engine.Maps.Serialization;

/// <summary>
/// Map data struct used to deseralize map's from the map's json file.
/// </summary>
public sealed class MapData {
    public string MachineName { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public int TileSheetId { get; set; }
    public int BackgroundArtId { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public List<TileData> Tiles { get; set; } = [];
}