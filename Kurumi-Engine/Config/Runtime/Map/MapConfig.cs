namespace Config.Runtime.Map;

public sealed class MapConfig 
{
    public required int MaxTilesWide { get; init; }
    public required int MaxTilesHigh { get; init; }
    public required int RandomActorMoveVariance { get; init; }
}