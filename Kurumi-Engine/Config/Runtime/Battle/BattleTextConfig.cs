namespace Config.Runtime.Battle;

public sealed class BattleTextConfig 
{
    public required string BattleFontName { get; init; }

    public required int BattleFontSize { get; init; }

    public required int XOffset { get; init; }
    public required int YOffset { get; init; }

    public required float BattleDisplayLength { get; init; }
}