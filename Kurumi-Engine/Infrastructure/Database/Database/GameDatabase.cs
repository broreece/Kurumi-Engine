using Data.Definitions.Actors.Core;
using Data.Definitions.Actors.Factories;
using Data.Definitions.Entities.Abilities.Core;
using Data.Definitions.Entities.Abilities.Factories;
using Data.Definitions.Entities.Core;
using Data.Definitions.Entities.Factories;
using Data.Definitions.Entities.Statuses.Core;
using Data.Definitions.Entities.Statuses.Factories;
using Data.Definitions.Formations.Core;
using Data.Definitions.Formations.Factories;
using Data.Definitions.Items.Core;
using Data.Definitions.Items.Factories;
using Data.Definitions.Maps.Core;
using Data.Definitions.Maps.Factories;

using Infrastructure.Database.Base;
using Infrastructure.Database.Factories;
using Infrastructure.Database.Loaders.Abilities;
using Infrastructure.Database.Loaders.Actors;
using Infrastructure.Database.Loaders.Entities;
using Infrastructure.Database.Loaders.Formations;
using Infrastructure.Database.Loaders.Items;
using Infrastructure.Database.Loaders.Names;
using Infrastructure.Database.Loaders.Tiles;
using Infrastructure.Database.Repositories.Core.Abilities;
using Infrastructure.Database.Repositories.Core.Actors;
using Infrastructure.Database.Repositories.Core.Characters;
using Infrastructure.Database.Repositories.Core.Entities;
using Infrastructure.Database.Repositories.Core.Equipment;
using Infrastructure.Database.Repositories.Core.Formations;
using Infrastructure.Database.Repositories.Core.Items;
using Infrastructure.Database.Repositories.Core.Names;
using Infrastructure.Database.Repositories.Core.Statuses;
using Infrastructure.Database.Repositories.Core.Tiles;
using Infrastructure.Database.Services;

namespace Infrastructure.Database.Database;

/// <summary>
/// Contains all the registries for the game objects stored in the database.
/// </summary>
public sealed class GameDatabase 
{
    public GameDatabase() 
    {
        // Create service.
        DatabaseService databaseService = new(Path.Combine(
            AppContext.BaseDirectory,
            "Infrastructure",
            "Database",
            "Database",
            "game_data.db"
        ));

        // Create factories.
        var abilityFactory = new AbilityDefinitionFactory();
        var abilitySetFactory = new NamedDataFactory();
        var actorInfoFactory = new ActorInfoFactory();
        var actorSpriteFactory = new ActorSpriteFactory();
        var characterDefinitionFactory = new CharacterDefinitionFactory();
        var elementNameFactory = new NamedDataFactory();
        var enemyBattleScriptFactory = new EnemyBattleScriptFactory();
        var enemyFactory = new EnemyDefinitionFactory();
        var entityDefinitionFactory = new EntityDefinitionFactory();
        var equipmentFactory = new EquipmentFactory();
        var formationDefinitionFactory = new FormationDefinitionFactory();
        var equipmentSlotNameFactory = new NamedDataFactory();
        var equipmentTypeNameFactory = new NamedDataFactory();
        var itemFactory = new ItemFactory();
        var statusFactory = new StatusDefinitionFactory();
        var statNameFactory = new TwoNamedDataFactory();
        var tileFactory = new TileFactory();

        // Create repos.
        var abilityRepository = new AbilityRepository(databaseService);
        var abilitySetRepository = new AbilitySetRepository(databaseService);
        var actorInfoRepository = new ActorInfoRepository(databaseService);
        var actorPathRepository = new ActorPathRepository(databaseService);
        var actorSpriteRepository = new ActorSpriteRepository(databaseService);
        var characterRepository = new CharacterRepository(databaseService);
        var entityAbilityRepository = new EntityAbilityRepository(databaseService);
        var entityElementRepository = new EntityElementRepository(databaseService);
        var entityRepository = new EntityRepository(databaseService);
        var entityStatRepository = new EntityStatRepository(databaseService);
        var entityStatusRepository = new EntityStatusRepository(databaseService);
        var equipmentAbilityRepository = new EquipmentAbilityRepository(databaseService);
        var equipmentAbilitySetRepository = new EquipmentAbilitySetRepository(databaseService);
        var equipmentElementRepository = new EquipmentElementRepository(databaseService);
        var equipmentRepository = new EquipmentRepository(databaseService);
        var equipmentStatRepository = new EquipmentStatRepository(databaseService);
        var equipmentStatusRepository = new EquipmentStatusRepository(databaseService);
        var enemyBattleScriptRepository = new EnemyBattleScriptRepository(databaseService);
        var enemyRepository = new EnemyRepository(databaseService);
        var formationRepository = new FormationRepository(databaseService);
        var itemPoolRepository = new ItemPoolRepository(databaseService);
        var itemPoolItemRepository = new ItemPoolItemRepository(databaseService);
        var itemRepository = new ItemRepository(databaseService);
        var elementNameRepository = new ElementNameRepository(databaseService);
        var equipmentSlotNameRepository = new EquipmentSlotNameRepository(databaseService);
        var equipmentTypeNameRepository = new EquipmentTypeNameRepository(databaseService);
        var statNameRepository = new StatNameRepository(databaseService);
        var statusAbilityRepository = new StatusAbilityRepository(databaseService);
        var statusAbilitySetRepository = new StatusAbilitySetRepository(databaseService);
        var statusElementRepository = new StatusElementRepository(databaseService);
        var statusRepository = new StatusRepository(databaseService);
        var statusStatRepository = new StatusStatRepository(databaseService);
        var tileRepository = new TileRepository(databaseService);

        // Create loaders.
        var abilityLoader = new AbilityLoader(abilityRepository, abilityFactory);
        var abilitySetLoader = new AbilitySetLoader(abilitySetRepository, abilitySetFactory);
        var actorInfoLoader = new ActorInfoLoader(actorInfoRepository, actorPathRepository, actorInfoFactory);
        var actorSpriteLoader = new ActorSpriteLoader(actorSpriteRepository, actorSpriteFactory);
        var characterLoader = new CharacterLoader(characterRepository, characterDefinitionFactory);
        var elementNameLoader = new ElementNameLoader(elementNameRepository, elementNameFactory);
        var enemyBattleScriptLoader = new EnemyBattleScriptLoader(enemyBattleScriptRepository, enemyBattleScriptFactory);
        var enemyDefinitionLoader = new EnemyDefinitionLoader(enemyRepository, enemyBattleScriptRepository, enemyFactory);

        var entityDefinitionLoader = new EntityDefinitionLoader(
            entityRepository,
            entityAbilityRepository,
            entityElementRepository,
            entityStatRepository,
            entityStatusRepository,
            entityDefinitionFactory);

        var equipmentLoader = new EquipmentLoader(
            equipmentRepository,
            equipmentAbilityRepository,
            equipmentAbilitySetRepository,
            equipmentElementRepository,
            equipmentStatRepository,
            equipmentStatusRepository,
            equipmentFactory);

        var equipmentSlotNameLoader = new EquipmentSlotNameLoader(equipmentSlotNameRepository, equipmentSlotNameFactory);
        var equipmentTypeNameLoader = new EquipmentTypeNameLoader(equipmentTypeNameRepository, equipmentTypeNameFactory);

        var formationDefinitionLoader = new FormationDefinitionLoader(
            formationRepository,
            enemyRepository,
            formationDefinitionFactory);

        var itemLoader = new ItemLoader(itemRepository, itemFactory);

        var statusLoader = new StatusLoader(
            statusRepository,
            statusAbilityRepository,
            statusAbilitySetRepository,
            statusElementRepository,
            statusStatRepository,
            statusFactory);
            
        var statNameLoader = new StatNameLoader(statNameRepository, statNameFactory);
        var tileLoader = new TileLoader(tileRepository, tileFactory);

        AbilityRegistry = new Registry<AbilityDefinition>(
            abilityLoader.LoadAll(),
            ability => ability.Id
        );

        AbilitySetRegistry = new Registry<NamedData>(
            abilitySetLoader.LoadAll(),
            abilitySet => abilitySet.Id
        );

        ActorInfoRegistry = new Registry<ActorInfo>(
            actorInfoLoader.LoadAll(),
            actor => actor.Id
        );

        ActorSpriteRegistry = new Registry<ActorSprite>(
            actorSpriteLoader.LoadAll(),
            actorSprite => actorSprite.Id
        );

        CharacterRegistry = new Registry<CharacterDefinition>(
            characterLoader.LoadAll(),
            character => character.Id
        );

        EnemyBattleScriptRegistry = new Registry<EnemyBattleScript>(
            enemyBattleScriptLoader.LoadAll(),
            script => script.Id
        );

        EnemyDefinitionRegistry = new Registry<EnemyDefinition>(
            enemyDefinitionLoader.LoadAll(),
            enemy => enemy.Id
        );

        ElementNameRegistry = new Registry<NamedData>(
            elementNameLoader.LoadAll(),
            elementName => elementName.Id
        );

        EntityDefinitionRegistry = new Registry<EntityDefinition>(
            entityDefinitionLoader.LoadAll(),
            entityDefinition => entityDefinition.Id
        );

        EquipmentRegistry = new Registry<Equipment>(
            equipmentLoader.LoadAll(),
            equipment => equipment.Id
        );

        EquipmentSlotNameRegistry = new Registry<NamedData>(
            equipmentSlotNameLoader.LoadAll(),
            slot => slot.Id
        );

        EquipmentTypeNameRegistry = new Registry<NamedData>(
            equipmentTypeNameLoader.LoadAll(),
            type => type.Id
        );

        FormationRegistry = new Registry<FormationDefinition>(
            formationDefinitionLoader.LoadAll(),
            formation => formation.Id
        );

        ItemRegistry = new Registry<Item>(
            itemLoader.LoadAll(),
            item => item.Id
        );

        StatusRegistry = new Registry<StatusDefinition>(
            statusLoader.LoadAll(),
            status => status.Id
        );

        StatNameRegistry = new Registry<TwoNamedData>(
            statNameLoader.LoadAll(),
            stat => stat.Id
        );

        // Load a lookup table for stat short name indexes where the stat short name is the key.
        StatShortNameIndex = statNameLoader.LoadStatShortNameIndex();

        TileRegistry = new Registry<Tile>(
            tileLoader.LoadAll(),
            tile => tile.Id
        );
    }

    public Registry<AbilityDefinition> AbilityRegistry { get; }
    public Registry<NamedData> AbilitySetRegistry { get; }
    public Registry<ActorInfo> ActorInfoRegistry { get; }
    public Registry<ActorSprite> ActorSpriteRegistry { get; }
    public Registry<CharacterDefinition> CharacterRegistry { get; }
    public Registry<NamedData> ElementNameRegistry { get; }
    public Registry<EnemyBattleScript> EnemyBattleScriptRegistry { get; }
    public Registry<EnemyDefinition> EnemyDefinitionRegistry { get; }
    public Registry<EntityDefinition> EntityDefinitionRegistry { get; }
    public Registry<Equipment> EquipmentRegistry { get; }
    public Registry<NamedData> EquipmentSlotNameRegistry { get; }
    public Registry<NamedData> EquipmentTypeNameRegistry { get; }
    public Registry<FormationDefinition> FormationRegistry { get; }
    public Registry<Item> ItemRegistry { get; }
    public Registry<TwoNamedData> StatNameRegistry { get; }
    public Registry<StatusDefinition> StatusRegistry { get; }
    public Registry<Tile> TileRegistry { get; }

    public IReadOnlyDictionary<string, int> StatShortNameIndex { get; }
}