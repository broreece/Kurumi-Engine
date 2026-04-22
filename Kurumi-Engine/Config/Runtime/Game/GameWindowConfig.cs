namespace Config.Runtime.Game;

public sealed class GameWindowConfig 
{
    public required int Width { get; init; }
    public required int Height { get; init; }
    public required string Title { get; init; }
}