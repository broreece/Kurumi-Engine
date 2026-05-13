using Data.Models.Variables;

using Game.Scripts.Context.Capabilities.Interfaces.Universal;

namespace Game.Scripts.Context.Capabilities.Implementations.Universal;

public sealed class GameStateActions : IGameStateActions 
{
    private readonly GameVariables _gameVariables;

    public GameStateActions(GameVariables gameVariables)
    {
        _gameVariables = gameVariables;
    }

    public void ChangeFlag(string flagKey, bool newValue) 
    {
        _gameVariables.Flags[flagKey] = newValue;
    }

    public bool GetGameFlag(string flagKey) 
    {
        if (_gameVariables.Flags.TryGetValue(flagKey, out var returnValue))
        {
            return returnValue;
        }
        // TODO: Throw custom exception here.
        throw new Exception("Custom exception here.");
    }
}