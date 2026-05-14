using Data.Definitions.Formations.Core;
using Data.Runtime.Formations.Core;

using Infrastructure.Database.Base;

namespace Data.Runtime.Formations.Factories;

public sealed class EnemyFactory 
{
    private readonly Registry<EnemyBattleScript> _enemyBattleScriptRegistry;
    
    public EnemyFactory(Registry<EnemyBattleScript> enemyBattleScriptRegistry)
    {
        _enemyBattleScriptRegistry = enemyBattleScriptRegistry;
    }

    public Enemy Create(EnemyDefinition enemyDefinition)
    {
        var enemyBattleScripts = new List<EnemyBattleScript>();
        foreach (var battleScriptId in enemyDefinition.BattleScripts)
        {
            enemyBattleScripts.Add(_enemyBattleScriptRegistry.Get(battleScriptId));
        }
        return new Enemy(enemyDefinition, enemyBattleScripts);
    }
}