namespace Config.Loaders.Defaults;

using Config.Runtime.Defaults;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

/// <summary>
/// The defaults manager class for the text window defaults.
/// </summary>
public static class TextWindowDefaultsManager {
    /// <summary>
    /// The load function for the text window defaults manager.
    /// </summary>
    /// <param name="fileName">The filename of the defaults being opened.</param>
    public static TextWindowDefaults Load(string fileName) {
        // Load the config file data.
        string yamlData = File.ReadAllText("Config\\yaml\\" + fileName);
        var deserializer = new DeserializerBuilder().WithNamingConvention(UnderscoredNamingConvention.Instance).Build();
        var defaults = deserializer.Deserialize<Dictionary<string, int>>(yamlData);

        // Store defaults variables.
        int textWindowWidth = defaults["window_default_width"];
        int textWindowHeight = defaults["window_default_height"];;
        int textWindowX = defaults["window_default_x"];
        int textWindowY = defaults["window_default_y"];

        return new TextWindowDefaults(textWindowWidth, textWindowHeight, textWindowX, textWindowY);
    }
}