namespace Config.Runtime.Defaults;

/// <summary>
/// The defaults class for the text window.
/// </summary>
public sealed class TextWindowDefaults {
    /// <summary>
    /// Constructor for the text window defaults class.
    /// </summary>
    /// <param name="textWindowArtId">The default text window art ID.</param>
    /// <param name="textWindowFontId">The default text window font ID.</param>
    /// <param name="textWindowFontSize">The default text window font size.</param>
    /// <param name="textWindowWidth">The default text window width.</param>
    /// <param name="textWindowHeight">The default text window height.</param>
    /// <param name="textWindowX">The default text window X location.</param>
    /// <param name="textWindowY">The default text window Y location.</param>
    public TextWindowDefaults(int textWindowArtId, int textWindowFontId, int textWindowFontSize, int textWindowWidth, 
        int textWindowHeight, int textWindowX, int textWindowY) {
        this.textWindowArtId = textWindowArtId;
        this.textWindowFontId = textWindowFontId;
        this.textWindowFontSize = textWindowFontSize;
        this.textWindowWidth = textWindowWidth;
        this.textWindowHeight = textWindowHeight;
        this.textWindowX = textWindowX;
        this.textWindowY = textWindowY;
    }

    /// <summary>
    /// Getter for the text window art ID default value.
    /// </summary>
    /// <returns>The text window art ID default value.</returns>
    public int GetTextWindowArtId() {
        return textWindowArtId;
    }

    /// <summary>
    /// Getter for the text window font ID default value.
    /// </summary>
    /// <returns>The text window font ID default value.</returns>
    public int GetTextWindowFontId() {
        return textWindowFontId;
    }

    /// <summary>
    /// Getter for the text window font size default value.
    /// </summary>
    /// <returns>The text window font size default value.</returns>
    public int GetTextWindowFontSize() {
        return textWindowFontSize;
    }

    /// <summary>
    /// Getter for the text window width default value.
    /// </summary>
    /// <returns>The text window width default value.</returns>
    public int GetTextWindowWidth() {
        return textWindowWidth;
    }

    /// <summary>
    /// Getter for the text window height default value.
    /// </summary>
    /// <returns>The text window height default value.</returns>
    public int GetTextWindowHeight() {
        return textWindowHeight;
    }

    /// <summary>
    /// Getter for the text window X default value.
    /// </summary>
    /// <returns>The text window X default value.</returns>
    public int GetTextWindowX() {
        return textWindowX;
    }

    /// <summary>
    /// Getter for the text window Y default value.
    /// </summary>
    /// <returns>The text window Y default value.</returns>
    public int GetTextWindowY() {
        return textWindowY;
    }

    private readonly int textWindowArtId, textWindowFontId, textWindowFontSize, textWindowWidth, textWindowHeight, 
        textWindowX, textWindowY;
}