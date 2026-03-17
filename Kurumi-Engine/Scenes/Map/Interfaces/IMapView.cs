namespace Scenes.Map.Interfaces;

using Game.Map.Actors.Base;
using Game.Map.Elements;
using Game.Map.Tiles;

/// <summary>
/// The public map view interface, used to control visibility of map functions required in the map scene.
/// </summary>
public interface IMapView {
    /// <summary>
    /// Setter that sets if an actor at a given coordinate is being forced to move.
    /// </summary>
    /// <param name="xLocation">The x location being checked.</param>
    /// <param name="yLocation">The y location being checked.</param>
    /// <param name="value">The new value at the given location if it's being forced to move.</param>
    public void SetActorAtIsForcedMoved(int xLocation, int yLocation, bool value);

    /// <summary>
    /// Sets the parties current animation frame.
    /// </summary>
    /// <param name="newAnimationFrame">The new current animation frame of the party.</param>
    public void SetPartyCurrentAnimationFrame(int animationFrame);

    /// <summary>
    /// Checks if an actor at a given point is being forced to move.
    /// </summary>
    /// <param name="xLocation">The x location being checked.</param>
    /// <param name="yLocation">The y location being checked.</param>
    /// <returns>The actor at a given point.</returns>
    public bool ActorAtIsForcedMoved(int xLocation, int yLocation);

    /// <summary>
    /// Returns the tile objects of a coordinate.
    /// </summary>
    /// <param name="xLocation">The x location of the tile being checked.</param>
    /// <param name="yLocation">The y location of the tile being checked.</param>
    /// <returns>The tile objects at a given point.</returns>
    public List<TileObject> GetTileObjectsOfTile(int xLocation, int yLocation);

    /// <summary>
    /// Function that returns the parties lead field sprite ID.
    /// </summary>
    /// <returns>The map's associated party's lead field sprite ID.</returns>
    public int GetLeadFieldSpriteId();

    /// <summary>
    /// Getter for the map's actors.
    /// </summary>
    /// <returns>The map's actors.</returns>
    public IActorHandler?[,] GetActors();

    /// <summary>
    /// Getter for the map's actors in the form of a list.
    /// </summary>
    /// <returns>The map's actors in the form of a list.</returns>
    public List<IActorHandlerView> GetListActorViews();

    /// <summary>
    /// Getter for the map's width.
    /// </summary>
    /// <returns>The map's width.</returns>
    public int GetWidth();

    /// <summary>
    /// Getter for the map's height.
    /// </summary>
    /// <returns>The map's height.</returns>
    public int GetHeight();

    /// <summary>
    /// Getter for the map's tile sheet ID.
    /// </summary>
    /// <returns>The map's tile sheet ID.</returns>
    public int GetTileSheetId();

    /// <summary>
    /// Getter for the map's background art ID.
    /// </summary>
    /// <returns>The map's background art ID.</returns>
    public int GetBackgroundArtId();

    /// <summary>
    /// Function used to load the parties current animation frame.
    /// </summary>
    /// <returns>The current animation frame of the party.</returns>
    public int GetPartyCurrentAnimationFrame();

    public int GetPartyXLocation();

    public int GetPartyYLocation();

    public int GetPartyDirection();

    /// <summary>
    /// Getter for the party associated to the map.
    /// </summary>
    /// <returns>The party associated to the map.</returns>
    public MapElement GetParty();
}