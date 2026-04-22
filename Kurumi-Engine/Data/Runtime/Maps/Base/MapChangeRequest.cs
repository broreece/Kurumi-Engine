namespace Data.Runtime.Maps.Base;

/// <summary>
/// Class used to store information regarding a map change request. Stored in game objects.
/// </summary>
public sealed class MapChangeRequest 
{
    public required string MapName { get; init; }
    public required int X { get; init; }
    public required int Y { get; init; }
}