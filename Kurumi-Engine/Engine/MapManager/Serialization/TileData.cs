namespace Engine.MapManager.Serialization;

/// <summary>
/// Tile data used to deseralize tiles from the map's json file.
/// </summary>
public sealed class TileData {
    public int XIndex { get; set; } 
    public int YIndex { get; set; }
    public string Objects { get; set; } = string.Empty;
    public ActorData? Actor { get; set; }
}