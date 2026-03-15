namespace Config.Runtime.Map;

/// <summary>
/// The config class for the map background sprite config.
/// </summary>
public sealed class MapBackgroundSpriteConfig {
    /// <summary>
    /// The constructor for the map background sprite config.
    /// </summary>
    /// <param name="width">The map background width config.</param>
    /// <param name="height">The map background height config.</param>
    public MapBackgroundSpriteConfig(int width, int height) {
        this.width = width;
        this.height = height;
    }

    /// <summary>
    /// Getter for the map background width config.
    /// </summary>
    /// <returns>The map background width config.</returns>
    public int GetWidth() {
        return width;
    }

    /// <summary>
    /// Getter for the map background height config.
    /// </summary>
    /// <returns>The map background height config.</returns>
    public int GetHeight() {
        return height;
    }

    private readonly int width, height;
}