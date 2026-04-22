namespace Config.Runtime.Battle;

public sealed class PartyChoicesConfig 
{
    public required bool ItemsEnabled { get; init; }
    public required bool RunAwayEnabled { get; init; }

    public required string ItemsText { get; init; }
    public required string RunAwayText { get; init; }
}