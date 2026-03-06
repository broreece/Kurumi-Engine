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
using Engine.Maps.Core;
using Engine.Runtime;
using Save.Core;

/// <summary>
/// The bootstrap program that loads all inital config, builds context, starts scene manager and starts game loop.
/// </summary>
public static class Program {
    public static void Main(string[] args){
        // Load database.
        DatabaseManager database = new();

        // Create save manager using save file passed.
        // TODO: Change this to any file neccesary.
        SaveManager saveManager = new(0);

        // Load managers using static file paths.
        AssetManager assetManager = new("Assets\\Registry\\asset_registry.json");
        MapManager mapManager = new("Assets\\Registry\\map_registry.json");

        // Load config.
        // Battle config.
        BattleBackgroundSpriteConfig battleBackgroundSpriteConfig = BattleBackgroundSpriteConfigManager.Load("Battle\\battle_background_sprite_config.yaml");
        BattleSceneConfig battleSceneConfig = BattleSceneConfigManager.Load("Battle\\battle_scene_config.yaml");
        BattleWindowConfig battleWindowConfig = BattleWindowConfigManager.Load("Battle\\battle_window_config.yaml");
        CharacterBattleSpriteConfig characterBattleSpriteConfig = CharacterBattleSpriteConfigManager.Load("Battle\\character_battle_sprite_config.yaml");
        EnemyBattleSpriteConfig enemyBattleSpriteConfig = EnemyBattleSpriteConfigManager.Load("Battle\\enemy_battle_sprite_config.yaml");

        // Defaults.
        ChoiceBoxDefaults choiceBoxDefaults = ChoiceBoxDefaultsManager.Load("Defaults\\choice_box_defaults.yaml");
        GlobalMessageDefaults globalMessageDefaults = GlobalMessageDefaultsManager.Load("Defaults\\global_message_defaults.yaml");
        NameBoxDefaults nameBoxDefaults = NameBoxDefaultsManager.Load("Defaults\\name_box_defaults.yaml");
        TextWindowDefaults textWindowDefaults = TextWindowDefaultsManager.Load("Defaults\\text_window_defaults.yaml");

        // Game config.
        GameConfig gameConfig = GameConfigManager.Load("Game\\game_config.yaml");
        GameWindowConfig gameWindowConfig = GameWindowConfigManager.Load("Game\\game_window_config.yaml");
        GlobalMessageConfig globalMessageConfig = GlobalMessageConfigManager.Load("Game\\global_message_config.yaml");

        // Map config.
        AnimatedTileSheetConfig animatedTileSheetConfig = AnimatedTileSheetConfigManager.Load("Map\\animated_tilesheet_config.yaml");
        CharacterFieldSpriteConfig characterFieldSpriteConfig = CharacterFieldSpriteConfigManager.Load("Map\\character_field_sprite_config.yaml");
        MapBackgroundSpriteConfig mapBackgroundSpriteConfig = MapBackgroundSpriteConfigManager.Load("Map\\map_background_sprite_config.yaml");
        MapConfig mapConfig = MapConfigManager.Load("Map\\map_config.yaml");
        TileSheetConfig tileSheetConfig = TileSheetConfigManager.Load("Map\\tilesheet_config.yaml");

        // Menu Config.
        FileSelectorConfig fileSelectorConfig = FileSelectorConfigManager.Load("Menus\\file_selector_config.yaml");
        InventoryConfig inventoryConfig = InventoryConfigManager.Load("Menus\\inventory_config.yaml");
        MainMenuConfig mainMenuConfig = MainMenuConfigManager.Load("Menus\\main_menu_config.yaml");

        // Window Config.
        WindowConfig windowConfig = WindowConfigManager.Load("Windows\\windows_config.yaml");
        FontConfig fontConfig = FontConfigManager.Load("Windows\\font_config.yaml");

        // Create game context with neccesary arguments.
        GameContext gameContext = new(database, saveManager, assetManager, mapManager, animatedTileSheetConfig, battleBackgroundSpriteConfig, 
            battleSceneConfig, battleWindowConfig, characterFieldSpriteConfig, fileSelectorConfig, gameConfig, gameWindowConfig, inventoryConfig, 
            mapBackgroundSpriteConfig, mapConfig, mainMenuConfig, tileSheetConfig, windowConfig);
    }
}