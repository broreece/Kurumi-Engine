using Data.Models.Variables;

using Game.Scripts.Context.Capabilities.Exceptions;
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
        return _gameVariables.Flags.TryGetValue(flagKey, out var returnValue) ? returnValue :
            throw new MissingGameFlagException($"Game flag: {flagKey} could not be found.");
    }
}