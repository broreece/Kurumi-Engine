namespace Config.Runtime.Defaults;

/// <summary>
/// The defaults class for the global message.
/// </summary>
public sealed class GlobalMessageDefaults {
    /// <summary>
    /// Constructor for the global message defaults class.
    /// </summary>
    /// <param name="windowId">The default global message window ID.</param>
    /// <param name="fontId">The default global message font ID.</param>
    /// <param name="fontSize">The default global message font size.</param>
    /// <param name="width">The default global message width.</param>
    /// <param name="height">The default global message height.</param>
    /// <param name="windowX">The default global message X location.</param>
    /// <param name="windowY">The default global message Y location.</param>
    public GlobalMessageDefaults(int windowId, int fontId, int fontSize, int width, int height, int windowX, 
        int windowY) {
        this.windowId = windowId;
        this.fontId = fontId;
        this.fontSize = fontSize;
        this.width = width;
        this.height = height;
        this.windowX = windowX;
        this.windowY = windowY;
    }

    /// <summary>
    /// Getter for the global message window ID default value.
    /// </summary>
    /// <returns>The global message window ID default value.</returns>
    public int GetGlobalMessageWindowId() {
        return windowId;
    }

    /// <summary>
    /// Getter for the global message font ID default value.
    /// </summary>
    /// <returns>The global message font ID default value.</returns>
    public int GetGlobalMessageFontId() {
        return fontId;
    }

    /// <summary>
    /// Getter for the global message font size default value.
    /// </summary>
    /// <returns>The global message font size default value.</returns>
    public int GetGlobalMessageFontSize() {
        return fontSize;
    }

    /// <summary>
    /// Getter for the global message width default value.
    /// </summary>
    /// <returns>The global message width default value.</returns>
    public int GetGlobalMessageWidth() {
        return width;
    }

    /// <summary>
    /// Getter for the global message height default value.
    /// </summary>
    /// <returns>The global message height default value.</returns>
    public int GetGlobalMessageHeight() {
        return height;
    }

    /// <summary>
    /// Getter for the global message X default value.
    /// </summary>
    /// <returns>The global message X default value.</returns>
    public int GetGlobalMessageX() {
        return windowX;
    }

    /// <summary>
    /// Getter for the global message Y default value.
    /// </summary>
    /// <returns>The global message Y default value.</returns>
    public int GetGlobalMessageY() {
        return windowY;
    }

    private readonly int windowId, fontId, fontSize, width, height, windowX, windowY;
}