namespace Engine.MapManager.Serialization;

/// <summary>
/// Actor data used to deseralize actors from the tile's data.
/// </summary>
public sealed class ActorData {
    public int Actor { get; set; } 
    public int Facing { get; set; }
    public int Visible { get; set; }
}