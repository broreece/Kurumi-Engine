namespace Game.Map.Pathfinding;

/// <summary>
/// The navigation grid interface, used to restrict access to map data for actor controllers.
/// </summary>
public interface INavigationGrid {
    /// <summary>
    /// Function that checks if a given tile on the map is navigable.
    /// </summary>
    /// <param name="xLocation">The x location of the tile.</param>
    /// <param name="yLocation">The y location of the tile.</param>
    /// <returns></returns>
    public bool IsNavigable(int xLocation, int yLocation);
}