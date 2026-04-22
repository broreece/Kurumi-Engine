namespace Infrastructure.Database.Repositories.Rows.Actors;

public sealed class ActorSpriteRow 
{
    public required int Id { get; init; }
    public required string SpriteName { get; init; }
    public required int Width { get; init; }
    public required int Height { get; init; }
}