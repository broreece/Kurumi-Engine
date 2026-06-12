// Engine.
using Engine.Context.Core;

// Game.
using Game.Scripts.Context.Builder.Base;
using Game.Scripts.Context.Capabilities.Base;
using Game.Scripts.Context.Capabilities.Implementations.Maps.Factories;
using Game.Scripts.Context.Capabilities.Implementations.Universal.Core;
using Game.Scripts.Context.Capabilities.Interfaces.Map;
using Game.Scripts.Context.Capabilities.Interfaces.Universal;
using Game.Scripts.Context.Core;
using Game.Scripts.Context.Variables.Core;

namespace Game.Scripts.Context.Builder.Core;

public sealed class MapScriptContextBuilder : IScriptContextBuilder 
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

    internal MapScriptContextBuilder(
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

    public ScriptContext BuildScriptContext() 
    {
        var capabilityContainer = new CapabilityContainer();
        var variableTable = new VariableTable();

        var gameObjects = _gameContext.GameObjects;

        // Construct capability container.
        capabilityContainer.SetCapability(typeof(IBattleActions), _battleActionsFactory.Create());

        capabilityContainer.SetCapability(
            typeof(IMapNavigationActions), 
            _mapNavigationActionsFactory.Create()
        );

        capabilityContainer.SetCapability(
            typeof(IMovementActions), 
            _movementActionsFactory.Create(gameObjects.CurrentMap)
        );

        capabilityContainer.SetCapability(typeof(IGameStateActions), _gameStateActionsFactory.Create());

        capabilityContainer.SetCapability(typeof(IItemActions), _itemActionsFactory.Create());

        capabilityContainer.SetCapability(typeof(IPartyStatusActions), _partyStatusActionsFactory.Create());
        
        capabilityContainer.SetCapability(typeof(IUIActions), _uiActionsFactory.Create());

        return new ScriptContext(capabilityContainer, variableTable);
    }
}