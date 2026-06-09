// Data.
using Data.Definitions.Actors.Base;
using Data.Definitions.Actors.Core;
using Data.Definitions.Entities.Core;
using Data.Definitions.Formations.Core;

using Data.Models.Formations;

using Data.Runtime.Entities.Core;
using Data.Runtime.Entities.Factories;
using Data.Runtime.Formations.Base;
using Data.Runtime.Formations.Core;
using Data.Runtime.Formations.Exceptions;
using Data.Runtime.Maps.Base.Controllers.Base;
using Data.Runtime.Maps.Base.Controllers.Core;
using Data.Runtime.Parties.Core;

// Engine.
using Engine.Systems.Navigation.Core;

// Game.
using Game.Scripts.Library;

// Infrastructure.
using Infrastructure.Database.Base;

namespace Data.Runtime.Formations.Factories;

public sealed class FormationFactory 
{
    // Party used for controllers.
    private readonly Party _party;

    // Map registries.
    private readonly Registry<ActorInfo> _actorInfoRegistry;

    // Battle registries.
    private readonly Registry<EnemyDefinition> _enemyDefinitionRegistry;
    private readonly Registry<EntityDefinition> _entityDefinitionRegistry;

    private readonly EnemyFactory _enemyFactory;
    private readonly EntityFactory _entityFactory;

    private readonly ScriptLibrary _scriptLibrary;

    public FormationFactory
    (
        Party party, 
        Registry<ActorInfo> actorInfoRegsitry, 
        Registry<EnemyDefinition> enemyDefinitionRegistry, 
        Registry<EnemyBattleScript> enemyBattleScriptRegistry, 
        Registry<EntityDefinition> entityDefinitionRegistry, 
        ScriptLibrary scriptLibrary, 
        int agilityIndex
    )
    {
        _party = party;

        _actorInfoRegistry = actorInfoRegsitry;

        _enemyDefinitionRegistry = enemyDefinitionRegistry;
        _entityDefinitionRegistry = entityDefinitionRegistry;

        _scriptLibrary = scriptLibrary;

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
    /// <param name="navigationGrid">The current map in the form of a navigation grid, if it's a map state.</param>
    /// <returns>A new enemy formation runtime object.</returns>
    public Formation Create(FormationDefinition definition, FormationModel model, NavigationGrid? navigationGrid) 
    {
        // If the formation definition and formation model don't match throw an exception.
        if (definition.Enemies.Count != model.Enemies.Count)
        {
            throw new FormationException($"Formation: {definition.Id} has a model with a different number of enemies.");
        }

        // Load actors.
        var defaultActor = _actorInfoRegistry.Get(definition.DefaultActorInfoId);
        var onFoundActor = _actorInfoRegistry.Get(definition.OnFoundActorInfoId);

        // Load actor controllers.
        Controller? defaultController = CreateController(defaultActor, navigationGrid);
        Controller? onFoundController = CreateController(onFoundActor, navigationGrid);

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
            onFoundActor, 
            defaultController, 
            onFoundController, 
            entities, 
            enemies
        ) 
        { 
            StoredEntityData = storedEntityData, 
            Script = defaultActor.ScriptName == null ? null : _scriptLibrary.GetMapScript(defaultActor.ScriptName) 
        };
    }

    /// <summary>
    /// Function that returns a nullable controller based on the passed actor information.
    /// </summary>
    /// <param name="actorInfo">The actor information being used to create the controller.</param>
    /// <param name="navigationGrid">The current map in the form of a navigation grid, if it's a map state.</param>
    /// <returns>A new controller.</returns>
    private Controller? CreateController(ActorInfo actorInfo, NavigationGrid? navigationGrid)
    {
        if (navigationGrid == null)
        {
            return null;
        }
        return actorInfo.Behaviour switch
        {
            (int) ActorBehaviour.DumbTracking => new DumbTrackingController(_party, actorInfo.TrackingRange)
            {
                Interval = actorInfo.MovementSpeed
            },

            (int) ActorBehaviour.FollowsPath => new PathedController(canFinish: false, actorInfo.Path)
            {
                Interval = actorInfo.MovementSpeed
            },

            (int) ActorBehaviour.SmartTracking => new SmartTrackingController(
                _party, navigationGrid, 
                actorInfo.TrackingRange
            )
            {
                Interval = actorInfo.MovementSpeed
            },

            (int) ActorBehaviour.RandomMovement => new RandomController() { Interval = actorInfo.MovementSpeed },

            _ => null,
        };
    }
}