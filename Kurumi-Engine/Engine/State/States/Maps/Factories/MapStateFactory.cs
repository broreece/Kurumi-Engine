// Engine.
using Engine.Context.Core;

using Engine.State.Base;
using Engine.State.States.Maps.Core;

// Game.
using Game.Scripts.Context.Builder.Factories;

namespace Engine.State.States.Maps.Factories;

public sealed class MapStateFactory
{
    // Context.
    private readonly GameContext _gameContext;
    private readonly StateContext _stateContext;

    // Script context builder factory.
    private readonly MapScriptContextBuilderFactory _mapScriptContextBuilderFactory;

    public MapStateFactory(
        GameContext gameContext, 
        StateContext stateContext, 
        MapScriptContextBuilderFactory mapScriptContextBuilderFactory
    )
    {
        _gameContext = gameContext;
        _stateContext = stateContext;

        _mapScriptContextBuilderFactory = mapScriptContextBuilderFactory;
    }

    public MapState Create(string? startingScript)
    {
        return new MapState(_gameContext, _stateContext, _mapScriptContextBuilderFactory, startingScript);
    }
}