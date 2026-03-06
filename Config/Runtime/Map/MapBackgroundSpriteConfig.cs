namespace Config.Runtime.Map;

/// <summary>
/// The config class for the map background sprite config.
/// </summary>
public sealed class MapBackgroundSpriteConfig {
    /// <summary>
    /// The constructor for the map background sprite config.
    /// </summary>
    /// <param name="mapBackgroundWidth">The map background width config.</param>
    /// <param name="mapBackgroundHeight">The map background height config.</param>
    public MapBackgroundSpriteConfig(int mapBackgroundWidth, int mapBackgroundHeight) {
        this.mapBackgroundWidth = mapBackgroundWidth;
        this.mapBackgroundHeight = mapBackgroundHeight;
    }

    /// <summary>
    /// Getter for the map background width config.
    /// </summary>
    /// <returns>The map background width config.</returns>
    public int GetMapBackgroundWidth() {
        return mapBackgroundWidth;
    }

    /// <summary>
    /// Getter for the map background height config.
    /// </summary>
    /// <returns>The map background height config.</returns>
    public int GetMapBackgroundHeight() {
        return mapBackgroundHeight;
    }

    private readonly int mapBackgroundWidth, mapBackgroundHeight;
}