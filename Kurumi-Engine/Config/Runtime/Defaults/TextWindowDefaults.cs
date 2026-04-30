namespace Config.Runtime.Defaults;

public sealed class TextWindowDefaults 
{
    public required string WindowName { get; init; }
    public required string FontName { get; init; }
    public required int FontSize { get; init; }

    public required int Width { get; init; }
    public required int Height { get; init; }

    public required int X { get; init; }
    public required int Y { get; init; }
}