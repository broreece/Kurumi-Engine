using Data.Runtime.Maps.Base;
using Engine.Context.Containers;
using Game.Scripts.Context.Capabilities.Interfaces.Map;

namespace Game.Scripts.Context.Capabilities.Implementations.Maps;

public sealed class MapNavigationActions : IMapNavigationActions 
{
    private readonly GameObjects _gameObjects;

    public MapNavigationActions(GameObjects gameObjects) 
    {
        _gameObjects = gameObjects;
    }

    public void ChangeMap(MapChangeRequest mapChangeRequest) 
    {
        _gameObjects.MapChangeRequest = mapChangeRequest;
    }
}