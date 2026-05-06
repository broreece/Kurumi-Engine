namespace Config.Runtime.Battle;

public sealed class CharacterBattleSpriteConfig 
{
    public required int Width { get; init; }
    public required int Height { get; init; }

    public required int PartyXPlacement { get; init; }
    public required int PartyYPlacement { get; init; }
}