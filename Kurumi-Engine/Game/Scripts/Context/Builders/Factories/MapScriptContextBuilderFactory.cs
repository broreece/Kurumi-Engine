// Engine.
using Engine.Context.Core;

// Game.
using Game.Scripts.Context.Builder.Core;
using Game.Scripts.Context.Capabilities.Implementations.Universal.Core;

namespace Game.Scripts.Context.Builder.Factories;

public sealed class MapScriptContextBuilderFactory
{
    // Contexts.
    private readonly GameContext _gameContext;

    // Factories.
    private readonly UIActionsFactory _uiActionsFactory;

    public MapScriptContextBuilderFactory(
        GameContext gameContext, 
        UIActionsFactory uiActionsFactory
    )
    {
        _gameContext = gameContext;

        _uiActionsFactory = uiActionsFactory;
    }

    public MapScriptContextBuilder Create()
    {
        return new MapScriptContextBuilder(
            _gameContext, 
            _uiActionsFactory
        );
    }
}