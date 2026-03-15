namespace Config.Loaders.Defaults;

using Config.Runtime.Defaults;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

/// <summary>
/// The defaults manager class for the global message defaults.
/// </summary>
public static class GlobalMessageDefaultsManager {
    /// <summary>
    /// The load function for the global message defaults manager.
    /// </summary>
    /// <param name="fileName">The filename of the defaults being opened.</param>
    public static GlobalMessageDefaults Load(string fileName) {
        // Load the config file data.
        string yamlData = File.ReadAllText(fileName);
        var deserializer = new DeserializerBuilder().WithNamingConvention(UnderscoredNamingConvention.Instance).Build();
        var defaults = deserializer.Deserialize<Dictionary<string, int>>(yamlData);

        // Store defaults variables.
        int windowId = defaults["window_id"];
        int fontId = defaults["font_id"];
        int fontSize = defaults["font_size"];
        int windowWidth = defaults["width"];
        int windowHeight = defaults["height"];;
        int windowX = defaults["window_x"];
        int windowY = defaults["window_y"];

        return new GlobalMessageDefaults(windowId, fontId, fontSize, windowWidth, windowHeight, windowX, windowY);
    }
}