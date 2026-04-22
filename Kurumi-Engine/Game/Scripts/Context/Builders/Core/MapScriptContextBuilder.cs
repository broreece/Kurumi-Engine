using Engine.Context.Core;
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

    public MapScriptContextBuilder(GameContext gameContext) 
    {
        _gameContext = gameContext;
    }

    public ScriptContext BuildScriptContext() 
    {
        var capabilityContainer = new CapabilityContainer();
        var variableTable = new VariableTable();

        // Construct capability container.
        capabilityContainer.SetCapability(typeof(IBattleActions), new BattleActions());
        capabilityContainer.SetCapability(
            typeof(IMapNavigationActions), 
            new MapNavigationActions(_gameContext.GameObjects)
        );
        capabilityContainer.SetCapability(
            typeof(IMovementActions), 
            new MovementActions(_gameContext.GameObjects.CurrentMap)
        );
        capabilityContainer.SetCapability(typeof(IGameStateActions), new GameStateActions());
        capabilityContainer.SetCapability(typeof(IPartyStatusActions), new PartyStatusActions());
        capabilityContainer.SetCapability(typeof(IUIActions), new UIActions());

        return new ScriptContext(capabilityContainer, variableTable);
    }
}