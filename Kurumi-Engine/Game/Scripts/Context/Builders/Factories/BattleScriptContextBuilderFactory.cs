// Data.
using Data.Runtime.Formations.Core;
using Data.Runtime.Parties.Core;

// Engine.
using Engine.Context.Core;

// Game.
using Game.Scripts.Context.Builder.Core;
using Game.Scripts.Context.Capabilities.Implementations.Universal.Core;

namespace Game.Scripts.Context.Builder.Factories;

public sealed class BattleScriptContextBuilderFactory
{
    // Contexts.
    private readonly GameContext _gameContext;

    // Party.
    private readonly Party _party;

    // Factories.
    private readonly UIActionsFactory _uiActionsFactory;

    public BattleScriptContextBuilderFactory(
        GameContext gameContext, 
        Party party, 
        UIActionsFactory uiActionsFactory
    )
    {
        _gameContext = gameContext;

        _party = party;
        
        _uiActionsFactory = uiActionsFactory;
    }

    public BattleScriptContextBuilder Create(Formation formation)
    {
        return new BattleScriptContextBuilder(
            _gameContext, 
            _party, 
            _uiActionsFactory, 
            formation
        );
    }
}