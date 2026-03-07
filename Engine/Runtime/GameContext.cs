namespace Engine.Runtime;

using Assets;
using Config.Runtime.Battle;
using Config.Runtime.Defaults;
using Config.Runtime.Game;
using Config.Runtime.Map;
using Config.Runtime.Menus;
using Config.Runtime.Windows;
using Database.Core;
using Engine.Input.Core;
using Engine.Input.Scenes;
using Engine.Maps.Core;
using Engine.Rendering;
using Engine.Systems;
using Game.Battle;
using Game.Entities.PlayableCharacter;
using Game.Map.Core;
using Game.Party;
using Registry.Enemies;
using Registry.Entities;
using Registry.Items;
using Registry.Names;
using Registry.Skills;
using Registry.TileObjects;
using Registry.Variables;
using Save.Core;
using Scenes.Base;
using Scenes.Battle.Core;
using Scenes.Map.Core;
using States.Base;
using States.Battle.Core;
using States.Map.Core;
using UI.Component.Core;
using UI.Core;
using UI.Interfaces;
using UI.States.Menu.Menus;

/// <summary>
/// The game context class. The game context stores the current state of the game, including the data registries,
/// party, game state, save manager and asset manager.
/// </summary>
public sealed class GameContext : IGameUIContext {
    /// <summary>
    /// Constructor for the game context. Contains the data registries as well as managers for saving and asset access.
    /// </summary>
    /// <param name="databaseManager">The database object that interacts with SQL.</param>
    /// <param name="saveManager">The save manager object that directly interacts with .json save data.</param>
    /// <param name="assetManager">The asset manager object that directly interacts with the file assets.</param>
    /// <param name="mapManager">The map manager object that directly interacts with the map assets.</param>
    /// <param name="animatedTileSheetConfig">The animated tile sheet config object.</param>
    /// <param name="battleBackgroundSpriteConfig">The battle background config object.</param>
    /// <param name="battleSceneConfig">The battle scene config object.</param>
    /// <param name="battleWindowConfig">The battle window config object.</param>
    /// <param name="characterFieldSpriteConfig">The character field sprite config object.</param>
    /// <param name="fileSelectorConfig">The file selector sprite config object.</param>
    /// <param name="gameConfig">The game config object.</param>
    /// <param name="gameWindowConfig">The game window config object.</param>
    /// <param name="inventoryConfig">The inventory config object.</param>
    /// <param name="mapBackgroundSpriteConfig">The map background sprite config object.</param>
    /// <param name="mapConfig">The map config object.</param>
    /// <param name="mainMenuConfig">The main menu config object.</param>
    /// <param name="tileSheetConfig">The tile sheet config object.</param>
    /// <param name="windowConfig">The window config object.</param>
    /// <param name="textWindowDefaults">The text window defaults object.</param>
    /// <param name="globalMessageDefaults">The global message defaults object.</param>
    public GameContext(DatabaseManager databaseManager, SaveManager saveManager, AssetManager assetManager, MapManager mapManager,
        AnimatedTileSheetConfig animatedTileSheetConfig, BattleBackgroundSpriteConfig battleBackgroundSpriteConfig,
        BattleSceneConfig battleSceneConfig, BattleWindowConfig battleWindowConfig, CharacterFieldSpriteConfig characterFieldSpriteConfig, 
        FileSelectorConfig fileSelectorConfig, GameConfig gameConfig, GameWindowConfig gameWindowConfig, InventoryConfig inventoryConfig,
        MapBackgroundSpriteConfig mapBackgroundSpriteConfig, MapConfig mapConfig, MainMenuConfig mainMenuConfig, 
        TileSheetConfig tileSheetConfig, WindowConfig windowConfig, TextWindowDefaults textWindowDefaults, 
        GlobalMessageDefaults globalMessageDefaults) {
        // Store config objects.
        this.animatedTileSheetConfig = animatedTileSheetConfig;
        this.battleBackgroundSpriteConfig = battleBackgroundSpriteConfig;
        this.battleSceneConfig = battleSceneConfig;
        this.battleWindowConfig = battleWindowConfig;
        this.characterFieldSpriteConfig = characterFieldSpriteConfig;
        this.fileSelectorConfig = fileSelectorConfig;
        this.gameConfig = gameConfig;
        this.inventoryConfig = inventoryConfig;
        this.mapBackgroundSpriteConfig = mapBackgroundSpriteConfig;
        this.mapConfig = mapConfig;
        this.mainMenuConfig = mainMenuConfig;
        this.tileSheetConfig = tileSheetConfig;
        this.windowConfig = windowConfig;
        this.globalMessageDefaults = globalMessageDefaults;
        this.textWindowDefaults = textWindowDefaults;

        // Database data.
        abilityRegistry = new AbilityRegistry(databaseManager.LoadAbilities());
        elementNameRegistry = new ElementNameRegistry(databaseManager.LoadElementNames());
        enemyRegistry = new EnemyRegistry(databaseManager.LoadEnemies(abilityRegistry));
        equipmentSlotNameRegistry = new EquipmentSlotNameRegistry(databaseManager.LoadEquipmentSlotNames());
        equipmentTypeNameRegistry = new EquipmentTypeNameRegistry(databaseManager.LoadEquipmentTypeNames());
        itemRegistry = new ItemRegistry(databaseManager.LoadItems());
        skillRegistry = new SkillRegistry(databaseManager.LoadSkills());
        equipmentRegistry = new EquipmentRegistry(databaseManager.LoadEquipment(itemRegistry, skillRegistry, abilityRegistry));
        statNameRegistry = new StatNameRegistry(databaseManager.LoadStatNames());
        statusRegistry = new StatusRegistry(databaseManager.LoadStatuses(skillRegistry, abilityRegistry));
        tileObjectRegistry = new TileObjectRegistry(databaseManager.LoadTileObjects());

        // Save data.
        // TODO: This line will crash when loading because we don't save enemy formations yet.
        enemyFormationRegistry = new EnemyFormationRegistry(saveManager.LoadEnemyFormations());
        playableCharacterRegistry = new PlayableCharacterRegistry(databaseManager, skillRegistry, abilityRegistry, 
            equipmentRegistry, equipmentSlotNameRegistry, saveManager);
        this.saveManager = saveManager;

        // Managers.
        this.assetManager = assetManager;
        this.mapManager = mapManager;

        // Game variables.
        gameVariables = new GameVariables(saveManager);

        // Party.
        party = saveManager.LoadParty(playableCharacterRegistry.GetPlayableCharacters(), itemRegistry, statusRegistry, gameConfig.GetMaxPartySize());

        // Game window.
        gameWindow = new(gameWindowConfig, mapConfig, tileSheetConfig);

        // UI stack.
        uiStates = new Stack<UIState>();

        // Load initial map, scene and state.
        // Add map name here, it'll be set in the function.
        mapName = "";
        LoadNewMap();

        // Systems.
        statusResolver = new StatusResolver();

        // Start game loop.
        GameLoop();
    }

    /// <summary>
    /// Function used to load a new map scene / state and map object.
    /// </summary>
    public void LoadNewMap() {
        Map map = mapManager.LoadMap(party, tileObjectRegistry);
        mapName = map.GetMapName();
        MapScene mapScene = new(gameWindow, assetManager, animatedTileSheetConfig, characterFieldSpriteConfig, 
            mapBackgroundSpriteConfig, mapConfig, tileSheetConfig, map);
        currentScene = mapScene;
        MapState mapState = new (this, mapScene, mapConfig, map);

        // Set the controls after creation.
        MapInputMap inputMap = new(mapState);
        currentInputMap = inputMap;
        gameWindow.SetInputMap(inputMap);
        
        currentState = mapState;
    }

    /// <summary>
    /// Function used to start a new battle and load it into the scene controller as the active scene.
    /// </summary>
    /// <param name="backgroundMusicId">The battle background music ID.</param>
    /// <param name="battleBackgroundArtId">The battle background art ID.</param>
    /// <param name="enemyFormationId">The enemy formation used in the battle.</param>
    public void LoadNewBattle(int backgroundMusicId, int battleBackgroundArtId, int enemyFormationId) {
        BattleScene battleScene = new(gameWindow, gameConfig, battleBackgroundSpriteConfig, battleSceneConfig, battleWindowConfig, windowConfig,
             assetManager, party, enemyFormationRegistry.GetEnemyFormation(enemyFormationId), enemyRegistry, battleBackgroundArtId, backgroundMusicId);
        currentScene = battleScene;
        Battle battle = new(enemyFormationRegistry, enemyRegistry, enemyFormationId);
        BattleState battleState = new(battle, this, battleScene);

        // Set the controls after creation.
        BattleInputMap inputMap = new(battleState);
        currentInputMap = inputMap;
        gameWindow.SetInputMap(inputMap);
        
        currentState = battleState;
    }

    /// <summary>
    /// Game loop function.
    /// </summary>
    public void GameLoop() {
        // TODO: Yets consider moving this to another file or create a variable for if the game is open.
        while (gameWindow.IsOpen()) {
            gameWindow.Clear();
            if (!paused) {
                currentState?.Update();
                currentScene?.Update();
                currentScene?.Draw();
            }
            else {
                currentScene?.ResetClocks();
            }
            foreach (UIState uiState in uiStates) {
                IUIComponent[] components = [.. uiState.GetComponents()];
                for (int componentIndex = components.Length - 1; componentIndex >= 0; componentIndex --) {
                    gameWindow.Draw(components[componentIndex].CreateSprite());
                }
            }
            gameWindow.DispatchEvents();
            gameWindow.Display();
        }
    }

    /// <summary>
    /// Function used to enqueue a new UI state.
    /// </summary>
    /// <param name="newUIState">The new state to be added to the end of the queue.</param>
    public void AddUIState(UIState newUIState) {
        if (newUIState.TakesControl()) {
            gameWindow.SetInputMap(newUIState.GetInputMap());
        }
        uiStates.Push(newUIState);
    }

    /// <summary>
    /// Function used to pop the UI queue.
    /// </summary>
    public void PopUIStack() {
        uiStates.Pop();
        if (uiStates.Count > 0 && uiStates.Peek().TakesControl()) {
            currentInputMap = uiStates.Peek().GetInputMap();
        }
        if (currentInputMap == null) {
            // TODO: Custom exception here.
            throw new Exception();
        }
        gameWindow.SetInputMap(currentInputMap);
    }

    /// <summary>
    /// Function add the main menu to the UI stack.
    /// </summary>
    public void OpenMainMenu() {
        AddUIState(new MenuState(this, mainMenuConfig, inventoryConfig, fileSelectorConfig, windowConfig, gameWindow, 
            characterFieldSpriteConfig, assetManager, saveManager, party, gameVariables, GetPlayableCharacters(), 
            GetMaxSaveFiles(), GetMaxPartySize(), mapName));
    }

    /// <summary>
    /// Function used to resume the current game windows input.
    /// </summary>
    public void ResumeInput() {
        gameWindow.ResumeInput();
    }

    /// <summary>
    /// Function used to freeze the current game windows input.
    /// </summary>
    public void FreezeInput() {
        gameWindow.FreezeInput();
    }

    /// <summary>
    /// Function used to pause the game.
    /// </summary>
    public void Pause() {
        paused = true;
    }

    /// <summary>
    /// Function used to resume the game.
    /// </summary>
    public void Resume() {
        paused = false;
    }

    /// <summary>
    /// Function used to check if the game is paused.
    /// </summary>
    /// <returns>If the game is paused.</returns>
    public bool IsPaused() {
        return paused;
    }

    /// <summary>
    /// Getter for the maximum number of save files.
    /// </summary>
    /// <returns>The maximum number of save files.</returns>
    public int GetMaxSaveFiles() {
        return gameConfig.GetSaveFiles();
    }

    /// <summary>
    /// Getter for the max party size in the game.
    /// </summary>
    /// <returns>The max party size of the game.</returns>
    public int GetMaxPartySize() {
        return gameConfig.GetMaxPartySize();
    }

    /// <summary>
    /// Getter for the max lines in the window config.
    /// </summary>
    /// <returns>The max lines per page.</returns>
    public int GetMaxLinesPerPage() {
        return windowConfig.GetMaxLinesPerWindow();
    }

    /// <summary>
    /// Getter for the character movement speed.
    /// </summary>
    /// <returns>The character movement speed.</returns>
    public int GetCharacterMovementSpeed() {
        return characterFieldSpriteConfig.GetWalkAnimationSpeed();
    }

    /// <summary>
    /// Function used to load a specific window file art name.
    /// </summary>
    /// <param name="windowArtId">The window art ID.</param>
    /// <returns>The window art file name of a specific ID.</returns>
    public string GetWindowArtFileName(int windowArtId) {
        return assetManager.GetWindowArtFileName(windowArtId);
    }

    /// <summary>
    /// Function used to load a specific font file name.
    /// </summary>
    /// <param name="fontId">The font ID.</param>
    /// <returns>The font file name of a specific ID.</returns>
    public string GetFontFileName(int fontId) {
        return assetManager.GetFontFileName(fontId);
    }

    /// <summary>
    /// Getter for the playable characters data.
    /// </summary>
    /// <returns>The array of the game's playable characters.</returns>
    public PlayableCharacter[] GetPlayableCharacters() {
        return playableCharacterRegistry.GetPlayableCharacters();
    }

    /// <summary>
    /// Function used to load an array of all the playable character field sprite IDs.
    /// </summary>
    /// <returns>The array of field sprite IDs.</returns>
    public int[] GetPlayableCharacterFieldSpriteIds() {
        return playableCharacterRegistry.GetPlayableCharacterFieldSpriteIds();
    }

    /// <summary>
    /// Getter for the character field sprite config.
    /// </summary>
    /// <returns>The character field sprite config.</returns>
    public CharacterFieldSpriteConfig GetCharacterFieldSpriteConfig() {
        return characterFieldSpriteConfig;
    }

    /// <summary>
    /// Getter for the file selector config.
    /// </summary>
    /// <returns>The file selector config.</returns>
    public FileSelectorConfig GetFileSelectorConfig() {
        return fileSelectorConfig;
    }

    /// <summary>
    /// Getter for the main menu config.
    /// </summary>
    /// <returns>The main menu config object.</returns>
    public MainMenuConfig GetMainMenuConfig() {
        return mainMenuConfig;
    }

    /// <summary>
    /// Getter for the inventory config.
    /// </summary>
    /// <returns>The inventory config object.</returns>
    public InventoryConfig GetInventoryConfig() {
        return inventoryConfig;
    }

    /// <summary>
    /// Getter for the window config.
    /// </summary>
    /// <returns>The window config object.</returns>
    public WindowConfig GetWindowConfig() {
        return windowConfig;
    }

    /// <summary>
    /// Getter for the global message defaults object.
    /// </summary>
    /// <returns>The global message defaults.</returns>
    public GlobalMessageDefaults GetGlobalMessageDefaults() {
        return globalMessageDefaults;
    }

    /// <summary>
    /// Getter for the text window defaults object.
    /// </summary>
    /// <returns>The text window defaults.</returns>
    public TextWindowDefaults GetTextWindowDefaults() {
        return textWindowDefaults;
    }

    /// <summary>
    /// Getter for the ability data registry.
    /// </summary>
    /// <returns>The ability data.</returns>
    public AbilityRegistry GetAbilityRegistry() {
        return abilityRegistry;
    }

    /// <summary>
    /// Getter for the element name data registry.
    /// </summary>
    /// <returns>The element name data.</returns>
    public ElementNameRegistry GetElementNameRegistry() {
        return elementNameRegistry;
    }

    /// <summary>
    /// Getter for the enemy data registry.
    /// </summary>
    /// <returns>The enemy data.</returns>
    public EnemyRegistry GetEnemyRegistry() {
        return enemyRegistry;
    }

    /// <summary>
    /// Getter for the enemy formation data registry.
    /// </summary>
    /// <returns>The enemy formation data.</returns>
    public EnemyFormationRegistry GetEnemyFormationRegistry() {
        return enemyFormationRegistry;
    }

    /// <summary>
    /// Getter for the equipment data registry.
    /// </summary>
    /// <returns>The equipment data.</returns>
    public EquipmentRegistry GetEquipmentRegistry() {
        return equipmentRegistry;
    }

    /// <summary>
    /// Getter for the equipment slot name data registry.
    /// </summary>
    /// <returns>The equipment slot name data.</returns>
    public EquipmentSlotNameRegistry GetEquipmentSlotNameRegistry() {
        return equipmentSlotNameRegistry;
    }

    /// <summary>
    /// Getter for the equipment type name data registry.
    /// </summary>
    /// <returns>The equipment type name data.</returns>
    public EquipmentTypeNameRegistry GetEquipmentTypeNameRegistry() {
        return equipmentTypeNameRegistry;
    }

    /// <summary>
    /// Getter for the item data registry.
    /// </summary>
    /// <returns>The item data.</returns>
    public ItemRegistry GetItemRegistry() {
        return itemRegistry;
    }

    /// <summary>
    /// Getter for the playable character data registry.
    /// </summary>
    /// <returns>The playable character data.</returns>
    public PlayableCharacterRegistry GetPlayableCharacterRegistry() {
        return playableCharacterRegistry;
    }

    /// <summary>
    /// Getter for the skill data registry.
    /// </summary>
    /// <returns>The skill data.</returns>
    public SkillRegistry GetSkillRegistry() {
        return skillRegistry;
    }

    /// <summary>
    /// Getter for the stat name data registry.
    /// </summary>
    /// <returns>The stat name data.</returns>
    public StatNameRegistry GetStatNameRegistry() {
        return statNameRegistry;
    }

    /// <summary>
    /// Getter for the status data registry.
    /// </summary>
    /// <returns>The status data.</returns>
    public StatusRegistry GetStatusRegistry() {
        return statusRegistry;
    }

    /// <summary>
    /// Getter for the tile object data registry.
    /// </summary>
    /// <returns>The tile object data.</returns>
    public TileObjectRegistry GetTileObjectRegistry() {
        return tileObjectRegistry;
    }

    /// <summary>
    /// Getter for the save manager.
    /// </summary>
    /// <returns>The save manager.</returns>
    public SaveManager GetSaveManager() {
        return saveManager;
    }

    /// <summary>
    /// Getter for the asset manager.
    /// </summary>
    /// <returns>The asset manager.</returns>
    public AssetManager GetAssetManager() {
        return assetManager;
    }

    /// <summary>
    /// Getter for the map manager.
    /// </summary>
    /// <returns>The map manager.</returns>
    public MapManager GetMapManager() {
        return mapManager;
    }

    /// <summary>
    /// Getter for the game variables object, allowing for checking / changing flags or variables.
    /// </summary>
    /// <returns>The game variables object.</returns>
    public GameVariables GetGameVariables() {
        return gameVariables;
    }

    /// <summary>
    /// Getter for the party instance.
    /// </summary>
    /// <returns>The party.</returns>
    public Party GetParty() {
        return party;
    }

    /// <summary>
    /// Getter for the party instance's party members.
    /// </summary>
    /// <returns>The array of party members..</returns>
    public PlayableCharacter[] GetPartyMembers() {
        return party.GetPartyMembers();
    }

    /// <summary>
    /// Getter for the games render window.
    /// </summary>
    /// <returns>The render window used by the game.</returns>
    public GameWindow GetGameWindow() {
        return gameWindow;
    }

    /// <summary>
    /// Getter for the current scene.
    /// </summary>
    /// <returns>The current scene.</returns>
    public SceneBase? GetCurrentScene() {
        return currentScene;
    }

    /// <summary>
    /// Getter for the game context's current state.
    /// </summary>
    /// <returns>The current game state.</returns>
    public StateBase? GetCurrentState() {
        return currentState;
    }

    /// <summary>
    /// Getter for the status resolver system.
    /// </summary>
    /// <returns>The status resolver system.</returns>
    public StatusResolver GetStatusResolver() {
        return statusResolver;
    }

    // Config objects.
    private readonly AnimatedTileSheetConfig animatedTileSheetConfig;
    private readonly BattleBackgroundSpriteConfig battleBackgroundSpriteConfig;
    private readonly BattleSceneConfig battleSceneConfig;
    private readonly BattleWindowConfig battleWindowConfig;
    private readonly CharacterFieldSpriteConfig characterFieldSpriteConfig;
    private readonly FileSelectorConfig fileSelectorConfig;
    private readonly GameConfig gameConfig;
    private readonly InventoryConfig inventoryConfig;
    private readonly MapBackgroundSpriteConfig mapBackgroundSpriteConfig;
    private readonly MapConfig mapConfig;
    private readonly MainMenuConfig mainMenuConfig;
    private readonly TileSheetConfig tileSheetConfig;
    private readonly WindowConfig windowConfig;

    // Defaults objects.
    private readonly GlobalMessageDefaults globalMessageDefaults;
    private readonly TextWindowDefaults textWindowDefaults;

    // Data registry objects.
    // Data that will not change.
    private readonly AbilityRegistry abilityRegistry;
    private readonly ElementNameRegistry elementNameRegistry;
    private readonly EnemyRegistry enemyRegistry;
    private readonly EquipmentRegistry equipmentRegistry;
    private readonly EquipmentSlotNameRegistry equipmentSlotNameRegistry;
    private readonly EquipmentTypeNameRegistry equipmentTypeNameRegistry;
    private readonly ItemRegistry itemRegistry;
    private readonly SkillRegistry skillRegistry;
    private readonly StatNameRegistry statNameRegistry;
    private readonly StatusRegistry statusRegistry;
    private readonly TileObjectRegistry tileObjectRegistry;
    // Data that will change.
    private readonly EnemyFormationRegistry enemyFormationRegistry;
    private readonly PlayableCharacterRegistry playableCharacterRegistry;

    // Managers.
    private readonly SaveManager saveManager;
    private readonly AssetManager assetManager;
    private readonly MapManager mapManager;

    // In game variables.
    private readonly GameVariables gameVariables;

    // Party.
    private readonly Party party;

    // Game window object.
    private readonly GameWindow gameWindow;

    // Active scene and the current state objects.
    private SceneBase? currentScene;
    private StateBase? currentState;
    private InputMap? currentInputMap;

    // UI stack.
    private Stack<UIState> uiStates;

    // Systems.
    private readonly StatusResolver statusResolver;

    // Additional stored information.
    private string mapName;
    private bool paused;
}