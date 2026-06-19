namespace Config.Runtime.Battle;

public sealed class BattleWindowConfig 
{
    public required string WindowArtName { get; init; }
    public required string ChoiceBoxArtName { get; init; }
    public required string FontName { get; init; }
    public required int FontSize { get; init; }

    public required int InfoWindowWidth { get; init; }
    public required int InfoWindowHeight { get; init; }
    public required int InfoWindowX { get; init; }
    public required int InfoWindowY { get; init; }
    public required int InfoWindowTextXOffset { get; init; }
    public required int InfoWindowTextYOffset { get; init; }

    public required int SelectionWindowWidth { get; init; }
    public required int SelectionWindowHeight { get; init; }
    public required int SelectionWindowX { get; init; }
    public required int SelectionWindowY { get; init; }
    public required int SelectionWindowTextXOffset { get; init; }
    public required int SelectionWindowTextYOffset { get; init; }

    public required int SelectionWindowSpacing { get; init; }

    public required int MaxChoicesPerPage { get; init; }
}