namespace Engine.Maps.Serialization;

/// <summary>
/// Actor data used to deseralize actor from the tiles data.
/// </summary>
public sealed class ActorData {
    public int Actor { get; set; } 
    public int Facing { get; set; }
    public int Visible { get; set; }
}