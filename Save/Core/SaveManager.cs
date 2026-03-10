namespace Save.Core;

using Game.Entities.PlayableCharacter;
using Game.Entities.Status;
using Game.Entities.Skills;
using Game.Items;
using Game.Party;
using Registry.Entities;
using Registry.Items;
using Registry.Names;
using Registry.Skills;
using Registry.Variables;
using Save.Interfaces;
using Save.Serialization.EnemyFormationData;
using Save.Serialization.PartyData;
using Save.Serialization.PlayableCharacterRegistry;
using Save.Serialization.VariablesData;
using Utils.Exceptions;
using System.Text.Json;

/// <summary>
/// The save manager class, used to handle saving and loading data.
/// </summary>
public sealed class SaveManager {
    /// <summary>
    /// Constructor for the save manager class.
    /// </summary>
    /// <param name="activeSlot">The active save file slot.</param>
    public SaveManager(int activeSlot) {
        this.activeSlot = activeSlot;
    }

    /// <summary>
    /// Save party function takes an index and save to that save folder.
    /// </summary>
    /// <param name="partyInfoAccessor">The party info accessor game object.</param>
    /// <param name="playableCharacters">The playable character array.</param>
    /// <param name="gameVariables">The game variables object.</param>
    /// <param name="mapName">The map name.</param>
    public async void Save(IPartyDynamicDataAccessor partyInfoAccessor, ISaveableCharacter[] playableCharacters, GameVariables gameVariables, string mapName) {
        // Store save directory for re-use.
        string saveDirectory = $"Save\\save_data\\save_{activeSlot}";

        // Ensure directory exists
        if (!Directory.Exists(saveDirectory)) {
            Directory.CreateDirectory(saveDirectory);
        }

        // Serialize with indentation for readability.
        JsonSerializerOptions options = new() { WriteIndented = true };
        
        // Use threading to execute all saves at once.
        Task nonCharacterTask = SaveAllExceptCharactersAsync(partyInfoAccessor, gameVariables, options, saveDirectory, mapName);
        Task characterTask = SaveCharactersAsync(playableCharacters, saveDirectory);
        await Task.WhenAll(nonCharacterTask, characterTask);
        // TODO: (AS-01) Implement save enemy formations.
    }

    /// <summary>
    /// A function used to load a save's parties sprites. Used to peek during the save/load screen.
    /// </summary>
    /// <param name="saveIndex">The save index to peek at.</param>
    /// <param name="maxPartySize">The max party size.</param>
    /// <param name="spriteIds">The sprite IDs array.</param>
    /// <returns>An array of ints representing the parties sprites.</returns>
    /// <exception cref="MissingPartyDataException">Error thrown if a .json data file is missing.</exception>
    public int[] GetPartiesSprites(int saveIndex, int maxPartySize, int[] spriteIds) {
        int[] sprites = new int[maxPartySize];
        try {
            string json = File.ReadAllText($"Save\\save_data\\save_{saveIndex + 1}\\info.json");
            // Load party members and party info.
            PartyData ? partyData = JsonSerializer.Deserialize<PartyData>(json) 
                ?? throw new MissingPartyDataException("info.json is missing");
            for (int partyIndex = 0; partyIndex < partyData.PartyMembers.Count; partyIndex ++) {
                sprites[partyIndex] = spriteIds[partyData.PartyMembers[partyIndex]];
            }
        } catch (FileNotFoundException) {
            // TODO: (HE-01) Throw custom exception here.
        }
        return sprites;
    }

    /// <summary>
    /// Function used on a new game to load the starting parties information.
    /// </summary>
    /// <param name="playableCharacters">The playable character array.</param>
    /// <param name="itemRegistry">The item data registry, contains all the items.</param>
    /// <param name="statusRegistry">The status data registry, contains all the statuses.</param>
    /// <param name="maxPartySize">The max party size, an indepedent config value required by savemanager.</param>
    /// <returns>The current save files party.</returns>
    /// <exception cref="MissingPartyDataException">Error thrown if a .json data file is missing.</exception>
    public Party LoadParty(PlayableCharacter[] playableCharacters, ItemRegistry itemRegistry, StatusRegistry statusRegistry,
        int maxPartySize) {
        string json;
        string inventoryJson;
        string ? statusesJson = null;
        if (activeSlot == 0) {
            json = File.ReadAllText("Save\\save_data\\new_game_data\\starting_info.json");
            inventoryJson = File.ReadAllText("Save\\save_data\\new_game_data\\starting_inventory.json");
        }
        else {
            json = File.ReadAllText($"Save\\save_data\\save_{activeSlot}\\info.json");
            inventoryJson = File.ReadAllText($"Save\\save_data\\save_{activeSlot}\\inventory.json");
            statusesJson = File.ReadAllText($"Save\\save_data\\save_{activeSlot}\\statuses.json");
        }
        
        // Load statuses.
        if (statusesJson != null) {
            CharacterStatusData ? statusData = JsonSerializer.Deserialize<CharacterStatusData>(statusesJson)
                ?? throw new MissingPartyDataException("statuses.json is missing");
            List<int> characters = statusData.Characters;
            List<int[]> statuses = statusData.Statuses;
            int characterIndex = 0;
            foreach (int characterId in characters) {
                List<Status> characterStatuses = [];
                int[] characterStatusesArray = statuses[characterIndex];
                foreach (int statusId in characterStatusesArray) {
                    playableCharacters[characterId - 1].AddStatus(statusRegistry.GetStatus(statusId - 1));
                }
            }
        }
        
        // Load party members and party info.
        PartyData ? partyData = JsonSerializer.Deserialize<PartyData>(json) 
            ?? throw new MissingPartyDataException("info.json is missing");
        PlayableCharacter[] partyMembers = new PlayableCharacter[maxPartySize];
        for (int partyIndex = 0; partyIndex < partyData.PartyMembers.Count; partyIndex ++) {
            int playableCharacterId = partyData.PartyMembers[partyIndex];
            partyMembers[partyIndex] = playableCharacters[playableCharacterId];
        }
        
        // Load starting inventory.
        Item[] allItems = itemRegistry.GetItems();
        List<InventoryItem> inventory = [];
        InventoryData ? inventoryData = JsonSerializer.Deserialize<InventoryData>(inventoryJson)
            ?? throw new MissingPartyDataException("inventory.json is missing");
        int itemIndex = 0;
        foreach (int itemId in inventoryData.Items) {
            int amount = inventoryData.Amount[itemIndex];
            inventory.Add(new(allItems[itemId], amount));
            itemIndex ++;
        }

        // Create and return party.
        return new Party(partyData.CurrentMap, partyMembers, inventory, partyData.XLocation, partyData.YLocation, partyData.Facing,
            partyData.Visible == 1);
    }

    /// <summary>
    /// Function that loads a specified file index's game flags and variables.
    /// </summary>
    /// <exception cref="MissingJsonFileException">Error thrown if a .json data file is missing.</exception>
    /// <returns>The variable data loaded from the saved flags .json.</returns>
    public VariablesData LoadVariables() {
        // Load filename based on selected slot.
        string fileName = activeSlot == 0 ? "Save\\save_data\\new_game_data\\starting_flags.json" : 
            $"Save\\save_data\\save_{activeSlot}\\flags.json";

        // Load game variables.
        var json = File.ReadAllText(fileName);
        return JsonSerializer.Deserialize<VariablesData>(json) ?? throw new MissingJsonFileException();
    }

    /// <summary>
    /// Function that loads a specified file index's enemy formation.
    /// </summary>
    /// <returns>Array of enemy formations.</returns>
    /// <exception cref="MissingJsonFileException">Error thrown if a .json data file is missing.</exception>
    public EnemyFormationData[] LoadEnemyFormations() {
        // Load enemy formation data from the files enemy_formations.json.
        var json = activeSlot == 0 ? File.ReadAllText("Save\\save_data\\new_game_data\\starting_enemy_formations.json") :
            File.ReadAllText($"Save\\save_data\\save{activeSlot}\\enemy_formations.json");
        EnemyFormationRootData root = JsonSerializer.Deserialize<EnemyFormationRootData>(json) 
            ?? throw new MissingJsonFileException();
        return [.. root.Formations];
    }

    /// <summary>
    /// Function that loads all playable characters in the database.
    /// </summary>
    /// <param name="characterDataLoader">The character data loader object.</param>
    /// <param name="skillRegistry">The skill data object, contains the skills.</param>
    /// <param name="abilityRegistry">The ability data object, contains the abilities.</param>
    /// <returns>The playable characters stored in the database.</returns>
    public PlayableCharacter[] LoadPlayableCharacters(ICharacterDataLoader characterDataLoader, SkillRegistry skillRegistry, AbilityRegistry abilityRegistry, 
        EquipmentRegistry equipmentRegistry, EquipmentSlotNameRegistry equipmentSlotNameRegistry) {
        // Load json save files.
        string characterSkillSAbilitiesFile, characterAbilitiesFile, characterStatsFile, characterElementsFile,
            characterStatusesFile, characterEquipmentTypesFile, characterEquippedFile, characterHpFile;
        if (activeSlot == 0) {
            characterSkillSAbilitiesFile = "Save\\save_data\\new_game_data\\starting_character_skills_abilities.json";
            characterAbilitiesFile = "Save\\save_data\\new_game_data\\starting_character_abilities.json";
            characterStatsFile = "Save\\save_data\\new_game_data\\starting_character_stats.json";
            characterElementsFile = "Save\\save_data\\new_game_data\\starting_character_elements.json";
            characterStatusesFile = "Save\\save_data\\new_game_data\\starting_character_statuses.json";
            characterEquipmentTypesFile = "Save\\save_data\\new_game_data\\starting_character_equipment_types.json";
            characterEquippedFile = "Save\\save_data\\new_game_data\\starting_character_equipped.json";
            characterHpFile = "Save\\save_data\\new_game_data\\starting_character_hp_mp.json";
        }
        else {
            string saveDirectory = $"Save\\save_data\\save_{activeSlot}";
            characterSkillSAbilitiesFile = $"{saveDirectory}\\character_skills_abilities.json";
            characterAbilitiesFile = $"{saveDirectory}\\character_abilities.json";
            characterStatsFile = $"{saveDirectory}\\character_stats.json";
            characterElementsFile = $"{saveDirectory}\\character_elements.json";
            characterStatusesFile = $"{saveDirectory}\\character_statuses.json";
            characterEquipmentTypesFile = $"{saveDirectory}\\character_equipment_types.json";
            characterEquippedFile = $"{saveDirectory}\\character_equipped.json";
            characterHpFile = $"{saveDirectory}\\character_hp_mp.json";
        }
        CharacterSkillsAbilitiesData skillsAbilities = LoadJson<CharacterSkillsAbilitiesData>(characterSkillSAbilitiesFile);
        CharacterAbilitiesData baseAbilities = LoadJson<CharacterAbilitiesData>(characterAbilitiesFile);
        CharacterStatsData statsData = LoadJson<CharacterStatsData>(characterStatsFile);
        CharacterResistanceData elementRes = LoadJson<CharacterResistanceData>(characterElementsFile);
        CharacterResistanceData statusRes = LoadJson<CharacterResistanceData>(characterStatusesFile);
        CharacterEquipmentTypesData equipTypes = LoadJson<CharacterEquipmentTypesData>(characterEquipmentTypesFile);
        CharacterEquippedData equippedData = LoadJson<CharacterEquippedData>(characterEquippedFile);
        CharacterHpMpData hpMpData = LoadJson<CharacterHpMpData>(characterHpFile);

        // Load skills and abilities in advanced of loops.
        Skill[] skills = skillRegistry.GetSkills();
        Ability[] abilities = abilityRegistry.GetAbilities();
        Equipment[] equipment = equipmentRegistry.GetEquipment();

        PlayableCharacter[] playableCharacters = characterDataLoader.LoadPlayableCharacters();
        foreach (PlayableCharacter playableCharacter in playableCharacters) {
            // Load characters ID.
            int id = playableCharacter.GetId() - 1;

            // Load basic int lists.
            List<int> equipmentTypes = [];
            List<int> equipmentTypesList = equipTypes.EquipmentTypes[id];
            int[] equipmentTypesArray = [.. equipmentTypesList];
            foreach (int equipmentType in equipmentTypesArray) {
                equipmentTypes.Add(equipmentType);
            }
            int[] stats = [.. statsData.Stats[id]];
            int[] elementalResistances = [.. elementRes.Resistances[id]];
            int[] statusResistances = [.. statusRes.Resistances[id]];

            // Load equipment.
            Equipment?[] characterEquipment = new Equipment[equipmentSlotNameRegistry.GetEquipmentSlotNames().Length];
            for (int equipSlotIndex = 0; equipSlotIndex < equipmentSlotNameRegistry.GetEquipmentSlotNames().Length; equipSlotIndex ++) {
                int equipmentId = equippedData.Equipped[id][equipSlotIndex];
                characterEquipment[equipSlotIndex] = equipmentId > 0 ? equipment[equipmentId - 1] : null;
            }

            // Load base abilities.
            List<Ability> characterBaseAbilities = [];
            foreach (int abilityId in baseAbilities.Abilities[id]) {
                characterBaseAbilities.Add(abilities[abilityId]);
            }

            // Load skills.
            List<Skill> characterSkills = [];
            for (int skillIndex = 0; skillIndex < skillsAbilities.Skills[id].Count; skillIndex++) {
                int skillId = skillsAbilities.Skills[id][skillIndex];
                Skill skill = skills[skillId];
                foreach (int abilityId in skillsAbilities.Abilities[id][skillIndex]) {
                    skill.AddAbility(abilities[abilityId]);
                }
                characterSkills.Add(skill);
            }

            // Load HP and MP values.
            int currentHp = hpMpData.HP[id][0];
            int maxHp     = hpMpData.HP[id][1];
            int currentMp = hpMpData.MP[id][0];
            int maxMp     = hpMpData.MP[id][1];

            // Update other character information.
            playableCharacter.SetEquipmentTypes(equipmentTypes);
            playableCharacter.SetStats(stats);
            playableCharacter.SetElementalResistances(elementalResistances);
            playableCharacter.SetStatusResistances(statusResistances);
            playableCharacter.SetEquipment(characterEquipment);
            playableCharacter.SetBaseAbilities(characterBaseAbilities);
            playableCharacter.SetSkills(characterSkills);
            playableCharacter.SetMaxHp(maxHp);
            playableCharacter.SetCurrentHP(currentHp);
            playableCharacter.SetMaxMp(maxMp);
            playableCharacter.SetCurrentMP(currentMp);
        }
        return playableCharacters;
    }

    /// <summary>
    /// The getter for the current active save slot.
    /// </summary>
    /// <returns>The active slot.</returns>
    public int GetActiveSlot() {
        return activeSlot;
    }

    /// <summary>
    /// The setter for the current active save slot.
    /// </summary>
    /// <param name="newActiveSlot">The new active slot being  used.</param>
    public void SetActiveSlot(int newActiveSlot) {
        activeSlot = newActiveSlot;
    }

    /// <summary>
    /// Save async function that executes all saving tasks at once.
    /// </summary>
    /// <param name="partyInfoAccessor">The party info accessor game object.</param>
    /// <param name="gameVariables">The game variables object.</param>
    /// <param name="options">The seralizer options of the json output.</param>
    /// <param name="saveDirectory">The save directory.</param>
    /// <param name="mapName">The map name.</param>
    private async Task SaveAllExceptCharactersAsync(IPartyDynamicDataAccessor partyInfoAccessor, GameVariables gameVariables, 
        JsonSerializerOptions options, string saveDirectory, string mapName) {
        Task saveStatuses = SaveStatuses(partyInfoAccessor.GetPartyMembers(), options, saveDirectory);
        Task saveInfo = SaveInfo(partyInfoAccessor, partyInfoAccessor.GetPartyMembers(), options, saveDirectory, mapName);
        Task saveInventory = SaveInventory(partyInfoAccessor.GetInventory(), options, saveDirectory);
        Task saveVariables = SaveVariables(gameVariables, options, saveDirectory);

        await Task.WhenAll(saveStatuses, saveInfo, saveInventory, saveVariables);
    }

    /// <summary>
    /// Function used to save the playable characters.
    /// </summary>
    /// <param name="playableCharacters">The array of playable characters.</param>
    /// <param name="saveDirectory">The save directory.</param>
    private async Task SaveCharactersAsync(ICharacterModifiersAccessor[] playableCharacters, string saveDirectory) {
        // Prepare all container objects.
        CharacterSkillsAbilitiesData skillsAbilitiesData = new();
        CharacterAbilitiesData baseAbilitiesData = new();
        CharacterStatsData statsData = new();
        CharacterResistanceData elementResData = new();
        CharacterResistanceData statusResData = new();
        CharacterEquipmentTypesData equipTypesData = new();
        CharacterEquippedData equippedData = new();
        CharacterHpMpData hpMpData = new();

        // Load each characters dyamic data.
        foreach (ICharacterModifiersAccessor character in playableCharacters) {
            int id = character.GetId();
            skillsAbilitiesData.Characters.Add(id);
            baseAbilitiesData.Characters.Add(id);
            statsData.Characters.Add(id);
            elementResData.Characters.Add(id);
            statusResData.Characters.Add(id);
            equipTypesData.Characters.Add(id);
            equippedData.Characters.Add(id);
            hpMpData.Characters.Add(id);

            // Equipment types.
            equipTypesData.EquipmentTypes.Add(character.GetEquipmentTypes());

            // Stats.
            statsData.Stats.Add(character.GetStats().ToList());

            // Resistances.
            elementResData.Resistances.Add(character.GetElementalResistances().ToList());
            statusResData.Resistances.Add(character.GetStatusResistances().ToList());

            // Equipment.
            List<int> equippedIds = [.. character.GetEquipment().Select(equipment => equipment != null 
                ? equipment.GetId() : 0)];
            equippedData.Equipped.Add(equippedIds);

            // Base abilities.
            List<int> abilityIds = [.. character.GetBaseAbilities().ToArray().ToList().Select(ability => ability.GetId())];
            baseAbilitiesData.Abilities.Add(abilityIds);

            // Skills and their related abilities.
            List<int> skillIds = [];
            List<List<int>> allAbilityIds = [];
            foreach (Skill skill in character.GetSkills()) {
                skillIds.Add(skill.GetId() - 1);
                List<int> skillAbilitiesIds = [.. skill.GetAbilities().Select(ability => ability.GetId())];
                allAbilityIds.Add(skillAbilitiesIds);
            }
            skillsAbilitiesData.Skills.Add(skillIds);
            skillsAbilitiesData.Abilities.Add(allAbilityIds);

            // HP and MP.
            List<int> hpValues =
            [
                character.GetCurrentHp(),
                character.GetMaxHp()
            ];
            List<int> mpValues =
            [
                character.GetCurrentMp(),
                character.GetMaxMp()
            ];

            hpMpData.HP.Add(hpValues);
            hpMpData.MP.Add(mpValues);
        }

        // Serialize each file.
        Task saveSkills = SaveJson(skillsAbilitiesData, $"{saveDirectory}\\character_skills_abilities.json");
        Task saveAbilities = SaveJson(baseAbilitiesData, $"{saveDirectory}\\character_abilities.json");
        Task saveStats = SaveJson(statsData, $"{saveDirectory}\\character_stats.json");
        Task saveElementalResistances = SaveJson(elementResData, $"{saveDirectory}\\character_elements.json");
        Task saveStatusResistances = SaveJson(statusResData, $"{saveDirectory}\\character_statuses.json");
        Task saveEquipmentTypes = SaveJson(equipTypesData, $"{saveDirectory}\\character_equipment_types.json");
        Task saveEquipment = SaveJson(equippedData, $"{saveDirectory}\\character_equipped.json");
        Task saveHPAndMp = SaveJson(hpMpData, $"{saveDirectory}\\character_hp_mp.json");
        await Task.WhenAll(saveSkills, saveAbilities, saveStats, saveElementalResistances, saveStatusResistances, saveEquipmentTypes,
            saveEquipment, saveHPAndMp);
    }

    /// <summary>
    /// Function used to save the current statuses characters have.
    /// </summary>
    /// <param name="partyMembers">The array of playable characters representing the party members.</param>
    /// <param name="options">The seralizer options of the json output.</param>
    /// <param name="saveDirectory">The save directory.</param>
    private Task SaveStatuses(IStatusAccessor[] partyMembers, JsonSerializerOptions options, string saveDirectory) {
        // Load status data.
        List<int> characterIds = [];
        List<int[]> statuses = [];
        foreach (IStatusAccessor character in partyMembers) {
            if (character != null) {
                // I'm not really a fan of this code but C# doesen't like implicit casting of lists of a child to a parent class...
                List<Status> characterStatuses = character.GetStatuses();
                if (characterStatuses.Count != 0) {
                    int[] statusIds = new int[characterStatuses.Count];
                    int statusIndex = 0;
                    foreach(IIDAccessor status in characterStatuses) {
                        statusIds[statusIndex] = status.GetId();
                        statusIndex ++;
                    }
                    characterIds.Add(character.GetId());
                    statuses.Add(statusIds);
                }
            }
        }
        // Write to file.
        var statusRegistry = new CharacterStatusData {
            Characters = characterIds,
            Statuses = statuses
        };
        string json = JsonSerializer.Serialize(statusRegistry, options);
        return Task.Run(() =>
        {
            File.WriteAllText($"{saveDirectory}//statuses.json", json);
        });
    }

    /// <summary>
    /// Function used to save the current game info.
    /// </summary>
    /// <param name="partyLocationAccessor">The party of the game.</param>
    /// <param name="partyMembers">The array of playable characters in the form of ID accessor interfaces.</param>
    /// <param name="options">The seralizer options of the json output.</param>
    /// <param name="saveDirectory">The save directory.</param>
    /// <param name="mapName">The current map name.</param>
    private Task SaveInfo(IPartyLocationAccessor partyLocationAccessor, IIDAccessor[] partyMembers, 
        JsonSerializerOptions options, string saveDirectory, string mapName) {
        List<int> exportPartyMembers = [];
        foreach (IIDAccessor character in partyMembers) {
            if (character != null) {
                exportPartyMembers.Add(character.GetId() - 1);
            }
        }
        PartyData partyData = new()
        {
            Visible = partyLocationAccessor.GetVisible() ? 1 : 0,
            CurrentMap = partyLocationAccessor.GetCurrentMapId(),
            MapName = mapName,
            XLocation = partyLocationAccessor.GetXLocation(),
            YLocation = partyLocationAccessor.GetYLocation(),
            Facing = partyLocationAccessor.GetDirection(),
            PartyMembers = exportPartyMembers
        };

        // Serialize.
        string json = JsonSerializer.Serialize(partyData, options);

        // Write to file.
        return Task.Run(() =>
        {
            File.WriteAllText($"{saveDirectory}//info.json", json);
        });
    }

    /// <summary>
    /// Function used to save the parties inventory.
    /// </summary>
    /// <param name="inventory">The inventory of the party.</param>
    /// <param name="options">The seralizer options of the json output.</param>
    /// <param name="saveDirectory">The save directory.</param>
    private Task SaveInventory(List<InventoryItem> inventory, JsonSerializerOptions options, string saveDirectory) {
        List<int> items = [], amount = [];
        foreach (InventoryItem item in inventory) {
            amount.Add(item.GetAmount());
            items.Add(item.GetItem().GetId() - 1);
        }
        var inventoryData = new InventoryData {
            Items = items,
            Amount = amount
        };
        
        // Serialize.
        string json = JsonSerializer.Serialize(inventoryData, options);

        // Write to file.
        return Task.Run(() =>
        {
            File.WriteAllText($"{saveDirectory}//inventory.json", json);
        });
    }

    /// <summary>
    /// Function used to save the game variables.
    /// </summary>
    /// <param name="gameVariables">The game variables registry.</param>
    /// <param name="options">The seralizer options of the json output.</param>
    /// <param name="saveDirectory">The save directory.</param>
    private Task SaveVariables(GameVariables gameVariables, JsonSerializerOptions options, string saveDirectory) {
        Dictionary<string, int> exportGameFlags = [], exportGameVariables = [];
        int index = 0;
        foreach (int gameVariable in gameVariables.GetGameVariables()) {
            exportGameVariables.Add(gameVariables.GetGameVariableName(index), gameVariable);
            index++;
        }
        index = 0;
        foreach (bool gameFlag in gameVariables.GetGameFlags()) {
            int value;
            if (gameFlag) {
                value = 1;
            }
            else {
                value = 0;
            }
            exportGameFlags.Add(gameVariables.GetGameFlagName(index), value);
            index++;
        }
        var variablesData = new VariablesData {
            Flags = exportGameFlags,
            Variables = exportGameVariables   
        };
        
        // Serialize.
        string json = JsonSerializer.Serialize(variablesData, options);

        // Write to file.
        return Task.Run(() =>
        {
            File.WriteAllText($"{saveDirectory}//flags.json", json);
        });
    }

    /// <summary>
    /// Function used to write data into json files.
    /// </summary>
    /// <param name="data">The varying data to be saved.</param>
    /// <param name="path">The file path to be saved into.</param>
    /// <typeparam name="T">The different data types, representing the dynamic data.</typeparam>
    private Task SaveJson<T>(T data, string path) {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(data, options);
        return Task.Run(() =>
        {
            File.WriteAllText(path, json);
        });
    }

    /// <summary>
    /// Function used to reduce duplicate code whe loading json.
    /// </summary>
    /// <param name="path">The file path of json file.</param>
    /// <typeparam name="T">The different data types, representing the dynamic data.</typeparam>
    /// <returns>The deserailsed data.</returns>
    private T LoadJson<T>(string path) {
        string json = File.ReadAllText(path);
        return JsonSerializer.Deserialize<T>(json)!;
    }

    private int activeSlot;
}