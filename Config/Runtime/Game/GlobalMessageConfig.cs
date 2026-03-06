namespace Config.Runtime.Game;

/// <summary>
/// The config class for the global message config.
/// </summary>
public sealed class GlobalMessageConfig {
    /// <summary>
    /// The constructor for the global message config.
    /// </summary>
    /// <param name="globalMessageWindowId">The global message window ID.</param>
    /// <param name="globalMessageFont">The global message font ID.</param>
    /// <param name="globalMessageWidth">The global message width.</param>
    /// <param name="globalMessageHeight">The global message height.</param>
    /// <param name="globalMessageX">The global message x.</param>
    /// <param name="globalMessageY">The global message y.</param>
    public GlobalMessageConfig(int globalMessageWindowId, int globalMessageFont, int globalMessageWidth, int globalMessageHeight, 
        int globalMessageX, int globalMessageY) {
        this.globalMessageWindowId = globalMessageWindowId;
        this.globalMessageFont = globalMessageFont;
        this.globalMessageWidth = globalMessageWidth;
        this.globalMessageHeight = globalMessageHeight;
        this.globalMessageX = globalMessageX;
        this.globalMessageY = globalMessageY;
    }

    /// <summary>
    /// Getter for the global message window ID config.
    /// </summary>
    /// <returns>The global message window ID config.</returns>
    public int GetGlobalMessageWindowId() {
        return globalMessageWindowId;
    }

    /// <summary>
    /// Getter for the global message font config.
    /// </summary>
    /// <returns>The global message font config.</returns>
    public int GetGlobalMessageFont() {
        return globalMessageFont;
    }

    /// <summary>
    /// Getter for the global message width config.
    /// </summary>
    /// <returns>The global message width config.</returns>
    public int GetGlobalMessageWidth() {
        return globalMessageWidth;
    }

    /// <summary>
    /// Getter for the global message height config.
    /// </summary>
    /// <returns>The global message height config.</returns>
    public int GetGlobalMessageHeight() {
        return globalMessageHeight;
    }

    /// <summary>
    /// Getter for the global message x config.
    /// </summary>
    /// <returns>The global message x config.</returns>
    public int GetGlobalMessageX() {
        return globalMessageX;
    }

    /// <summary>
    /// Getter for the global message y config.
    /// </summary>
    /// <returns>The global message y config.</returns>
    public int GetGlobalMessageY() {
        return globalMessageY;
    }

    private readonly int globalMessageWindowId, globalMessageFont, globalMessageWidth, globalMessageHeight, globalMessageX, globalMessageY;
}