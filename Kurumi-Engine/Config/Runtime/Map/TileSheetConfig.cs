namespace Config.Runtime.Map;

public sealed class TileSheetConfig 
{
    public required int Width { get; init; }
    public required int Height { get; init; }
    public required int TileSheetMaxTilesWide { get; init; }
}