namespace Config.Runtime.Game;

/// <summary>
/// The config class for the game window config.
/// </summary>
public sealed class GameWindowConfig {
    /// <summary>
    /// The constructor for the game window config.
    /// </summary>
    /// <param name="windowWidth">The game window width.</param>
    /// <param name="windowHeight">The game window height.</param>
    /// <param name="windowTitle">The game window title.</param>
    public GameWindowConfig(int windowWidth, int windowHeight, string windowTitle) {
        this.windowWidth = windowWidth;
        this.windowHeight = windowHeight;
        this.windowTitle = windowTitle;
    }

    /// <summary>
    /// Getter for the game window width config.
    /// </summary>
    /// <returns>The game window width config.</returns>
    public int GetWindowWidth() {
        return windowWidth;
    }

    /// <summary>
    /// Getter for the game window height config.
    /// </summary>
    /// <returns>The game window height config.</returns>
    public int GetWindowHeight() {
        return windowHeight;
    }

    /// <summary>
    /// Getter for the game window title config.
    /// </summary>
    /// <returns>The game window title config.</returns>
    public string GetWindowTitle() {
        return windowTitle;
    }

    private readonly int windowWidth, windowHeight;
    private readonly string windowTitle;
}