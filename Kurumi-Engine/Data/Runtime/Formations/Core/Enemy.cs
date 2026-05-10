using Data.Definitions.Formations.Core;

namespace Data.Runtime.Formations.Core;

public sealed class Enemy
{
    private readonly EnemyDefinition _enemyDefinition;

    public IReadOnlyList<EnemyBattleScript> BattleScripts { get; }

    internal Enemy(EnemyDefinition enemyDefinition, IReadOnlyList<EnemyBattleScript> battleScripts)
    {
        _enemyDefinition = enemyDefinition;
        
        BattleScripts = battleScripts;
    }
}