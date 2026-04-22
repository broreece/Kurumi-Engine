namespace Config.Runtime.Defaults;

public sealed class GlobalMessageDefaults 
{
    public required int WindowId { get; init; }
    public required int FontId { get; init; }
    public required int FontSize { get; init; }

    public required int Width { get; init; }
    public required int Height { get; init; }

    public required int X { get; init; }
    public required int Y { get; init; }
}