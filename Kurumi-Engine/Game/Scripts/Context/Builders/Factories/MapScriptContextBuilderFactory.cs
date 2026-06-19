// Engine.
using Engine.Context.Core;

// Game.
using Game.Scripts.Context.Builder.Core;
using Game.Scripts.Context.Capabilities.Implementations.Maps.Factories;
using Game.Scripts.Context.Capabilities.Implementations.Universal.Core;

namespace Game.Scripts.Context.Builder.Factories;

public sealed class MapScriptContextBuilderFactory
{
    // Contexts.
    private readonly GameContext _gameContext;

    // Factories.
    private readonly BattleActionsFactory _battleActionsFactory;
    private readonly MapNavigationActionsFactory _mapNavigationActionsFactory;
    private readonly MovementActionsFactory _movementActionsFactory;    

    private readonly GameStateActionsFactory _gameStateActionsFactory;
    private readonly ItemActionsFactory _itemActionsFactory;
    private readonly PartyStatusActionsFactory _partyStatusActionsFactory;
    private readonly UIActionsFactory _uiActionsFactory;

    public MapScriptContextBuilderFactory(
        GameContext gameContext, 
        BattleActionsFactory battleActionsFactory, 
        MapNavigationActionsFactory mapNavigationActionsFactory, 
        MovementActionsFactory movementActionsFactory, 
        GameStateActionsFactory gameStateActionsFactory, 
        ItemActionsFactory itemActionsFactory, 
        PartyStatusActionsFactory partyStatusActionsFactory, 
        UIActionsFactory uiActionsFactory
    )
    {
        _gameContext = gameContext;

        _battleActionsFactory = battleActionsFactory;
        _mapNavigationActionsFactory = mapNavigationActionsFactory;
        _movementActionsFactory = movementActionsFactory;

        _gameStateActionsFactory = gameStateActionsFactory;
        _itemActionsFactory = itemActionsFactory;
        _partyStatusActionsFactory = partyStatusActionsFactory;
        _uiActionsFactory = uiActionsFactory;
    }

    public MapScriptContextBuilder Create()
    {
        return new MapScriptContextBuilder(
            _gameContext, 
            _battleActionsFactory, 
            _mapNavigationActionsFactory, 
            _movementActionsFactory, 
            _gameStateActionsFactory, 
            _itemActionsFactory, 
            _partyStatusActionsFactory, 
            _uiActionsFactory
        );
    }
}