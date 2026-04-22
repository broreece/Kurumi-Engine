using Config.Loader;
using Config.Runtime.Battle;
using Config.Runtime.Defaults;
using Config.Runtime.Game;
using Config.Runtime.Map;
using Config.Runtime.Menus;
using Config.Runtime.Windows;

namespace Config.Core;

/// <summary>
/// Contains and initalizes all the configs for the engine.
/// </summary>
public sealed class ConfigProvider 
{
    private readonly string _configBasePath;

    // Battle config.
    public BattleBackgroundSpriteConfig BattleBackgroundSpriteConfig { get; }
    public BattleSceneConfig BattleSceneConfig { get; }
    public BattleWindowConfig BattleWindowConfig { get; }
    public CharacterBattleSpriteConfig CharacterBattleSpriteConfig { get; }
    public EnemyBattleSpriteConfig EnemyBattleSpriteConfig { get; }
    public PartyChoicesConfig PartyChoicesConfig { get; }

    // Defaults.
    public ChoiceBoxDefaults ChoiceBoxDefaults { get; }
    public GlobalMessageDefaults GlobalMessageDefaults { get; }
    public NameBoxDefaults NameBoxDefaults { get; }
    public TextWindowDefaults TextWindowDefaults { get; }

    // Game config.
    public GameConfig GameConfig { get; }
    public GameWindowConfig GameWindowConfig { get; }

    // Map config.
    public AnimatedTileSheetConfig AnimatedTileSheetConfig { get; }
    public CharacterFieldSpriteConfig CharacterFieldSpriteConfig { get; }
    public MapBackgroundSpriteConfig MapBackgroundSpriteConfig { get; }
    public MapConfig MapConfig { get; }
    public TileSheetConfig TileSheetConfig { get; }

    // Menu config.
    public FileSelectorConfig FileSelectorConfig { get; }
    public InventoryConfig InventoryConfig { get; }
    public MainMenuConfig MainMenuConfig { get; }

    // Window config.
    public WindowConfig WindowConfig { get; }

    public ConfigProvider(string configBasePath) 
    {
        _configBasePath = configBasePath;

        // Battle config.
        BattleBackgroundSpriteConfig = LoadConfig<BattleBackgroundSpriteConfig>(
            "Battle", 
            "battle_background_sprite_config.yaml"
        );
        BattleSceneConfig = LoadConfig<BattleSceneConfig>("Battle", "battle_scene_config.yaml");
        BattleWindowConfig = LoadConfig<BattleWindowConfig>("Battle", "battle_window_config.yaml");
        CharacterBattleSpriteConfig = LoadConfig<CharacterBattleSpriteConfig>(
            "Battle", 
            "character_battle_sprite_config.yaml"
        );
        EnemyBattleSpriteConfig = LoadConfig<EnemyBattleSpriteConfig>("Battle", "enemy_battle_sprite_config.yaml");
        PartyChoicesConfig = LoadConfig<PartyChoicesConfig>("Battle", "party_choices_config.yaml");

        // Defaults.
        ChoiceBoxDefaults = LoadConfig<ChoiceBoxDefaults>("Defaults", "choice_box_defaults.yaml");
        GlobalMessageDefaults = LoadConfig<GlobalMessageDefaults>("Defaults", "global_message_defaults.yaml");
        NameBoxDefaults = LoadConfig<NameBoxDefaults>("Defaults", "name_box_defaults.yaml");
        TextWindowDefaults = LoadConfig<TextWindowDefaults>("Defaults", "text_window_defaults.yaml");

        // Game config.
        GameConfig = LoadConfig<GameConfig>("Game", "game_config.yaml");
        GameWindowConfig = LoadConfig<GameWindowConfig>("Game", "game_window_config.yaml");

        // Map config.
        AnimatedTileSheetConfig = LoadConfig<AnimatedTileSheetConfig>("Map", "animated_tilesheet_config.yaml");
        CharacterFieldSpriteConfig = LoadConfig<CharacterFieldSpriteConfig>(
            "Map", 
            "character_field_sprite_config.yaml"
        );
        MapBackgroundSpriteConfig = LoadConfig<MapBackgroundSpriteConfig>("Map", "map_background_sprite_config.yaml");
        MapConfig = LoadConfig<MapConfig>("Map", "map_config.yaml");
        TileSheetConfig = LoadConfig<TileSheetConfig>("Map", "tilesheet_config.yaml");

        // Menu config.
        FileSelectorConfig = LoadConfig<FileSelectorConfig>("Menus", "file_selector_config.yaml");
        InventoryConfig = LoadConfig<InventoryConfig>("Menus", "inventory_config.yaml");
        MainMenuConfig = LoadConfig<MainMenuConfig>("Menus", "main_menu_config.yaml");

        // Window config.
        WindowConfig = LoadConfig<WindowConfig>("Windows", "windows_config.yaml");
    }

    private T LoadConfig<T>(string folder, string file) 
    {
        var path = Path.Combine(_configBasePath, folder, file);
        return ConfigLoader.Load<T>(path);
    }
}