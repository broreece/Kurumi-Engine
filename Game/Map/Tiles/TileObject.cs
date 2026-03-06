namespace Game.Map.Tiles;

/// <summary>
/// Tile objects contain information that make up a tile.
/// </summary>
public sealed class TileObject {
    /// <summary>
    /// Constructor for the tile object, parameters relate to information about the object.
    /// </summary>
    /// <param name="artId">Art ID of the tile object.</param>
    /// <param name="animated">If the tile is animated.</param>
    /// <param name="passable">If the tile can be walked on.</param>
    public TileObject(int artId, bool animated, bool passable) {
        this.artId = artId;
        this.animated = animated;
        this.passable = passable;
    }

    /// <summary>
    /// Getter for the tile objects art ID.
    /// </summary>
    /// <returns>The art ID of the tile object.</returns>
    public int GetArtId() {
        return artId;
    }

    /// <summary>
    /// Returns if the tile object is animated.
    /// </summary>
    /// <returns>True: Tile is animated; False: Tile is not animated</returns>
    public bool IsAnimated() {
        return animated;
    }

    /// <summary>
    /// Returns if the tile object is passable.
    /// </summary>
    /// <returns>True: Tile is passable; False: Tile is not passable</returns>
    public bool IsPassable() {
        return passable;
    }

    private readonly int artId;
    private readonly bool animated;
    private readonly bool passable;
}
