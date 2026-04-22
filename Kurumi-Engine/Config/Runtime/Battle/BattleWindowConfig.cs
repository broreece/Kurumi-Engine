namespace Config.Runtime.Battle;

public sealed class BattleWindowConfig 
{
    public required int WindowId { get; init; }
    public required int ChoiceBoxId { get; init; }
    public required int FontId { get; init; }
    public required int FontSize { get; init; }

    public required int InfoWindowWidth { get; init; }
    public required int InfoWindowHeight { get; init; }
    public required int InfoWindowX { get; init; }
    public required int InfoWindowY { get; init; }

    public required int SelectionWindowWidth { get; init; }
    public required int SelectionWindowHeight { get; init; }
    public required int SelectionWindowX { get; init; }
    public required int SelectionWindowY { get; init; }

    public required int PartyXPlacement { get; init; }
    public required int PartyYPlacement { get; init; }
}