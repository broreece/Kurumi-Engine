namespace Data.Models.Maps;

public sealed class TileModel 
{
    public required int X { get; set; }
    public required int Y { get; set; }
    
    public required IReadOnlyList<int> Objects { get; set; }
}