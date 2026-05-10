using Data.Definitions.Entities.Core;
using Data.Definitions.Formations.Core;
using Data.Models.Formations;
using Data.Runtime.Entities.Core;
using Data.Runtime.Entities.Factories;
using Data.Runtime.Formations.Base;
using Data.Runtime.Formations.Core;

using Infrastructure.Database.Base;

namespace Data.Runtime.Formations.Factories;

public sealed class FormationFactory 
{
    private readonly Registry<EnemyDefinition> _enemyDefinitionRegistry;
    private readonly Registry<EntityDefinition> _entityDefinitionRegistry;

    private readonly EnemyFactory _enemyFactory;
    private readonly EntityFactory _entityFactory;

    public FormationFactory
    (
        Registry<EnemyDefinition> enemyDefinitionRegistry,
        Registry<EnemyBattleScript> enemyBattleScriptRegistry,
        Registry<EntityDefinition> entityDefinitionRegistry
    )
    {
        _enemyDefinitionRegistry = enemyDefinitionRegistry;
        _entityDefinitionRegistry = entityDefinitionRegistry;

        _enemyFactory = new EnemyFactory(enemyBattleScriptRegistry);
        _entityFactory = new EntityFactory();
    }

    /// <summary>
    /// Create list of runtime entities by cross referencing the enemies, from the definition, then the 'EnemyID' 
    /// field from the enemy registry. Stores in the same list the coordinates of where to draw each enemy as well.
    /// </summary>
    /// <param name="definition">The enemy formation definition.</param>
    /// <param name="model">The enemy formation model.</param>
    /// <returns>A new enemy formation runtime object.</returns>
    public Formation Create(FormationDefinition definition, FormationModel model) 
    {
        // TODO: (DKE-01) Exception here if model and definition have different numbers of enemies.
        var storedEntityData = new List<StoredEntityData>();
        var entities = new List<Entity>();
        var enemies = new List<Enemy>();
        for (var enemyIndex = 0; enemyIndex < model.Enemies.Count; enemyIndex ++) 
        {
            var enemyId = definition.Enemies[enemyIndex];
            var enemyDefinition = _enemyDefinitionRegistry.Get(enemyId);

            // Load and store entity.
            var entityId = enemyDefinition.Id;
            var entityDefinition = _entityDefinitionRegistry.Get(entityId);
            var entity = _entityFactory.Create(entityDefinition, model.Enemies[enemyIndex], entityDefinition.MaxHp);
            enemies.Add(_enemyFactory.Create(enemyDefinition));
            entities.Add(entity);
            storedEntityData.Add(new StoredEntityData() 
            { 
                Entity = entity, 
                XLocation = enemyDefinition.XLocation, 
                YLocation = enemyDefinition.YLocation
            });
        }
        
        return new Formation(definition, model, entities, enemies) { StoredEntityData = storedEntityData };
    }
}