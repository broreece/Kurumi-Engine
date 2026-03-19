namespace Game.Maps.Core;

using Game.Maps.Actors.Core;
using Game.Maps.Elements;
using Game.Maps.Pathfinding;
using Game.Maps.Tiles;
using Game.Party;
using Scenes.Map.Interfaces;

/// <summary>
/// Maps contain 2d arrays of tiles, actors, encounters and other information relating to the map scene such as background, if the map name is displayed and music.
/// </summary>
public sealed class Map : INavigationGrid, IMapView {
    /// <summary>
    /// Constructor for maps. Loads a map from Files/Maps/{mapId}. 
    /// </summary>
    /// <param name="width">The width of the map.</param>
    /// <param name="height">The height of the map.</param>
    /// <param name="tileSheetId">The tile sheet id of the map.</param>
    /// <param name="backgroundArtId">The background art id of the map.</param>
    /// <param name="mapName">The name of the map.</param>
    /// <param name="animatedTiles">2D array of if the tile is animated.</param>
    /// <param name="tiles">2D array of tiles.</param>
    /// <param name="party">The game's party object.</param>
    public Map(int width, int height, int tileSheetId, int backgroundArtId, string mapName, bool[,] animatedTiles,
        Tile[,] tiles, Party party) {
        this.width = width;
        this.height = height;
        this.tileSheetId = tileSheetId;
        this.backgroundArtId = backgroundArtId;
        this.mapName = mapName;
        this.animatedTiles = animatedTiles;
        this.tiles = tiles;
        this.party = party;

        // Set dynamic data.
        actorForcedMove = new bool[width, height];

        // Assign null actor values.
        actors = new Actor[0, 0];
        listActors = [];
        autoActors = [];
        onFoundActors = [];
    }

    /// <summary>
    /// Function used to assign actors onto a map. 
    /// </summary>
    /// <param name="actors">2D array of actors.</param>
    /// <param name="listActors">List of actors.</param>
    public void AssignActors(Actor[,] actors, List<Actor> listActors) {
        this.actors = actors;
        this.listActors = listActors;

        // Create and store actors based on behaviour and create actor controllers.
        onFoundActors = [];
        autoActors = [];

        foreach (Actor actor in listActors) {
            // Add the actor to lists based on when it's activated.
            if (actor.ActivatesAutomatically()) {
                autoActors.Add(actor);
            }
            if (actor.ActivatesOnFind()) {
                onFoundActors.Add(actor);
            }
        }
    }

    /// <summary>
    /// Function that checks if a given coordinate is navigable in the map.
    /// </summary>
    /// <param name="xLocation">The x location being checked.</param>
    /// <param name="yLocation">The y location being checked.</param>
    /// <returns>If the tile is navigable.</returns>
    public bool IsNavigable(int xLocation, int yLocation) {
        return xLocation >= 0 && xLocation < width && yLocation >= 0 && yLocation < height &&
            tiles[xLocation, yLocation].IsPassable() && actors[xLocation, yLocation] == null;
    }

    /// <summary>
    /// Sets a map actor at a given point.
    /// </summary>
    /// <param name="xLocation">The x location of the actor being checked.</param>
    /// <param name="yLocation">The y location of the actor being checked.</param>
    /// <param name="actor">The new actor value.</param>
    public void SetActorAt(int xLocation, int yLocation, Actor? actor) {
        actors[xLocation, yLocation] = actor;
    }

    /// <summary>
    /// Setter that sets if an actor at a given coordinate is being forced to move.
    /// </summary>
    /// <param name="xLocation">The x location being checked.</param>
    /// <param name="yLocation">The y location being checked.</param>
    /// <param name="value">The new value at the given location if it's being forced to move.</param>
    public void SetActorAtIsForcedMoved(int xLocation, int yLocation, bool value) {
        actorForcedMove[xLocation, yLocation] = value;
    }

    /// <summary>
    /// Sets the parties current animation frame.
    /// </summary>
    /// <param name="newAnimationFrame">The new current animation frame of the party.</param>
    public void SetPartyCurrentAnimationFrame(int animationFrame) {
        party.SetCurrentAnimationFrame(animationFrame);
    }

    /// <summary>
    /// Checks if an actor at a given point is being forced to move.
    /// </summary>
    /// <param name="xLocation">The x location being checked.</param>
    /// <param name="yLocation">The y location being checked.</param>
    /// <returns>The actor at a given point.</returns>
    public bool ActorAtIsForcedMoved(int xLocation, int yLocation) {
        return actorForcedMove[xLocation, yLocation];
    }

    /// <summary>
    /// Returns a map actor at a provided location.
    /// </summary>
    /// <param name="xLocation">The x location of the actor being checked.</param>
    /// <param name="yLocation">The y location of the actor being checked.</param>
    /// <returns>The actor at a given point.</returns>
    public Actor ? GetActorAt(int xLocation, int yLocation) {
        return actors[xLocation, yLocation];
    }

    /// <summary>
    /// Returns the tile objects of a coordinate.
    /// </summary>
    /// <param name="xLocation">The x location of the tile being checked.</param>
    /// <param name="yLocation">The y location of the tile being checked.</param>
    /// <returns>The tile objects at a given point.</returns>
    public List<TileObject> GetTileObjectsOfTile(int xLocation, int yLocation) {
        return tiles[xLocation, yLocation].GetObjects();
    }

    /// <summary>
    /// Function that returns the parties lead field sprite ID.
    /// </summary>
    /// <returns>The map's associated party's lead field sprite ID.</returns>
    public int GetLeadFieldSpriteId() {
        return party.GetLeadFieldSpriteId();
    }

    /// <summary>
    /// Getter for the map's tiles.
    /// </summary>
    /// <returns>The map's tiles.</returns>
    public Tile[,] GetTiles() {
        return tiles;
    }

    /// <summary>
    /// Getter for a specific tile in the map.
    /// </summary>
    /// <param name="xLocation">The x location of the tile needed.</param>
    /// <param name="yLocation">The y location of the tile needed.</param>
    /// <returns>The specified tile.</returns>
    public Tile GetTile(int xLocation, int yLocation) {
        return tiles[xLocation, yLocation];
    }

    /// <summary>
    /// Getter for the map's actors.
    /// </summary>
    /// <returns>The map's actors.</returns>
    public Actor?[,] GetActors() {
        return actors;
    }
    
    /// <summary>
    /// Function used to return a list of actor handler views for the map scene.
    /// </summary>
    /// <returns>A list of actor handlers with restrictions to data for the map scene.</returns>
    public List<IActorView> GetListActorViews() {
        return [.. listActors];
    }

    /// <summary>
    /// Getter for the map's actors in the form of a list.
    /// </summary>
    /// <returns>The map's actors in the form of a list.</returns>
    public List<Actor> GetListActors() {
        return listActors;
    }
    
    /// <summary>
    /// Getter for the map's animated tiles.
    /// </summary>
    /// <returns>The map's animated tiles.</returns>
    public bool[,] GetAnimatedTiles() {
        return animatedTiles;
    }

    /// <summary>
    /// Getter for the map's name.
    /// </summary>
    /// <returns>The map's name.</returns>
    public string GetMapName() {
        return mapName;
    }

    /// <summary>
    /// Getter for the map's width.
    /// </summary>
    /// <returns>The map's width.</returns>
    public int GetWidth() {
        return width;
    }

    /// <summary>
    /// Getter for the map's height.
    /// </summary>
    /// <returns>The map's height.</returns>
    public int GetHeight() {
        return height;
    }

    /// <summary>
    /// Getter for the map's tile sheet ID.
    /// </summary>
    /// <returns>The map's tile sheet ID.</returns>
    public int GetTileSheetId() {
        return tileSheetId;
    }

    /// <summary>
    /// Getter for the map's background art ID.
    /// </summary>
    /// <returns>The map's background art ID.</returns>
    public int GetBackgroundArtId() {
        return backgroundArtId;
    }

    /// <summary>
    /// Gets the parties current animation frame.
    /// </summary>
    /// <returns>The parties current animation frame.</returns>
    public int GetPartyCurrentAnimationFrame() {
        return party.GetCurrentAnimationFrame();
    }

    /// <summary>
    /// Gets the parties X location.
    /// </summary>
    /// <returns>The parties X coordinate.</returns>
    public int GetPartyXLocation() {
        return party.GetXLocation();
    }

    /// <summary>
    /// Gets the parties Y location.
    /// </summary>
    /// <returns>The parties Y coordinate.</returns>
    public int GetPartyYLocation() {
        return party.GetYLocation();
    }

    /// <summary>
    /// Getter for the parties direction.
    /// </summary>
    /// <returns>The direction the party is facing.</returns>
    public int GetPartyDirection() {
        return party.GetDirection();
    }

    /// <summary>
    /// Getter for the map's actors that activate automatically.
    /// </summary>
    /// <returns>The list of actors that activate automatically.</returns>
    public List<Actor> GetAutoActors() {
        return autoActors;
    }

    /// <summary>
    /// Getter for the map's actors that activate when they find the party.
    /// </summary>
    /// <returns>The list of actors that activate when they find the party.</returns>
    public List<Actor> GetOnFoundActors() {
        return onFoundActors;
    }

    /// <summary>
    /// Getter for the party associated to the map.
    /// </summary>
    /// <returns>The party associated to the map.</returns>
    public MapElement GetParty() {
        return party;
    }

    // Tiles and actors.
    private readonly Tile[,] tiles;
    private Actor?[,] actors;
    private List<Actor> listActors;

    // Boolean grids.
    private readonly bool[,] animatedTiles;
    private readonly bool[,] actorForcedMove;

    // Static elements.
    private readonly string mapName;
    private readonly int width, height, tileSheetId, backgroundArtId;

    // Map actor variables.
    private List<Actor> autoActors, onFoundActors;

    // Stored party currently loaded on the map.
    private readonly Party party;
}
