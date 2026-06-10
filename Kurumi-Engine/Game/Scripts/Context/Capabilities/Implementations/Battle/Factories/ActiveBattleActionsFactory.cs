// Data.
using Data.Runtime.Formations.Core;

// Game.
using Game.Scripts.Context.Capabilities.Implementations.Battle.Core;

namespace Game.Scripts.Context.Capabilities.Implementations.Battle.Factories;

public sealed class ActiveBattleActionsFactory
{
    public ActiveBattleActionsFactory() {}

    public ActiveBattleActions Create(Formation formation)
    {
        return new ActiveBattleActions(formation);
    }
}