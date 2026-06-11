// Data.
using Data.Models.Variables;

namespace Game.Scripts.Context.Capabilities.Implementations.Universal.Core;

public sealed class GameStateActionsFactory 
{
    private readonly GameVariables _gameVariables;

    public GameStateActionsFactory(GameVariables gameVariables)
    {
        _gameVariables = gameVariables;
    }

    public GameStateActions Create()
    {
        return new GameStateActions(_gameVariables);
    }
}