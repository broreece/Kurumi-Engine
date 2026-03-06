namespace Config.Runtime.Map;

/// <summary>
/// The config class for the tile sheet sprite config.
/// </summary>
public sealed class TileSheetConfig {
    /// <summary>
    /// The constructor for the tile sheet sprite config.
    /// </summary>
    /// <param name="tileWidth">The width of tiles in the tilesheet.</param>
    /// <param name="tileHeight">The height of tiles in the tilesheet.</param>
    /// <param name="tileSheetMaxTilesWide">The max tiles wide a tile sheet can be.</param>
    public TileSheetConfig(int tileWidth, int tileHeight, int tileSheetMaxTilesWide) {
        this.tileWidth = tileWidth;
        this.tileHeight = tileHeight;
        this.tileSheetMaxTilesWide = tileSheetMaxTilesWide;
    }

    /// <summary>
    /// Getter for the tile sheet width config.
    /// </summary>
    /// <returns>The tile sheet width config.</returns>
    public int GetTileWidth() {
        return tileWidth;
    }

    /// <summary>
    /// Getter for the tile sheet height config.
    /// </summary>
    /// <returns>The tile sheet height config.</returns>
    public int GetTileHeight() {
        return tileHeight;
    }

    /// <summary>
    /// Getter for the tile sheet max tiles wide config.
    /// </summary>
    /// <returns>The tile sheet max tiles wide config.</returns>
    public int GetTileSheetMaxTilesWide() {
        return tileSheetMaxTilesWide;
    }

    private readonly int tileWidth, tileHeight, tileSheetMaxTilesWide;
}