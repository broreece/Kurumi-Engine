using System.Text.Json;

using Engine.Assets.Base;
using Infrastructure.Exceptions.Base;

using SFML.Graphics;

namespace Engine.Assets.Core;

/// <summary>
/// The asset manager class, loads and stores string file name locations for assets for dynamic loading in scenes.
/// </summary>
public sealed class AssetRegistry 
{
    private readonly Dictionary<AssetType, Dictionary<string, Texture>> _assets;

    public AssetRegistry(string registryPath) 
    {
        // TODO: (MLE-01) Handle exceptions here similar to map loader.
        // Load json file.
        var json = File.ReadAllText(registryPath);
        var stringDictionary = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, string>>>(json) ?? 
            throw new JsonFileException($"JSON file: {registryPath} is corrupted or incorrect format");
        
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
                    assetNamePair.Value
                )));
            }
        }
    }

    /// <summary>
    /// Getter for a specific asset texture using the asset type and the name of the asset.
    /// </summary>
    /// <param name="assetType">The type of asset.</param>
    /// <param name="assetName">The name of the asset.</param>
    /// <returns>The asset texture of a provided asset type and name.</returns>
    public Texture GetTexture(AssetType assetType, string assetName) => _assets[assetType][assetName];

    /// <summary>
    /// Function used to convert a string into an asset type.
    /// </summary>
    /// <param name="key">The string being converted.</param>
    /// <returns>An asset type enum.</returns>
    private static AssetType ParseAssetType(string key) 
    {
        // TODO: (MLE-01) Custom exception here.
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

            _ => throw new Exception($"Unknown asset type: {key}")
        };
    }
}
