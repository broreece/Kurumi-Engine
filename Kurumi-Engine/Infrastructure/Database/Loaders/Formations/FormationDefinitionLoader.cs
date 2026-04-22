using Data.Definitions.Formations.Core;
using Data.Definitions.Formations.Factories;
using Infrastructure.Database.Interfaces;
using Infrastructure.Database.Repositories.Core.Formations;
using Infrastructure.Database.Repositories.Rows.Formations;

namespace Infrastructure.Database.Loaders.Formations;

public sealed class FormationDefinitionLoader : IDataLoader<FormationDefinition> 
{
    private readonly FormationRepository _formationRepository;
    private readonly EnemyRepository _enemyRepository;
    private readonly FormationDefinitionFactory _formationDefinitionFactory;

    public FormationDefinitionLoader(
        FormationRepository formationRepository, 
        EnemyRepository enemyRepository, 
        FormationDefinitionFactory formationDefinitionFactory) 
    {
        _formationRepository = formationRepository;
        _enemyRepository = enemyRepository;
        _formationDefinitionFactory = formationDefinitionFactory;
    }

    public IReadOnlyList<FormationDefinition> LoadAll() 
    {
        FormationRow[] rows = _formationRepository.LoadAll();
        var formations = new FormationDefinition[rows.Length];

        // Create a lookup for the enemies based on the enemy formation ID.
        ILookup<int, EnemyRow> enemyLookup = _enemyRepository.LoadAll().ToLookup(
            enemyRow => enemyRow.EnemyFormationId
        );

        for (var index = 0; index < rows.Length; index ++) 
        {
            var row = rows[index];

            // Load list of enemies.
            var id = row.Id;
            var enemies = new List<int>();
            foreach (EnemyRow enemyRow in enemyLookup[id]) 
            {
                enemies.Add(enemyRow.EnemyId);
            }

            formations[index] = _formationDefinitionFactory.Create(
                id,
                row.ReturnX,
                row.ReturnY,
                row.SearchTimer,
                row.ItemPoolId,
                row.OnFoundActorInfoId,
                row.DefaultActorInfoId,
                row.MapName,
                row.OnLoseScript,
                row.OnWinScript,
                enemies
            );
        }
        return formations;
    }
}