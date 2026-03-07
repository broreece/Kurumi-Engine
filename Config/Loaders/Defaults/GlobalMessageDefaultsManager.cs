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
        string yamlData = File.ReadAllText("Config\\yaml\\" + fileName);
        var deserializer = new DeserializerBuilder().WithNamingConvention(UnderscoredNamingConvention.Instance).Build();
        var defaults = deserializer.Deserialize<Dictionary<string, int>>(yamlData);

        // Store defaults variables.
        int globalMessageWindowId = defaults["global_message_default_window_id"];
        int globalMessageFontId = defaults["global_message_default_font_id"];
        int globalMessageFontSize = defaults["global_message_default_font_size"];
        int globalMessageWidth = defaults["global_message_default_width"];
        int globalMessageHeight = defaults["global_message_default_height"];;
        int globalMessageX = defaults["global_message_default_x"];
        int globalMessageY = defaults["global_message_default_y"];

        return new GlobalMessageDefaults(globalMessageWindowId, globalMessageFontId, globalMessageFontSize, globalMessageWidth, 
            globalMessageHeight, globalMessageX, globalMessageY);
    }
}