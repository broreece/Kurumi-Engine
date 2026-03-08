namespace Config.Runtime.Defaults;

/// <summary>
/// The defaults class for the global message.
/// </summary>
public sealed class GlobalMessageDefaults {
    /// <summary>
    /// Constructor for the global message defaults class.
    /// </summary>
    /// <param name="globalMessageWindowId">The default global message window ID.</param>
    /// <param name="globalMessageFontId">The default global message font ID.</param>
    /// <param name="globalMessageFontSize">The default global message font size.</param>
    /// <param name="globalMessageWidth">The default global message width.</param>
    /// <param name="globalMessageHeight">The default global message height.</param>
    /// <param name="globalMessageX">The default global message X location.</param>
    /// <param name="globalMessageY">The default global message Y location.</param>
    public GlobalMessageDefaults(int globalMessageWindowId, int globalMessageFontId, int globalMessageFontSize, int globalMessageWidth, 
        int globalMessageHeight, int globalMessageX, 
        int globalMessageY) {
        this.globalMessageWindowId = globalMessageWindowId;
        this.globalMessageFontId = globalMessageFontId;
        this.globalMessageFontSize = globalMessageFontSize;
        this.globalMessageWidth = globalMessageWidth;
        this.globalMessageHeight = globalMessageHeight;
        this.globalMessageX = globalMessageX;
        this.globalMessageY = globalMessageY;
    }

    /// <summary>
    /// Getter for the global message window ID default value.
    /// </summary>
    /// <returns>The global message window ID default value.</returns>
    public int GetGlobalMessageWindowId() {
        return globalMessageWindowId;
    }

    /// <summary>
    /// Getter for the global message font ID default value.
    /// </summary>
    /// <returns>The global message font ID default value.</returns>
    public int GetGlobalMessageFontId() {
        return globalMessageFontId;
    }

    /// <summary>
    /// Getter for the global message font size default value.
    /// </summary>
    /// <returns>The global message font size default value.</returns>
    public int GetGlobalMessageFontSize() {
        return globalMessageFontSize;
    }

    /// <summary>
    /// Getter for the global message width default value.
    /// </summary>
    /// <returns>The global message width default value.</returns>
    public int GetGlobalMessageWidth() {
        return globalMessageWidth;
    }

    /// <summary>
    /// Getter for the global message height default value.
    /// </summary>
    /// <returns>The global message height default value.</returns>
    public int GetGlobalMessageHeight() {
        return globalMessageHeight;
    }

    /// <summary>
    /// Getter for the global message X default value.
    /// </summary>
    /// <returns>The global message X default value.</returns>
    public int GetGlobalMessageX() {
        return globalMessageX;
    }

    /// <summary>
    /// Getter for the global message Y default value.
    /// </summary>
    /// <returns>The global message Y default value.</returns>
    public int GetGlobalMessageY() {
        return globalMessageY;
    }

    private readonly int globalMessageWindowId, globalMessageFontId, globalMessageFontSize, globalMessageWidth, 
        globalMessageHeight, globalMessageX, globalMessageY;
}