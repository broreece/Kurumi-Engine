namespace Config.Runtime.Menus;

public sealed class FileSelectorConfig 
{
    public required int WindowId { get; init; }
    public required int ChoiceBoxId { get; init; }

    public required int FontId { get; init; }
    public required int FontSize { get; init; }

    public required int MaxSavesOneScreen { get; init; }

    public required int MessageWindowWidth { get; init; }
    public required int MessageWindowHeight { get; init; }
    public required int MessageWindowX { get; init; }
    public required int MessageWindowY { get; init; }

    public required string SaveMessage { get; init; }
    public required string LoadMessage { get; init; }
    public required string WarningMessage { get; init; }

    public required int WarningWindowWidth { get; init; }
    public required int WarningWindowHeight { get; init; }
    public required int WarningWindowX { get; init; }
    public required int WarningWindowY { get; init; }

    public required int WarningChoiceWidth { get; init; }
    public required int WarningChoiceHeight { get; init; }
    public required int WarningChoiceX { get; init; }
    public required int WarningChoiceY { get; init; }
}
