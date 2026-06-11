// Data.
using Data.Runtime.Maps.Base.Change;

// Engine.
using Engine.Context.Containers;

// Game.
using Game.Scripts.Context.Capabilities.Interfaces.Map;

namespace Game.Scripts.Context.Capabilities.Implementations.Maps.Core;

public sealed class MapNavigationActions : IMapNavigationActions 
{
    private readonly GameObjects _gameObjects;

    internal MapNavigationActions(GameObjects gameObjects) 
    {
        _gameObjects = gameObjects;
    }

    public void ChangeMap(MapChangeRequest mapChangeRequest) 
    {
        _gameObjects.MapChangeRequest = mapChangeRequest;
    }
}