using Data.Definitions.Actors.Core;
using Data.Definitions.Entities.Core;
using Data.Definitions.Formations.Core;
using Data.Models.Formations;
using Data.Runtime.Entities.Core;
using Data.Runtime.Entities.Factories;
using Data.Runtime.Formations.Base;
using Data.Runtime.Formations.Core;
using Data.Runtime.Formations.Exceptions;

using Infrastructure.Database.Base;

namespace Data.Runtime.Formations.Factories;

public sealed class FormationFactory 
{
    // Map registries.
    private readonly Registry<ActorInfo> _actorInfoRegistry;

    // Battle registries.
    private readonly Registry<EnemyDefinition> _enemyDefinitionRegistry;
    private readonly Registry<EntityDefinition> _entityDefinitionRegistry;

    private readonly EnemyFactory _enemyFactory;
    private readonly EntityFactory _entityFactory;

    public FormationFactory
    (
        Registry<ActorInfo> actorInfoRegsitry, 
        Registry<EnemyDefinition> enemyDefinitionRegistry,
        Registry<EnemyBattleScript> enemyBattleScriptRegistry,
        Registry<EntityDefinition> entityDefinitionRegistry,
        int agilityIndex
    )
    {
        _actorInfoRegistry = actorInfoRegsitry;

        _enemyDefinitionRegistry = enemyDefinitionRegistry;
        _entityDefinitionRegistry = entityDefinitionRegistry;

        _enemyFactory = new EnemyFactory(enemyBattleScriptRegistry);
        _entityFactory = new EntityFactory(agilityIndex);
    }

    /// <summary>
    /// Create list of runtime entities by cross referencing the enemies, from the definition, then the 'EnemyID' 
    /// field from the enemy registry. Stores in the same list the coordinates of where to draw each enemy as well.
    /// Also loads the actor definitions of the enemy formation for their default state and when they spot the party.
    /// </summary>
    /// <param name="definition">The enemy formation definition.</param>
    /// <param name="model">The enemy formation model.</param>
    /// <returns>A new enemy formation runtime object.</returns>
    public Formation Create(FormationDefinition definition, FormationModel model) 
    {
        // If the formation definition and formation model don't match throw an exception.
        if (definition.Enemies.Count != model.Enemies.Count)
        {
            throw new FormationException($"Formation: {definition.Id} has a model with a different number of enemies.");
        }

        // Load actors.
        var defaultActor = _actorInfoRegistry.Get(definition.DefaultActorInfoId);
        var onFindActor = _actorInfoRegistry.Get(definition.OnFoundActorInfoId);

        // Load entities and enemies.
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
        
        return new Formation(
            definition, 
            model, 
            defaultActor, 
            onFindActor, 
            entities, 
            enemies
        ) { StoredEntityData = storedEntityData };
    }
}