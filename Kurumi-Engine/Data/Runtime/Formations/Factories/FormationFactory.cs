using Data.Definitions.Entities.Core;
using Data.Definitions.Formations.Core;
using Data.Models.Formations;
using Data.Runtime.Entities.Core;
using Data.Runtime.Formations.Core;

using Infrastructure.Database.Base;

namespace Data.Runtime.Formations.Factories;

public sealed class FormationFactory 
{
    private readonly Registry<EnemyDefinition> _enemyDefinitionRegistry;
    private readonly Registry<EntityDefinition> _entityDefinitionRegistry;

    public FormationFactory
    (
        Registry<EnemyDefinition> enemyDefinitionRegistry,
        Registry<EntityDefinition> entityDefinitionRegistry
    )
    {
        _enemyDefinitionRegistry = enemyDefinitionRegistry;
        _entityDefinitionRegistry = entityDefinitionRegistry;
    }

    public Formation Create(FormationDefinition definition, FormationModel model) 
    {
        // Create list of runtime entities by cross referencing the enemies, from the definition, then the 'EnemyID' 
        // field from the enemy registry.
        // TODO: (DKE-01) Exception here if model and definition have different numbers of enemies.
        var entities = new List<Entity>();
        for (var enemyIndex = 0; enemyIndex < model.Enemies.Count; enemyIndex ++) 
        {
            var enemyId = definition.Enemies[enemyIndex];
            var enemyDefinition = _enemyDefinitionRegistry.Get(enemyId);
            var entityId = enemyDefinition.Id;
            var entity = _entityDefinitionRegistry.Get(entityId);
            entities.Add(new Entity(entity, model.Enemies[enemyIndex]) { CurrentHP = entity.MaxHp });
        }
        
        return new Formation(definition, model, entities);
    }
}