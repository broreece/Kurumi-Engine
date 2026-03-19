namespace Bootstrap;

using Config.Loaders.Battle;
using Config.Loaders.Defaults;
using Config.Loaders.Game;
using Config.Loaders.Map;
using Config.Loaders.Menus;
using Config.Loaders.Windows;
using Config.Runtime.Battle;
using Config.Runtime.Defaults;
using Config.Runtime.Game;
using Config.Runtime.Map;
using Config.Runtime.Menus;
using Config.Runtime.Windows;
using Database.Core;
using Engine.Assets;
using Engine.MapManager.Core;
using Engine.Runtime;
using Engine.ScriptManager.Core;
using Save.Core;

/// <summary>
/// The bootstrap program that loads all inital config, builds context, starts scene manager and starts game loop.
/// </summary>
public static class Program {
    public static void Main(string[] args) {
        // Load database.
        DatabaseManager database = new();

        // Create save manager using save file passed.
        // TODO: (TS-01): Implement main menu screen to decide save index.
        SaveManager saveManager = new(0);

        // Load managers using file paths.
        string registryBasePath = Path.Combine(
            AppContext.BaseDirectory,
            "Assets",
            "Registry"
        );
        string assetRegistryPath = Path.Combine(
            registryBasePath,
            "asset_registry.json"
        );
        AssetManager assetManager = new(assetRegistryPath);
        string mapRegistryPath = Path.Combine(
            registryBasePath,
            "map_registry.json"
        );
        MapManager mapManager = new(mapRegistryPath);
        string mapScriptsRegistryPath = Path.Combine(
            registryBasePath,
            "map_script_registry.json"
        );
        MapScriptManager mapScriptManager = new(mapScriptsRegistryPath);

        // Load config.
        string configBasePath = Path.Combine(
            AppContext.BaseDirectory,
            "Config",
            "yaml"
        );
        // Battle config.
        string battleBackgroundConfigPath = Path.Combine(
            configBasePath,
            "Battle",
            "battle_background_sprite_config.yaml"
        );
        BattleBackgroundSpriteConfig battleBackgroundSpriteConfig = BattleBackgroundSpriteConfigManager.Load(battleBackgroundConfigPath);
        string battleSceneConfigPath = Path.Combine(
            configBasePath,
            "Battle",
            "battle_scene_config.yaml"
        );
        BattleSceneConfig battleSceneConfig = BattleSceneConfigManager.Load(battleSceneConfigPath);
        string battleWindowConfigPath = Path.Combine(
            configBasePath,
            "Battle",
            "battle_window_config.yaml"
        );
        BattleWindowConfig battleWindowConfig = BattleWindowConfigManager.Load(battleWindowConfigPath);
        string characterBattleSpriteConfigPath = Path.Combine(
            configBasePath,
            "Battle",
            "character_battle_sprite_config.yaml"
        );
        CharacterBattleSpriteConfig characterBattleSpriteConfig = CharacterBattleSpriteConfigManager.Load(characterBattleSpriteConfigPath);
        string enemyBattleSpriteConfigPath = Path.Combine(
            configBasePath,
            "Battle",
            "enemy_battle_sprite_config.yaml"
        );
        EnemyBattleSpriteConfig enemyBattleSpriteConfig = EnemyBattleSpriteConfigManager.Load(enemyBattleSpriteConfigPath);
        string partyChoicesConfigPath = Path.Combine(
            configBasePath,
            "Battle",
            "party_choices_config.yaml"
        );
        PartyChoicesConfig partyChoicesConfig = PartyChoicesConfigManager.Load(partyChoicesConfigPath);

        // Defaults.
        string choiceBoxDefaultsPath = Path.Combine(
            configBasePath,
            "Defaults",
            "choice_box_defaults.yaml"
        );
        ChoiceBoxDefaults choiceBoxDefaults = ChoiceBoxDefaultsManager.Load(choiceBoxDefaultsPath);
        string globalMessageDefaultsPath = Path.Combine(
            configBasePath,
            "Defaults",
            "global_message_defaults.yaml"
        );
        GlobalMessageDefaults globalMessageDefaults = GlobalMessageDefaultsManager.Load(globalMessageDefaultsPath);
        string nameBoxDefaultsPath = Path.Combine(
            configBasePath,
            "Defaults",
            "name_box_defaults.yaml"
        );
        NameBoxDefaults nameBoxDefaults = NameBoxDefaultsManager.Load(nameBoxDefaultsPath);
        string textWindowDefaultsPath = Path.Combine(
            configBasePath,
            "Defaults",
            "text_window_defaults.yaml"
        );
        TextWindowDefaults textWindowDefaults = TextWindowDefaultsManager.Load(textWindowDefaultsPath);

        // Game config.
        string gameConfigPath = Path.Combine(
            configBasePath,
            "Game",
            "game_config.yaml"
        );
        GameConfig gameConfig = GameConfigManager.Load(gameConfigPath);
        string gameWindowConfigPath = Path.Combine(
            configBasePath,
            "Game",
            "game_window_config.yaml"
        );
        GameWindowConfig gameWindowConfig = GameWindowConfigManager.Load(gameWindowConfigPath);

        // Map config.
        string animatedTileSheetConfigPath = Path.Combine(
            configBasePath,
            "Map",
            "animated_tilesheet_config.yaml"
        );
        AnimatedTileSheetConfig animatedTileSheetConfig = AnimatedTileSheetConfigManager.Load(animatedTileSheetConfigPath);
        string characterFieldSpriteConfigPath = Path.Combine(
            configBasePath,
            "Map",
            "character_field_sprite_config.yaml"
        );
        CharacterFieldSpriteConfig characterFieldSpriteConfig = CharacterFieldSpriteConfigManager.Load(characterFieldSpriteConfigPath);
        string mapBackgroundSpriteConfigPath = Path.Combine(
            configBasePath,
            "Map",
            "map_background_sprite_config.yaml"
        );
        MapBackgroundSpriteConfig mapBackgroundSpriteConfig = MapBackgroundSpriteConfigManager.Load(mapBackgroundSpriteConfigPath);
        string mapConfigPath = Path.Combine(
            configBasePath,
            "Map",
            "map_config.yaml"
        );
        MapConfig mapConfig = MapConfigManager.Load(mapConfigPath);
        string tileSheetConfigPath = Path.Combine(
            configBasePath,
            "Map",
            "tilesheet_config.yaml"
        );
        TileSheetConfig tileSheetConfig = TileSheetConfigManager.Load(tileSheetConfigPath);

        // Menu Config.
        string fileSelectorConfigPath = Path.Combine(
            configBasePath,
            "Menus",
            "file_selector_config.yaml"
        );
        FileSelectorConfig fileSelectorConfig = FileSelectorConfigManager.Load(fileSelectorConfigPath);
        string inventoryConfigPath = Path.Combine(
            configBasePath,
            "Menus",
            "inventory_config.yaml"
        );
        InventoryConfig inventoryConfig = InventoryConfigManager.Load(inventoryConfigPath);
        string mainMenuConfigPath = Path.Combine(
            configBasePath,
            "Menus",
            "main_menu_config.yaml"
        );
        MainMenuConfig mainMenuConfig = MainMenuConfigManager.Load(mainMenuConfigPath);

        // Window Config.
        string windowConfigPath = Path.Combine(
            configBasePath,
            "Windows",
            "windows_config.yaml"
        );
        WindowConfig windowConfig = WindowConfigManager.Load(windowConfigPath);

        // Create game context with neccesary arguments.
        GameContext gameContext = new(database, saveManager, assetManager, mapManager, animatedTileSheetConfig, battleBackgroundSpriteConfig, 
            battleSceneConfig, battleWindowConfig, characterFieldSpriteConfig, fileSelectorConfig, gameConfig, gameWindowConfig, 
            inventoryConfig, mapBackgroundSpriteConfig, mapConfig, mainMenuConfig, partyChoicesConfig, tileSheetConfig, windowConfig,
            choiceBoxDefaults, globalMessageDefaults, nameBoxDefaults, textWindowDefaults);
    }
}