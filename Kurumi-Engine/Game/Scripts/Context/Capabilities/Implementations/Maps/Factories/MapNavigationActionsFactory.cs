// Engine.
using Engine.Context.Containers;

// Game.
using Game.Scripts.Context.Capabilities.Implementations.Maps.Core;

namespace Game.Scripts.Context.Capabilities.Implementations.Maps.Factories;

public sealed class MapNavigationActionsFactory
{
    private readonly GameObjects _gameObjects;

    public MapNavigationActionsFactory(GameObjects gameObjects)
    {
        _gameObjects = gameObjects;
    }

    public MapNavigationActions Create()
    {
        return new MapNavigationActions(_gameObjects);
    }
}