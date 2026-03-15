namespace Registry.TileObjects;

using Game.Map.Tiles;

/// <summary>
/// The tile object data registry, contains data about the tile objects.
/// </summary>
public sealed class TileObjectRegistry {
    /// <summary>
    /// Constructor for the tile object data registry.
    /// </summary>
    /// <param name="tileObjects">The tile objects array.</param>
    public TileObjectRegistry(TileObject[] tileObjects) {
        this.tileObjects = tileObjects;
    }

    /// <summary>
    /// Getter for a specific tile object at a provided index.
    /// </summary>
    /// <param name="index">The index of the specified tile object.</param>
    /// <returns>The tile object at the index.</returns>
    public TileObject GetTileObject(int index) {
        return tileObjects[index];
    }

    private readonly TileObject[] tileObjects;
}