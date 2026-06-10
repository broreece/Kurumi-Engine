// Engine.
using Engine.Context.Containers;

// Game.
using Game.Scripts.Context.Capabilities.Implementations.Maps.Core;

namespace Game.Scripts.Context.Capabilities.Implementations.Maps.Factories;

public sealed class BattleActionsFactory
{
    private readonly GameObjects _gameObjects;

    public BattleActionsFactory(GameObjects gameObjects)
    {
        _gameObjects = gameObjects;
    }

    public BattleActions Create()
    {
        return new BattleActions(_gameObjects);
    }
}