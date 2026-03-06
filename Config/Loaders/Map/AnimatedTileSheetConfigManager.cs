namespace Config.Loaders.Map;

using Config.Runtime.Map;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

/// <summary>
/// The config manager class for the animated tilesheet config.
/// </summary>
public static class AnimatedTileSheetConfigManager {
    /// <summary>
    /// The load function for the animated tilesheet config manager.
    /// </summary>
    /// <param name="fileName">The filename of the config being opened.</param>
    /// <return>The animated tile sheet config object.</return>
    public static AnimatedTileSheetConfig Load(string fileName) {
        // Load the config file data.
        string yamlData = File.ReadAllText("Config\\yaml\\" + fileName);
        var deserializer = new DeserializerBuilder().WithNamingConvention(UnderscoredNamingConvention.Instance).Build();
        var config = deserializer.Deserialize<Dictionary<string, int>>(yamlData);

        // Store config variables.
        int animationInterval = config["animation_interval"];
        int animatedTileFrames = config["animated_tile_frames"];

        return new AnimatedTileSheetConfig(animationInterval, animatedTileFrames);
    }
}