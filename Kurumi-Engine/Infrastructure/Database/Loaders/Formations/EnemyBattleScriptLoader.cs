using Data.Definitions.Formations.Core;
using Data.Definitions.Formations.Factories;
using Infrastructure.Database.Interfaces;
using Infrastructure.Database.Repositories.Core.Formations;
using Infrastructure.Database.Repositories.Rows.Formations;

namespace Infrastructure.Database.Loaders.Formations;

public sealed class EnemyBattleScriptLoader : IDataLoader<EnemyBattleScript> 
{
    private readonly EnemyBattleScriptRepository _enemyBattleScriptRepository;
    private readonly EnemyBattleScriptFactory _enemyBattleScriptFactory;

    public EnemyBattleScriptLoader(
        EnemyBattleScriptRepository enemyBattleScriptRepository, 
        EnemyBattleScriptFactory enemyBattleScriptFactory) 
    {
        _enemyBattleScriptRepository = enemyBattleScriptRepository;
        _enemyBattleScriptFactory = enemyBattleScriptFactory;
    }

    public IReadOnlyList<EnemyBattleScript> LoadAll() 
    {
        EnemyBattleScriptRow[] rows = _enemyBattleScriptRepository.LoadAll();
        var enemyBattleScripts = new EnemyBattleScript[rows.Length];
        for (var index = 0; index < rows.Length; index ++) 
        {
            var row = rows[index];
            enemyBattleScripts[index] = _enemyBattleScriptFactory.Create(
                row.Id,
                row.Target,
                row.StartTurn,
                row.Frequency,
                row.ScriptName
            );
        }
        return enemyBattleScripts;
    }
}