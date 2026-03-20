namespace Config.Runtime.Defaults;

/// <summary>
/// The defaults class for the text window.
/// </summary>
public sealed class TextWindowDefaults {
    /// <summary>
    /// Constructor for the text window defaults class.
    /// </summary>
    /// <param name="windowId">The default text window art ID.</param>
    /// <param name="fontId">The default text window font ID.</param>
    /// <param name="fontSize">The default text window font size.</param>
    /// <param name="windowWidth">The default text window width.</param>
    /// <param name="windowHeight">The default text window height.</param>
    /// <param name="windowX">The default text window X location.</param>
    /// <param name="windowY">The default text window Y location.</param>
    public TextWindowDefaults(int windowId, int fontId, int fontSize, int windowWidth, int windowHeight, int windowX, 
        int windowY) {
        this.windowId = windowId;
        this.fontId = fontId;
        this.fontSize = fontSize;
        this.windowWidth = windowWidth;
        this.windowHeight = windowHeight;
        this.windowX = windowX;
        this.windowY = windowY;
    }

    /// <summary>
    /// Getter for the text window art ID default value.
    /// </summary>
    /// <returns>The text window art ID default value.</returns>
    public int GetWindowId() {
        return windowId;
    }

    /// <summary>
    /// Getter for the text window font ID default value.
    /// </summary>
    /// <returns>The text window font ID default value.</returns>
    public int GetFontId() {
        return fontId;
    }

    /// <summary>
    /// Getter for the text window font size default value.
    /// </summary>
    /// <returns>The text window font size default value.</returns>
    public int GetFontSize() {
        return fontSize;
    }

    /// <summary>
    /// Getter for the text window width default value.
    /// </summary>
    /// <returns>The text window width default value.</returns>
    public int GetWindowWidth() {
        return windowWidth;
    }

    /// <summary>
    /// Getter for the text window height default value.
    /// </summary>
    /// <returns>The text window height default value.</returns>
    public int GetWindowHeight() {
        return windowHeight;
    }

    /// <summary>
    /// Getter for the text window X default value.
    /// </summary>
    /// <returns>The text window X default value.</returns>
    public int GetWindowX() {
        return windowX;
    }

    /// <summary>
    /// Getter for the text window Y default value.
    /// </summary>
    /// <returns>The text window Y default value.</returns>
    public int GetWindowY() {
        return windowY;
    }

    private readonly int windowId, fontId, fontSize, windowWidth, windowHeight, windowX, windowY;
}