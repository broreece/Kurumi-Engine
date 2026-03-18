namespace Database.Core;

using Database.Exceptions;
using Game.Entities.Enemy;
using Game.Entities.PlayableCharacter;
using Game.Entities.Skills;
using Game.Entities.Status;
using Game.Items;
using Game.Map.Actors.Base;
using Game.Map.Tiles;
using Registry.Actors;
using Registry.Items;
using Registry.Skills;
using Save.Interfaces;
using Microsoft.Data.Sqlite;

/// <summary>
/// Database class, contains functions that allow querying from the database.
/// </summary>
public sealed class DatabaseManager : ICharacterDataLoader {
    /// <summary>
    /// Constructor for the database, loads the database connection.
    /// </summary>
    public DatabaseManager() {
        string databasePath = Path.Combine(
            AppContext.BaseDirectory,
            "Database",
            "Schema",
            "game_data.db"
        );
        connection = new($"Data Source={databasePath}");
    }

    /// <summary>
    /// Function that loads all tile objects in the database.
    /// </summary>
    /// <returns>The tile objects stored in the database.</returns>
    public TileObject[] LoadTileObjects() {
        object[,] data = Load("tile_objects");
        int results = data.GetLength(0);
        TileObject[] tileObjects = new TileObject[results];
        for (int row = 0; row < results; row ++) {
            tileObjects[row] = new((int) (long) data[row, 1], (long) data[row, 2] == 1, (long) data[row, 3] == 1);
        }
        return tileObjects;
    }

    /// <summary>
    /// Function that loads all element names in the database.
    /// </summary>
    /// <returns>The element names stored in the database.</returns>
    public string[] LoadElementNames() {
        return LoadStringNames("elements");
    }

    /// <summary>
    /// Function that loads all equipment slots names in the database.
    /// </summary>
    /// <returns>The equipment slot names stored in the database.</returns>
    public string[] LoadEquipmentSlotNames() {
        return LoadStringNames("equipment_slots");
    }

    /// <summary>
    /// Function that loads all equipment types names in the database.
    /// </summary>
    /// <returns>The equipment types names stored in the database.</returns>
    public string[] LoadEquipmentTypeNames() {
        return LoadStringNames("equipment_types");
    }

    /// <summary>
    /// Function that loads all items in the database.
    /// </summary>
    /// <returns>The items in the database.</returns>
    public Item[] LoadItems() {
        object[,] data = Load("items");
        int results = data.GetLength(0);
        Item[] items = new Item[results];
        for (int row = 0; row < results; row ++) {
            int id = Convert.ToInt32((long) data[row, 0]);
            string name = (string) data[row, 1];
            string desc = (string) data[row, 2];
            string effect = new((string) data[row, 3]);
            bool usableInBattle = Convert.ToInt32((long) data[row, 4]) == 1;
            bool usableInMenu = Convert.ToInt32((long) data[row, 5]) == 1;
            bool targetsParty = Convert.ToInt32((long) data[row, 6]) == 1;
            bool targetsEnemies = Convert.ToInt32((long) data[row, 7]) == 1;
            bool targetsAll = Convert.ToInt32((long) data[row, 8]) == 1;
            bool consumeOnUse = Convert.ToInt32((long) data[row, 9]) == 1;
            int spriteId = Convert.ToInt32((long) data[row, 10]);
            int price = Convert.ToInt32((long)data[row, 11]);
            int weight = Convert.ToInt32((long) data[row, 12]);
            items[row] = new(id, name, desc, effect, usableInBattle, usableInMenu, targetsParty, targetsEnemies, targetsAll,
                consumeOnUse, spriteId, price, weight);
        }
        return items;
    }

    /// <summary>
    /// Function that loads all equipment in the database.
    /// </summary>
    /// <param name="itemRegistry">The item registry object.</param>
    /// <param name="skillRegistry">The skill registry object.</param>
    /// <param name="abilityRegistry">The ability registry object.</param>
    /// <returns>The equipment stored in the database.</returns>
    public Equipment[] LoadEquipment(ItemRegistry itemRegistry, SkillRegistry skillRegistry, AbilityRegistry abilityRegistry) {
        object[,] data = Load("equipment");
        int results = data.GetLength(0);
        Equipment[] equipment = new Equipment[results];
        for (int row = 0; row < results; row ++) {
            int id = (int) (long) data[row, 0];
            Item item = itemRegistry.GetItem((int) (long) data[row, 1] - 1);
            int equipmentType = (int) (long) data[row, 2];
            int equipmentSlot = (int) (long) data[row, 3];
            int attack = (int) (long) data[row, 4];
            int defence = (int) (long) data[row, 5];
            int magicAttack = (int) (long) data[row, 6];
            int magicDefence = (int) (long) data[row, 7];
            int accuracy = (int) (long) data[row, 8];
            int evasion = (int) (long) data[row, 9];
            string effect = new((string) data[row, 10]);
            int turnEffectSpriteId = (int) (long) data[row, 11];
            object[,] abilities = Load("equipment_abilities", ["equipment_id"], 
                [id.ToString()], "equipment_id");
            object[,] skills = Load("equipment_skills", ["equipment_id"], 
                [id.ToString()], "equipment_id");
            object[,] statData = Load("equipment_stats", ["equipment_id"], 
                [id.ToString()], "equipment_id");
            object[,] elementData = Load("equipment_elements", ["equipment_id"], 
                [id.ToString()], "equipment_id");
            int abilityResults = abilities.GetLength(0);
            int skillResults = skills.GetLength(0);
            int statResults = statData.GetLength(0);
            int elementResults = elementData.GetLength(0);
            int[] stats = new int[statResults];
            int[] elements = new int[elementResults];
            List<Skill> sealedSkills = [];
            List<Ability> sealedAbilities = [];
            List<Ability> addedAbilities = [];
            // Load sealed skills.
            for (int skillRow = 0; skillRow < skillResults; skillRow ++) {
                sealedSkills.Add(skillRegistry.GetSkill((int) (long) skills[skillRow, 1]));
            }
            // Load sealed and added abilities.
            for (int abilityRow = 0; abilityRow < abilityResults; abilityRow ++) {
                bool isSealed = (long) abilities[abilityRow, 2] == 1;
                if (isSealed) {
                    sealedAbilities.Add(abilityRegistry.GetAbility((int) (long) abilities[abilityRow, 1] - 1));
                }
                else {
                    addedAbilities.Add(abilityRegistry.GetAbility((int) (long) abilities[abilityRow, 1] - 1));
                }
            }
            // Load stats.
            for (int statRow = 0; statRow < statResults; statRow ++) {
                int statId = Convert.ToInt32((long) statData[statRow, 0]);
                stats[statId - 1] = Convert.ToInt32((long) statData[statRow, 2]);
            }
            // Load elemental resistances.
            for (int elementRow = 0; elementRow < elementResults; elementRow ++) {
                int elementId = Convert.ToInt32((long) elementData[elementRow, 1]);
                elements[elementId - 1] = Convert.ToInt32((long)elementData[elementRow, 2]);
            }
            equipment[row] = new(id, turnEffectSpriteId, accuracy, evasion, equipmentSlot, equipmentType, attack, magicAttack,
                defence, magicDefence, stats, elements, effect, sealedSkills, sealedAbilities, addedAbilities, item);
        }
        return equipment;
    }

    /// <summary>
    /// Function that loads all statuses in the database.
    /// </summary>
    /// <param name="skillRegistry">The skill registry object.</param>
    /// <param name="abilityRegistry">The ability registry object.</param>
    /// <returns>The statuses stored in the database.</returns>
    public Status[] LoadStatuses(SkillRegistry skillRegistry, AbilityRegistry abilityRegistry) {
        object[,] data = Load("statuses");
        int results = data.GetLength(0);
        Status[] statuses = new Status[results];
        for (int row = 0; row < results; row ++) {
            int id = (int) (long) data[row, 0];
            string name = (string) data[row, 1];
            string desc = (string) data[row, 2];
            int spriteId = (int) (long) data[row, 3];
            int priority = (int) (long) data[row, 4];
            int accuracyModifier = (int) (long) data[row, 5];
            int evasionModifier = (int) (long) data[row, 6];
            int maxTurns = (int) (long) data[row, 7];
            bool cureAtEnd = Convert.ToInt32((long) data[row, 8]) == 1;
            bool canAct = Convert.ToInt32((long) data[row, 9]) == 1;
            int turnEffectSpriteId = (int) (long) data[row, 10];
            string turnScript = new((string) data[row, 11]);

            object[,] abilities = Load("statuses_abilities", ["status_id"], 
                [id.ToString()], "ability_id");
            object[,] skills = Load("statuses_skills", ["status_id"], 
                [id.ToString()], "skill_id");
            object[,] statData = Load("statuses_stats", ["status_id"], 
                [id.ToString()], "stat_id");
            object[,] elementData = Load("statuses_elements", ["status_id"], 
                [id.ToString()], "element_id");
            int abilityResults = abilities.GetLength(0);
            int skillResults = skills.GetLength(0);
            int statResults = statData.GetLength(0);
            int elementResults = elementData.GetLength(0);
            int[] statModifiers = new int[statResults];
            int[] elementModifiers = new int[elementResults];
            List<Skill> sealedSkills = [];
            List<Ability> sealedAbilities = [];
            List<Ability> addedAbilities = [];
            // Load status skills.
            for (int skillRow = 0; skillRow < skillResults; skillRow ++) {
                sealedSkills.Add(skillRegistry.GetSkill((int) (long) data[row, 1]));
            }
            // Load sealed and added abilities.
            for (int abilityRow = 0; abilityRow < abilityResults; abilityRow ++) {
                bool isSealed = (long) data[row, 2] == 1;
                if (isSealed) {
                    sealedAbilities.Add(abilityRegistry.GetAbility((int) (long) data[row, 1]));
                }
                else {
                    addedAbilities.Add(abilityRegistry.GetAbility((int) (long) data[row, 1]));
                }
            }
            // Load stats.
            for (int statRow = 0; statRow < statResults; statRow ++) {
                int statId = Convert.ToInt32((long) statData[statRow, 0]);
                statModifiers[statId - 1] = Convert.ToInt32((long) statData[statRow, 2]);
            }
            // Load elemental resistances.
            for (int elementRow = 0; elementRow < elementResults; elementRow ++) {
                int elementId = Convert.ToInt32((long) elementData[elementRow, 1]);
                elementModifiers[elementId - 1] = Convert.ToInt32((long)elementData[elementRow, 2]);
            }

            statuses[row] = new Status(name, desc, spriteId, id, turnEffectSpriteId, maxTurns, priority, accuracyModifier,
                evasionModifier, canAct, cureAtEnd, statModifiers, elementModifiers, turnScript, sealedSkills, sealedAbilities,
                addedAbilities);
        }
        return statuses;
    }

    /// <summary>
    /// Function that loads all abilities in the database.
    /// </summary>
    /// <returns>The abilities stored in the database.</returns>
    public Ability[] LoadAbilities() {
        object[,] data = Load("abilities");
        int results = data.GetLength(0);
        Ability[] abilities = new Ability[results];
        for (int row = 0; row < results; row ++) {
            abilities[row] = new((int) (long) data[row, 0], (string) data[row, 1], (string) data[row, 2], 
                (string) data[row, 3], (int) (long) data[row, 4], (int) (long) data[row, 5], 
                (long) data[row, 6] == 1, (int) (long) data[row, 7]);
        }
        return abilities;
    }

    /// <summary>
    /// Function that loads all skills in the database.
    /// </summary>
    /// <returns>The skills stored in the database.</returns>
    public Skill[] LoadSkills() {
        object[,] data = Load("skills");
        int results = data.GetLength(0);
        Skill[] skills = new Skill[results];
        for (int row = 0; row < results; row ++) {
            skills[row] = new((int) (long) data[row, 0], (string) data[row, 1]);
        }
        return skills;
    }

    /// <summary>
    /// Function that loads all enemies in the database.
    /// </summary>
    /// <param name="abilityRegistry">The ability registry object.</param>
    /// <returns>The enemies stored in the database.</returns>
    public Enemy[] LoadEnemies(AbilityRegistry abilityRegistry) {
        Ability[] allAbilities = abilityRegistry.GetAbilities();
        object[,] data = Load("enemies");
        int results = data.GetLength(0);
        Enemy[] enemies = new Enemy[results];
        for (int row = 0; row < results; row ++) {
            List<Ability> enemyAbilities = [];
            long id = (long) data[row, 0];
            object[,] statData = Load("enemy_stats", ["enemy_id"],
                [id.ToString()]);
            object[,] abilities = Load("enemy_abilities", ["enemy_id"], 
                [id.ToString()], "ability_id");
            object[,] elementData = Load("enemy_elements", ["enemy_id"],
                [id.ToString()]);
            object[,] statusRegistry = Load("enemy_statuses", ["enemy_id"], 
                [id.ToString()]);
            int statResults = statData.GetLength(0);
            int abilityResults = abilities.GetLength(0);
            int elementResults = elementData.GetLength(0);
            int statusResults = statusRegistry.GetLength(0);
            int[] stats = new int[statResults];
            int[] elementalResistances = new int[elementResults];
            int[] statusResistances = new int[statusResults];

            // Load enemy abilities.
            for (int abilityRow = 0; abilityRow < abilityResults; abilityRow ++) {
                enemyAbilities.Add(allAbilities[((long) abilities[abilityRow, 1]) - 1]);
            }
            // Load stats.
            for (int statRow = 0; statRow < statResults; statRow ++) {
                int statId = Convert.ToInt32((long) statData[statRow, 1]);
                stats[statId - 1] = Convert.ToInt32((long) statData[statRow, 2]);
            }
            // Load elemental resistances.
            for (int elementRow = 0; elementRow < elementResults; elementRow ++) {
                int elementId = Convert.ToInt32((long) elementData[elementRow, 1]);
                elementalResistances[elementId - 1] = Convert.ToInt32((long) elementData[elementRow, 2]);
            }
            // Load status resistances.
            for (int statusRow = 0; statusRow < statusResults; statusRow ++) {
                int statusId = Convert.ToInt32((long) statusRegistry[statusRow, 1]);
                statusResistances[statusId - 1] = Convert.ToInt32((long) statusRegistry[statusRow, 2]);
            }
            // Load other enemy information.
            string name = (string) data[row, 1];
            string description = (string) data[row, 2];
            int maxHp = Convert.ToInt32((long) data[row, 3]);
            int battleSpriteId = Convert.ToInt32((long) data[row, 4]);
            enemies[row] = new(name, description, maxHp, battleSpriteId, stats, elementalResistances, statusResistances, 
                enemyAbilities);
        }
        return enemies;
    }

    /// <summary>
    /// Function that loads all playable characters in the database, does not load stats, equipment etc, that is to be adjusted
    /// in save files.
    /// </summary>
    /// <returns>The base of playable characters stored in the database.</returns>
    public PlayableCharacter[] LoadPlayableCharacters() {
        object[,] data = Load("playable_characters");
        int results = data.GetLength(0);
        PlayableCharacter[] playableCharacters = new PlayableCharacter[results];
        for (int row = 0; row < results; row ++) {
            long id = (long) (data[row, 0]) - 1;
            string name = (string) data[row, 1];
            string description = (string) data[row, 2];
            int battleSpriteId = Convert.ToInt32((long) data[row, 3]);
            int fieldSpriteId = Convert.ToInt32((long) data[row, 4]);
            int menuSpriteId = Convert.ToInt32((long) data[row, 5]);

            // Create empty values for remaining stats.
            int[] stats = [];
            int[] elementalResistances = [];
            int[] statusResistances = [];
            Equipment[] characterEquipment = [];
            List<Ability> characterBaseAbilities = [];
            List<int> equipmentTypes = [];
            List<Skill> characterSkills = [];

            playableCharacters[row] = new((int) id + 1, name, description, 0, 0, 0, 0, 
                battleSpriteId, fieldSpriteId, menuSpriteId, stats, elementalResistances, 
                statusResistances, characterEquipment, characterBaseAbilities, equipmentTypes, characterSkills);
        }
        return playableCharacters;
    }

    /// <summary>
    /// Function that loads all actor sprites in the database.
    /// </summary>
    /// <returns>The actor sprites stored in the database.</returns>
    public ActorSprite[] LoadActorSprites() {
        object[,] data = Load("actor_sprites");
        int results = data.GetLength(0);
        ActorSprite[] actorSprites = new ActorSprite[results];
        for (int row = 0; row < results; row ++) {
            int spriteId = Convert.ToInt32((long) data[row, 0]);
            int width = Convert.ToInt32((long) data[row, 1]);
            int height = Convert.ToInt32((long) data[row, 2]);
            actorSprites[row] = new(spriteId, width, height);
        }
        return actorSprites;
    }

    /// <summary>
    /// Function that loads all actors information in the database.
    /// </summary>
    /// <param name="actorSpriteRegistry">The actor sprite registry object.</param>
    /// <returns>The actor information stored in the database.</returns>
    public ActorInfo[] LoadActorInfo(ActorSpriteRegistry actorSpriteRegistry) {
        object[,] data = Load("actors");
        int results = data.GetLength(0);
        ActorInfo[] actors = new ActorInfo[results];
        for (int row = 0; row < results; row ++) {
            int id = (int) (long) data[row, 0];
            int behaviour = (int) data[row, 1];
            int spriteId = (int) (long) data[row, 2];
            int movementSpeed = (int) data[row, 3];
            int trackingRange = (int) (long) data[row, 4];
            bool belowParty = Convert.ToInt32((long) data[row, 5]) == 1;
            bool passable = Convert.ToInt32((long) data[row, 6]) == 1;
            bool onTouch = Convert.ToInt32((long) data[row, 7]) == 1;
            bool auto = Convert.ToInt32((long) data[row, 8]) == 1;
            bool onAction = Convert.ToInt32((long) data[row, 9]) == 1;
            bool onFind = Convert.ToInt32((long) data[row, 10]) == 1;
            int scriptId = (int) (long) data[row, 11];
            // TODO: (CSAF) Check behaviour is pathed, if so load the path from the other database.
            actors[row] = new ActorInfo(behaviour, actorSpriteRegistry.GetActorSprite(spriteId), movementSpeed, trackingRange, belowParty, passable, onTouch, auto,
                onAction, onFind, scriptId);
        }
        return actors;
    }

    /// <summary>
    /// Function that loads all stat names and short stat names in the database.
    /// </summary>
    /// <returns>The stat names and short stat names stored in the database.</returns>
    public string[,] LoadStatNames() {
        object[,] data = Load("stats");
        int results = data.GetLength(0);

        // Load both first name and short name.
        string[,] names = new string[results, 2];
        for (int row = 0; row < results; row ++) {
            names[row, 0] = (string) data[row, 1];
            names[row, 1] = (string) data[row, 2];
        }
        return names;
    }

    /// <summary>
    /// Loads elements from a passed table.
    /// </summary>
    /// <param name="tableName">The name of the table to have it's elements loaded.</param>
    /// <param name="condition">The conditions that is used when selecting from the database.</param>
    /// <param name="value">The value of the conditions that is used when selecting from the database.</param>
    /// <param name="sortBy">The key to sort the results by.</param>
    /// <returns>A 2D array representing the sql table results of the condition passed.</returns>
    /// <exception cref="MissingDatabaseTableException">Error thrown if a table that is trying to be loaded does not exist.</exception>
    private object [,] Load(string tableName, string[] ? conditions = null, string[] ? values = null, 
        string ? sortBy = null) {
        // Load database elements using sqlite connection.
        connection.Open();
        SqliteCommand command = connection.CreateCommand();
        SqliteCommand countCommand = connection.CreateCommand();
        long count;
        // If no conditions set load all.
        command.CommandText =
        $@"
            SELECT *
            FROM {tableName}";
        countCommand.CommandText =
        $@"
            SELECT COUNT(*)
            FROM {tableName}";
        // If conditions set only load from condition.
        if (conditions != null && values != null) {
            command.CommandText = $"{command.CommandText} WHERE ";
            countCommand.CommandText = $"{countCommand.CommandText} WHERE ";
            for (int index = 0; index < conditions.Length; index++) {
                command.CommandText = $"{command.CommandText}{conditions[index]} = {values[index]}";
                countCommand.CommandText = $"{countCommand.CommandText}{conditions[index]} = {values[index]}";
                if (index < conditions.Length - 1) {
                    command.CommandText = $"{command.CommandText} AND ";
                    countCommand.CommandText = $"{countCommand.CommandText} AND ";
                }
            }
        }
        // If sort is set sort by the passed variable.
        if (sortBy != null) {
            command.CommandText = $"{command.CommandText} ORDER BY {sortBy} DESC";
        }
        object ? test = countCommand.ExecuteScalar();
        if (test != null) {
            count = (long) test;
        } else {
            throw new MissingDatabaseTableException($"Table: {tableName} is missing from the database.");
        }
        // Load each row storing the tables data in array.
        SqliteDataReader query = command.ExecuteReader();
        object[,] objects = new object[count, query.FieldCount];
        int rowIndex = 0;
        while (query.Read()) {
            object[] ? data = new object[query.FieldCount];
            query.GetValues(data);
            for (int columnIndex = 0; columnIndex < query.FieldCount; columnIndex ++) {
                objects[rowIndex, columnIndex] = data[columnIndex];
            }
            rowIndex ++;
        }
        query.Close();
        connection.Close();
        return objects;
    }

    /// <summary>
    /// Helper function that loads string names from a database.
    /// </summary>
    /// <param name="tableName">The name of the table to search.</param>
    /// <returns>A array of string names from a passed database.</returns>
    private string[] LoadStringNames(string tableName) {
        object[,] data = Load(tableName);
        int results = data.GetLength(0);
        string[] strings = new string[results];
        for (int row = 0; row < results; row ++) {
            strings[row] = (string) data[row, 1];
        }
        return strings;
    }

    private readonly SqliteConnection connection;
}
