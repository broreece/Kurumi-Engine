using Data.Models.Maps;
using Data.Runtime.Actors.Core;

namespace Data.Runtime.Maps.Core;

public sealed class Map 
{
    private readonly MapModel _mapModel;
    private readonly IReadOnlyDictionary<(int, int), TileModel> _tileDictionary;

    private Dictionary<(int, int), List<Actor>>? _actorDictionary;
    private IReadOnlyList<Actor>? _actors;

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
    public void SetActors(IReadOnlyList<Actor> actors, Dictionary<(int, int), List<Actor>> actorDictionary) 
    {
        _actors = actors;
        _actorDictionary = actorDictionary;
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

    public int GetWidth() => _mapModel.Width;

    public int GetHeight() => _mapModel.Height;

    public IReadOnlyList<Actor> GetActors() => _actors!;

    public IReadOnlyList<TileModel> GetTiles() => _mapModel.Tiles;

    public string GetTileSheetName() => _mapModel.TileSheetName;
}