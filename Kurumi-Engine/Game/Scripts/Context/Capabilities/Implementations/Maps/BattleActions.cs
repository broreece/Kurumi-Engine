// Data.
using Data.Runtime.Formations.Base;

// Engine.
using Engine.Context.Containers;

// Game.
using Game.Scripts.Context.Capabilities.Interfaces.Map;

namespace Game.Scripts.Context.Capabilities.Implementations.Maps;

public sealed class BattleActions : IBattleActions 
{
    private readonly GameObjects _gameObjects;

    public BattleActions(GameObjects gameObjects) 
    {
        _gameObjects = gameObjects;
    }

    public void StartBattle(BattleStartRequest battleStartRequest) 
    {
        _gameObjects.BattleStartRequest = battleStartRequest;
    }
}