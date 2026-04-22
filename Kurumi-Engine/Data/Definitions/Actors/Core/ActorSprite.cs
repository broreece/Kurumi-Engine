namespace Data.Definitions.Actors.Core;

public sealed class ActorSprite 
{
    public required int Id { get; init; }
    public required int Width { get; init; }
    public required int Height { get; init; }

    public required string SpriteName { get; init; }
}