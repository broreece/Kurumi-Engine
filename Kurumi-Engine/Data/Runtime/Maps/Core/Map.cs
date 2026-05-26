// Data.
using Data.Models.Maps;
using Data.Runtime.Actors.Core;
using Data.Runtime.Formations.Core;
using Data.Runtime.Maps.Exceptions;

// Engine.
using Engine.Systems.Navigation.Base;

namespace Data.Runtime.Maps.Core;

public sealed class Map 
{
    private readonly MapModel _mapModel;

    private readonly IReadOnlyDictionary<(int, int), TileModel> _tileDictionary;

    private Dictionary<(int, int), Formation> _formationDictionary = [];

    private Dictionary<(int, int), List<Actor>>? _actorDictionary;
    private Dictionary<string, Actor>? _actorStringDictionary;

    // Formations, actors and the collision objects list used for navigation grids.
    public IReadOnlyList<Formation>? Formations { get; private set; }
    public IReadOnlyList<Actor>? Actors { get; private set; }

    public int Width => _mapModel.Width;

    public int Height => _mapModel.Height;

    public IReadOnlyList<TileModel> Tiles => _mapModel.Tiles;

    public string TileSheetName => _mapModel.TileSheetName;

    public string MapBackgroundArtName => _mapModel.BackgroundArtName;

    internal Map(MapModel mapModel, IReadOnlyDictionary<(int, int), TileModel> tileDictionary) 
    {
        _mapModel = mapModel;
        _tileDictionary = tileDictionary;
    }

    /// <summary>
    /// Function used after the map's creation to set the formations in the map.
    /// </summary>
    /// <param name="formations">The list of all formations.</param>
    /// <param name="formationDictionary">The dictionary of formations at each location.</param>
    public void SetFormations(
        IReadOnlyList<Formation> formations, 
        Dictionary<(int, int), Formation> formationDictionary
    )
    {
        Formations = formations;
        _formationDictionary = formationDictionary;
    }

    /// <summary>
    /// Function used after the map's creation to set the actors in the map.
    /// </summary>
    /// <param name="actors">The list of all actors.</param>
    /// <param name="actorDictionary">The dictionary of actors at each location.</param>
    /// <param name="actorStringDictionary">The dictionary of actors including their string keys.</param>
    public void SetActors(
        IReadOnlyList<Actor> actors, 
        Dictionary<(int, int), List<Actor>> actorDictionary,
        Dictionary<string, Actor>? actorStringDictionary
    ) 
    {
        Actors = actors;
        _actorDictionary = actorDictionary;
        _actorStringDictionary = actorStringDictionary;
    }

    public void AddActorTo(Actor actor, int xLocation, int yLocation) 
    {
        if (!_actorDictionary!.ContainsKey((xLocation, yLocation))) 
        {
            _actorDictionary[(xLocation, yLocation)] = [];
        }
        _actorDictionary![(xLocation, yLocation)].Add(actor);
    }

    public void AddFormationTo(Formation formation, int xLocation, int yLocation) 
    {
        if (!_formationDictionary.ContainsKey((xLocation, yLocation))) 
        {
            _formationDictionary[(xLocation, yLocation)] = formation;
        }
        else
        {
            throw new FormationAlreadyPresentException("A formation was already found at location: " +
                $"{xLocation}, {yLocation} on map: {_mapModel.MachineName}");
        }
    }

    public void RemoveActorAt(Actor actor, int xLocation, int yLocation) 
    {
        if (_actorDictionary!.ContainsKey((xLocation, yLocation))) 
        {
            _actorDictionary[(xLocation, yLocation)].Remove(actor);
        }
    }

    public void RemoveFormationAt(int xLocation, int yLocation) 
    {
        if (_formationDictionary.ContainsKey((xLocation, yLocation))) 
        {
            _formationDictionary.Remove((xLocation, yLocation));
        }
    }

    public IReadOnlyList<int> GetTileObjectsAt(int xLocation, int yLocation) 
    {
        if (_tileDictionary.ContainsKey((xLocation, yLocation))) 
        {
            return _tileDictionary[(xLocation, yLocation)].Objects;
        }
        return [];
    }

    public Formation? GetFormationAt(int xLocation, int yLocation) 
    {
        if (_formationDictionary!.ContainsKey((xLocation, yLocation))) 
        {
            return _formationDictionary[(xLocation, yLocation)];
        }
        return null;
    }

    public IReadOnlyList<Actor> GetActorsAt(int xLocation, int yLocation) 
    {
        if (_actorDictionary!.ContainsKey((xLocation, yLocation))) 
        {
            return _actorDictionary[(xLocation, yLocation)];
        }
        return [];
    }

    /// <summary>
    /// Function that returns a list of the potential collision objects at a provided location.
    /// </summary>
    /// <param name="xLocation">The X location being checked.</param>
    /// <param name="yLocation">The Y location being checked.</param>
    /// <returns>A list of collisionable objects at the provided location.</returns>
    public IReadOnlyList<ICollisionObject> GetCollisionObjectsAt(int xLocation, int yLocation)
    {
        // Make the base actors.
        List<ICollisionObject> collisionObjects = [.. GetActorsAt(xLocation, yLocation).Cast<ICollisionObject>()];

        // Add the formation if found.
        var potentialFormation = GetFormationAt(xLocation, yLocation);
        if (potentialFormation != null)
        {
            collisionObjects.Add(potentialFormation);
        }

        return collisionObjects;
    }

    public Actor GetActor(string key)
    {
        return _actorStringDictionary!.TryGetValue(key, out var result) ? result : 
            throw new ActorNotFoundException($"The map: {key} was not found.");
    }
}