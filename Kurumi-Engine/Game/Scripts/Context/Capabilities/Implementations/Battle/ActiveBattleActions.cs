using Data.Runtime.Formations.Core;

using Game.Scripts.Context.Capabilities.Interfaces.Battle;

namespace Game.Scripts.Context.Capabilities.Implementations.Battle;

public sealed class ActiveBattleActions : IActiveBattleActions 
{
    private readonly Formation _formation;

    public ActiveBattleActions(Formation formation)
    {
        _formation = formation;
    }

    public void KillEnemy(int enemyIndex) 
    {
        _formation.GetEntityAt(enemyIndex).CurrentHP = 0;
    }
}