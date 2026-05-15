using Data.Models.Maps;
using Data.Runtime.Actors.Core;
using Data.Runtime.Maps.Exceptions;

namespace Data.Runtime.Maps.Core;

public sealed class Map 
{
    private readonly MapModel _mapModel;
    private readonly IReadOnlyDictionary<(int, int), TileModel> _tileDictionary;

    private Dictionary<(int, int), List<Actor>>? _actorDictionary;
    private Dictionary<string, Actor>? _actorStringDictionary;

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
    /// Function used after the map's creation to set the actors in the map.
    /// </summary>
    /// <param name="actors">The list of all actors/</param>
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

    public void RemoveActorAt(Actor actor, int xLocation, int yLocation) 
    {
        if (_actorDictionary!.ContainsKey((xLocation, yLocation))) 
        {
            _actorDictionary[(xLocation, yLocation)].Remove(actor);
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

    public IReadOnlyList<Actor> GetActorsAt(int xLocation, int yLocation) 
    {
        if (_actorDictionary!.ContainsKey((xLocation, yLocation))) 
        {
            return _actorDictionary[(xLocation, yLocation)];
        }
        return [];
    }

    public Actor GetActor(string key)
    {
        return _actorStringDictionary!.TryGetValue(key, out var result) ? result : 
            throw new ActorNotFoundException($"The map: {key} was not found.");
    }
}