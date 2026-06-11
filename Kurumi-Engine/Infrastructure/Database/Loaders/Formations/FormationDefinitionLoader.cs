// Data.
using Data.Definitions.Formations.Core;
using Data.Definitions.Formations.Factories;

// Infrastructure.
using Infrastructure.Database.Interfaces;
using Infrastructure.Database.Repositories.Core.Formations;
using Infrastructure.Database.Repositories.Rows.Formations;

namespace Infrastructure.Database.Loaders.Formations;

public sealed class FormationDefinitionLoader : IDataLoader<FormationDefinition> 
{
    private readonly FormationRepository _formationRepository;
    private readonly EnemyRepository _enemyRepository;

    private readonly FormationDefinitionFactory _formationDefinitionFactory;

    // Index.
    private readonly Dictionary<string, IList<int>> _mapFormationsIndex = [];

    public FormationDefinitionLoader(
        FormationRepository formationRepository, 
        EnemyRepository enemyRepository, 
        FormationDefinitionFactory formationDefinitionFactory
    ) 
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
            var mapName = row.MapName;

            var enemies = new List<int>();
            foreach (EnemyRow enemyRow in enemyLookup[id]) 
            {
                enemies.Add(enemyRow.EnemyId);
            }

            formations[index] = _formationDefinitionFactory.Create(
                id, 
                mapName, 
                row.ReturnX, 
                row.ReturnY, 
                row.SearchTimer, 
                row.ItemPoolId, 
                row.OnFoundActorInfoId, 
                row.DefaultActorInfoId, 
                row.BackgroundMusicName, 
                row.BackgroundArtName, 
                row.OnLoseScript, 
                row.OnWinScript, 
                enemies
            );

            // Update index.
            if (_mapFormationsIndex.TryGetValue(mapName, out IList<int>? value))
            {
                value.Add(id);
            }
            else
            {
                _mapFormationsIndex[mapName] = [id];
            }
        }

        return formations;
    }

    /// <summary>
    /// Lookup index for which enemy formations appear on which maps.
    /// </summary>
    /// <returns>A dictionary storing the list of formations IDs on each map.</returns>
    public IReadOnlyDictionary<string, IReadOnlyList<int>> LoadMapFormationsIndex()
    {
        return _mapFormationsIndex.ToDictionary(
            x => x.Key,
            x => (IReadOnlyList<int>)x.Value
        );
    }
}