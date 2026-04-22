namespace Config.Runtime.Windows;

public sealed class WindowConfig 
{
    public required int WindowFileWidth { get; init; }
    public required int WindowFileHeight { get; init; }

    public required int ChoiceSelectionFileWidth { get; init; }
    public required int ChoiceSelectionFileHeight { get; init; }

    public required int MaxLinesPerWindow { get; init; }
}