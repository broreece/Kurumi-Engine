using Engine.Context.Containers;
using Engine.Context.Core;
using Engine.State.Base;

using Game.Scripts.Context.Builder.Base;
using Game.Scripts.Context.Capabilities.Base;
using Game.Scripts.Context.Capabilities.Implementations.Maps;
using Game.Scripts.Context.Capabilities.Implementations.Universal;
using Game.Scripts.Context.Capabilities.Interfaces.Map;
using Game.Scripts.Context.Capabilities.Interfaces.Universal;
using Game.Scripts.Context.Core;
using Game.Scripts.Context.Variables.Core;

namespace Game.Scripts.Context.Builder.Core;

public sealed class MapScriptContextBuilder : IScriptContextBuilder 
{
    private readonly GameContext _gameContext;
    private readonly StateContext _stateContext;

    public MapScriptContextBuilder(GameContext gameContext, StateContext stateContext) 
    {
        _gameContext = gameContext;
        _stateContext = stateContext;
    }

    public ScriptContext BuildScriptContext() 
    {
        var capabilityContainer = new CapabilityContainer();
        var variableTable = new VariableTable();
        var gameObjects = _gameContext.GameObjects;
        var gameData = _gameContext.GameData;

        // Construct capability container.
        capabilityContainer.SetCapability(typeof(IBattleActions), new BattleActions(gameObjects));
        capabilityContainer.SetCapability(
            typeof(IMapNavigationActions), 
            new MapNavigationActions(gameObjects)
        );
        capabilityContainer.SetCapability(
            typeof(IMovementActions), 
            new MovementActions(gameObjects.CurrentMap, gameObjects.Party)
        );
        capabilityContainer.SetCapability(typeof(IGameStateActions), new GameStateActions(
            gameObjects.SaveData.GameVariables
        ));
        capabilityContainer.SetCapability(typeof(IPartyStatusActions), new PartyStatusActions(
            gameObjects.Party,
            gameData.GameDatabase.StatusRegistry
        ));
        capabilityContainer.SetCapability(typeof(IUIActions), new UIActions(
            _stateContext,
            gameData.AssetRegistry, 
            gameData.ConfigProvider
        ));

        return new ScriptContext(capabilityContainer, variableTable);
    }
}