// Engine.
using Engine.Context.Core;

using Engine.State.Base;
using Engine.State.States.Maps.Core;

namespace Engine.State.States.Maps.Factories;

public sealed class MapStateFactory
{
    // Context.
    private readonly GameContext _gameContext;
    private readonly StateContext _stateContext;

    public MapStateFactory(GameContext gameContext, StateContext stateContext)
    {
        _gameContext = gameContext;
        _stateContext = stateContext;
    }

    public MapState Create(string? startingScript)
    {
        return new MapState(_gameContext, _stateContext, startingScript);
    }
}