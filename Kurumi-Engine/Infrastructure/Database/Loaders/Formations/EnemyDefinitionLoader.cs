using Data.Definitions.Formations.Core;
using Data.Definitions.Formations.Factories;
using Infrastructure.Database.Interfaces;
using Infrastructure.Database.Repositories.Core.Formations;
using Infrastructure.Database.Repositories.Rows.Formations;

namespace Infrastructure.Database.Loaders.Formations;

public sealed class EnemyDefinitionLoader : IDataLoader<EnemyDefinition> 
{
    private readonly EnemyRepository _enemyRepository;
    private readonly EnemyBattleScriptRepository _enemyBattleScriptRepository;
    private readonly EnemyDefinitionFactory _enemyDefinitionFactory;

    public EnemyDefinitionLoader(
        EnemyRepository enemyRepository, 
        EnemyBattleScriptRepository enemyBattleScriptRepository, 
        EnemyDefinitionFactory enemyDefinitionFactory) 
    {
        _enemyRepository = enemyRepository;
        _enemyBattleScriptRepository = enemyBattleScriptRepository;
        _enemyDefinitionFactory = enemyDefinitionFactory;
    }

    public IReadOnlyList<EnemyDefinition> LoadAll() 
    {
        EnemyRow[] rows = _enemyRepository.LoadAll();
        var enemies = new EnemyDefinition[rows.Length];

        // Create a lookup for the actor paths basedon the ID of the actor info.
        ILookup<int, EnemyBattleScriptRow> actorPathLookup = _enemyBattleScriptRepository.LoadAll().ToLookup(
            scriptRow => scriptRow.EnemyFormationEnemyId
        );

        for (var index = 0; index < rows.Length; index ++) 
        {
            var row = rows[index];
            var id = row.Id;

            // Load battle scripts.
            var battleScripts = new List<string>();
            foreach(var scriptRow in actorPathLookup[id]) 
            {
                battleScripts.Add(scriptRow.ScriptName);
            }

            enemies[index] = _enemyDefinitionFactory.Create(
                id,
                row.EnemyId,
                row.XLocation,
                row.YLocation,
                row.MainPart,
                row.OnKillScript,
                battleScripts
            );
        }
        return enemies;
    }
}