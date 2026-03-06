namespace Config.Runtime.Windows;

/// <summary>
/// The config class for the font config.
/// </summary>
public sealed class FontConfig {
    /// <summary>
    /// The constructor for the font config.
    /// </summary>
    /// <param name="fontSize">The fontsize config.</param>
    public FontConfig(int fontSize) {
        this.fontSize = fontSize;
    }

    /// <summary>
    /// Getter for the font size config.
    /// </summary>
    /// <returns>The font size config.</returns>
    public int GetFontSize() {
        return fontSize;
    }

    private readonly int fontSize;
}