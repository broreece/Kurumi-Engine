using System.Text.Json;

using Engine.Assets.Base;
using Engine.Assets.Exceptions;

using Infrastructure.Exceptions.Base;

using SFML.Graphics;

namespace Engine.Assets.Core;

/// <summary>
/// The asset manager class, loads and stores string file name locations for assets for dynamic loading in scenes.
/// </summary>
public sealed class AssetRegistry 
{
    private readonly Dictionary<AssetType, Dictionary<string, Texture>> _assets;
    private readonly Dictionary<string, Font> _fonts;

    private static readonly Dictionary<AssetType, string> _assetFolders = new()
    {
        [AssetType.ActorSpriteSheets] = Path.Combine("Assets", "Art", "ActorSpriteSheets"),
        [AssetType.AnimatedTileSpriteSheets] = Path.Combine("Assets", "Art", "AnimatedTileSpriteSheets"),
        [AssetType.BattleBackgroundArt] = Path.Combine("Assets", "Art", "BattleBackgroundArt"),
        [AssetType.CharacterBattleSpriteSheets] = Path.Combine("Assets", "Art", "CharacterBattleSpriteSheets"),
        [AssetType.CharacterFieldSpriteSheets] = Path.Combine("Assets", "Art", "CharacterFieldSpriteSheets"),
        [AssetType.EnemyBattleSprites] = Path.Combine("Assets", "Art", "EnemyBattleSprites"),
        [AssetType.MapBackgroundArt] = Path.Combine("Assets", "Art", "MapBackgroundArt"),
        [AssetType.TileSpriteSheets] = Path.Combine("Assets", "Art", "TileSpriteSheets"),
        [AssetType.Windows] = Path.Combine("Assets", "Art", "Windows", "WindowArt"),
        [AssetType.ChoiceSelectionArt] = Path.Combine("Assets", "Art", "Windows", "ChoiceSelectionArt")
    };
    private static readonly string _fontFolder = Path.Combine("Assets", "Fonts");

    public AssetRegistry(string registryPath, string fontRegistryPath) 
    {
        // Load main registry file.
        try 
        {
            var json = File.ReadAllText(registryPath);
            var stringDictionary = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, string>>>(json) ?? 
                throw new RegistryFormatException($"JSON file: {registryPath} is incorrect format");

            // Convert string dictionary to asset type enum dictionary with textures.
            _assets = [];
            foreach (var jsonDictionaryPair in stringDictionary) {
                var assetType = ParseAssetType(jsonDictionaryPair.Key);
                // Create sub dictionary for every type.
                var subDictionary = new Dictionary<string, Texture>();
                _assets.Add(assetType, subDictionary);

                foreach (var assetNamePair in jsonDictionaryPair.Value) {
                    // Add the texture to the sub dictionary.
                    subDictionary.Add(assetNamePair.Key, new Texture(Path.Combine(
                        AppContext.BaseDirectory,
                        _assetFolders[assetType],
                        assetNamePair.Value
                    )));
                }
            }

            // Load font registry file.
            json = File.ReadAllText(fontRegistryPath);
            var fontStringDictionary = JsonSerializer.Deserialize<Dictionary<string, string>>(json) ?? 
                throw new JsonFileException($"JSON file: {fontRegistryPath} is corrupted or incorrect format");
            _fonts = [];
            foreach (var jsonDictionaryPair in fontStringDictionary) {
                _fonts.Add(jsonDictionaryPair.Key, new Font(Path.Combine(
                    AppContext.BaseDirectory,
                    _fontFolder,
                    jsonDictionaryPair.Value
                )));
            }
        } 
        catch (Exception exception) when (exception is not RegistryFormatException) 
        {
            throw new JsonFileException($"Registry path: {registryPath} was not found, or an error occured opening " + 
                "the file.");
        }
    }

    /// <summary>
    /// Getter for a specific asset texture using the asset type and the name of the asset.
    /// </summary>
    /// <param name="assetType">The type of asset.</param>
    /// <param name="assetName">The name of the asset.</param>
    /// <returns>The asset texture of a provided asset type and name.</returns>
    public Texture GetTexture(AssetType assetType, string assetName) => _assets[assetType][assetName];

    public Font GetFont(string fontName) => _fonts[fontName];

    /// <summary>
    /// Function used to convert a string into an asset type.
    /// </summary>
    /// <param name="key">The string being converted.</param>
    /// <returns>An asset type enum.</returns>
    /// <exception cref="AssetTypeInvalidException">Exception thrown if a provided asset type is invalid.</exception>
    private static AssetType ParseAssetType(string key) 
    {
        return key switch {
            "actor_sprite_sheets" => AssetType.ActorSpriteSheets,
            "animated_tile_sprite_sheets" => AssetType.AnimatedTileSpriteSheets,
            "battle_background_art" => AssetType.BattleBackgroundArt,
            "character_battle_sprite_sheets" => AssetType.CharacterBattleSpriteSheets,
            "character_field_sprite_sheets" => AssetType.CharacterFieldSpriteSheets,
            "enemy_battle_sprites" => AssetType.EnemyBattleSprites,
            "map_background_art" => AssetType.MapBackgroundArt,
            "tile_sprite_sheets" => AssetType.TileSpriteSheets,
            "windows" => AssetType.Windows,
            "choice_selection_art" => AssetType.ChoiceSelectionArt,

            _ => throw new AssetTypeInvalidException($"Unknown asset type: {key}")
        };
    }
}
