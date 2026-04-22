namespace Config.Runtime.Map;

public sealed class CharacterFieldSpriteConfig 
{
    public required int Width { get; init; }
    public required int Height { get; init; }
    public required int WalkAnimationFrames { get; init; }
    public required float WalkAnimationSpeed { get; init; }
}