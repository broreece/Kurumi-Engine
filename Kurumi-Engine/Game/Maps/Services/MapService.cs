using Data.Runtime.Maps.Core;
using Data.Runtime.Maps.Factories;
using Game.Maps.Loader;
using Game.Maps.Registry;

namespace Game.Maps.Services;

/// <summary>
/// Can be used to load a new map, contains the map registry and loader.
/// </summary>
public sealed class MapService 
{
    private readonly MapRegistry _registry;
    private readonly MapLoader _loader;
    private readonly MapFactory _mapFactory;

    public MapService(MapRegistry registry, MapLoader loader, MapFactory mapFactory) 
    {
        _registry = registry;
        _loader = loader;
        _mapFactory = mapFactory;
    }

    public Map LoadMap(string mapName) {
        var path = _registry.GetMapFileName(mapName);
        return _mapFactory.Create(_loader.LoadMap(path));
    }
}