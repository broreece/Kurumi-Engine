namespace Game.Map.Tiles;

/// <summary>
/// Tiles are a list of objects, containing helper functions to determine properties of it's objects grouped together.
/// </summary>
public sealed class Tile {
    /// <summary>
    /// Constructor for the tile.
    /// </summary>
    /// <param name="objects">A list of the tiles objects.</params>
    public Tile(List<TileObject> objects) {
        this.objects = objects;
    }

    /// <summary>
    /// Getter for the list of objects from the tile.
    /// </summary>
    /// <returns>The list of all objects on the tile.</returns>
    public List<TileObject> GetObjects() {
        return objects;
    }

    /// <summary>
    /// Function that adds a new tile object onto the tile.
    /// </summary>
    /// <param name="newObject">The new tile object to be added.</param>
    public void AddObject(TileObject newObject) {
        objects.Add(newObject);
    }
    
    /// <summary>
    /// Function that checks if any object in the tile is animated.
    /// </summary>
    /// <returns>True: Tile has any animated objects; False: Tile is empty or has no animated objects.</returns>
    public bool IsAnimated() {
        if (objects.Count == 0) {
            return false;
        }
        return objects.Any(tileObject => tileObject.IsAnimated());
    }

    /// <summary>
    /// Function that checks if every object in the tile is passable.
    /// </summary>
    /// <returns>True: Tile is all passable; False: Tile is empty or has an impassable object.</returns>
    public bool IsPassable() {
        if (objects.Count == 0) {
            return false;
        }
        return objects.All(tileObject => tileObject.IsPassable());
    }

    private readonly List<TileObject> objects;
}
