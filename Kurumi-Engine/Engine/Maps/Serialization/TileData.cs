namespace Engine.Maps.Serialization;

/// <summary>
/// Tile data used to deseralize tile from the maps json file.
/// </summary>
public sealed class TileData {
    public int XIndex { get; set; } 
    public int YIndex { get; set; }
    public string Objects { get; set; } = string.Empty;
    public ActorData? Actor { get; set; }
}