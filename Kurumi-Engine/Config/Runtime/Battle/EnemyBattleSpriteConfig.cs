namespace Config.Runtime.Battle;

public sealed class EnemyBattleSpriteConfig 
{
    public required int Width { get; init; }
    public required int Height { get; init; }

    public required float WidthScale { get; init; }
    public required float HeightScale { get; init; }
}