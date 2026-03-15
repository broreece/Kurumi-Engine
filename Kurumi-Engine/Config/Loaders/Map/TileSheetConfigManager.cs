namespace Config.Loaders.Map;

using Config.Runtime.Map;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

/// <summary>
/// The config manager class for the tile sheet sprite config.
/// </summary>
public static class TileSheetConfigManager {
    /// <summary>
    /// The load function for the tile sheet sprite config manager.
    /// </summary>
    /// <param name="fileName">The filename of the config being opened.</param>
    /// <return>The tile sheet config object.</return>
    public static TileSheetConfig Load(string fileName) {
        // Load the config file data.
        string yamlData = File.ReadAllText(fileName);
        var deserializer = new DeserializerBuilder().WithNamingConvention(UnderscoredNamingConvention.Instance).Build();
        var config = deserializer.Deserialize<Dictionary<string, int>>(yamlData);

        // Store config variables.
        int tileWidth = config["tile_width"];
        int tileHeight = config["tile_height"];
        int tileSheetMaxTilesWide = config["tile_sheet_max_tiles_wide"];

        return new TileSheetConfig(tileWidth, tileHeight, tileSheetMaxTilesWide);
    }
}