namespace Config.Runtime.Windows;

using UI.Interfaces;

/// <summary>
/// The config class for the window config.
/// /// </summary>
public sealed class WindowConfig : IWindowFileAccessors {
    /// <summary>
    /// The constructor for the window config object.
    /// </summary>
    /// <param name="windowFileWidth">The window file width config.</param>
    /// <param name="windowFileHeight">The window file height config.</param>
    /// <param name="maxLinesPerWindow">The max lines per window config.</param>
    /// <param name="choiceSelectionFileWidth">The choice selection file width config.</param>
    /// <param name="choiceSelectionFileHeight">The choice selection file height config.</param>
    public WindowConfig(int windowFileWidth, int windowFileHeight, int maxLinesPerWindow, int choiceSelectionFileWidth, int choiceSelectionFileHeight) {
        this.windowFileWidth = windowFileWidth;
        this.windowFileHeight = windowFileHeight;
        this.maxLinesPerWindow = maxLinesPerWindow;
        this.choiceSelectionFileWidth = choiceSelectionFileWidth;
        this.choiceSelectionFileHeight = choiceSelectionFileHeight;
    }

    /// <summary>
    /// Getter for the window file width stored in the window config.
    /// </summary>
    /// <returns>The window file width in config.</returns>
    public int GetWindowFileWidth() {
        return windowFileWidth;
    }

    /// <summary>
    /// Getter for the window file height stored in the window config.
    /// </summary>
    /// <returns>The window file height in config.</returns>
    public int GetWindowFileHeight() {
        return windowFileHeight;
    }

    /// <summary>
    /// Getter for the maximum number of lines per window stored in the window config.
    /// </summary>
    /// <returns>The maximum number of lines per window in config.</returns>
    public int GetMaxLinesPerWindow() {
        return maxLinesPerWindow;
    }

    /// <summary>
    /// Getter for the choice selection file width stored in the window config.
    /// </summary>
    /// <returns>The choice selection file width in config.</returns>
    public int GetChoiceSelectionFileWidth() {
        return choiceSelectionFileWidth;
    }

    /// <summary>
    /// Getter for the choice selection file height stored in the window config.
    /// </summary>
    /// <returns>The choice selection file height in config.</returns>
    public int GetChoiceSelectionFileHeight() {
        return choiceSelectionFileHeight;
    }

    private readonly int windowFileWidth, windowFileHeight, maxLinesPerWindow, choiceSelectionFileWidth, choiceSelectionFileHeight;
}