namespace Config.Loaders.Map;

using Config.Runtime.Map;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

/// <summary>
/// The config manager class for the map background sprite config.
/// </summary>
public static class MapBackgroundSpriteConfigManager {
    /// <summary>
    /// The load function for the map background sprite config manager.
    /// </summary>
    /// <param name="fileName">The filename of the config being opened.</param>
    /// <return>The map background sprite config object.</return>
    public static MapBackgroundSpriteConfig Load(string fileName) {
        // Load the config file data.
        string yamlData = File.ReadAllText(fileName);
        var deserializer = new DeserializerBuilder().WithNamingConvention(UnderscoredNamingConvention.Instance).Build();
        var config = deserializer.Deserialize<Dictionary<string, int>>(yamlData);

        // Store config variables.
        int width = config["width"];
        int height = config["height"];

        return new MapBackgroundSpriteConfig(width, height);
    }
}