namespace Config.Loaders.Map;

using Config.Runtime.Map;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

/// <summary>
/// The config manager class for the map config.
/// </summary>
public static class MapConfigManager {
    /// <summary>
    /// The load function for the map config manager.
    /// </summary>
    /// <param name="fileName">The filename of the config being opened.</param>
    /// <return>The map config object.</return>
    public static MapConfig Load(string fileName) {
        // Load the config file data.
        string yamlData = File.ReadAllText("Config\\yaml\\" + fileName);
        var deserializer = new DeserializerBuilder().WithNamingConvention(UnderscoredNamingConvention.Instance).Build();
        var config = deserializer.Deserialize<Dictionary<string, int>>(yamlData);

        // Store config variables.
        int maxTilesWide = config["max_tiles_wide"];
        int maxTilesHigh = config["max_tiles_high"];
        int randomActorMoveVariance = config["random_actor_move_variance"];

        return new MapConfig(maxTilesWide, maxTilesHigh, randomActorMoveVariance);
    }
}