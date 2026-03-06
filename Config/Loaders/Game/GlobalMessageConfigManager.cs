namespace Config.Loaders.Game;

using Config.Runtime.Game;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

/// <summary>
/// The config manager class for the global message config.
/// </summary>
public static class GlobalMessageConfigManager {
    /// <summary>
    /// The load function for the global message config manager.
    /// </summary>
    /// <param name="fileName">The filename of the config being opened.</param>
    /// <return>The global message window config object.</return>
    public static GlobalMessageConfig Load(string fileName) {
        // Load the config file data.
        string yamlData = File.ReadAllText("Config\\yaml\\" + fileName);
        var deserializer = new DeserializerBuilder().WithNamingConvention(UnderscoredNamingConvention.Instance).Build();
        var config = deserializer.Deserialize<Dictionary<string, int>>(yamlData);

        // Store config variables.
        int globalMessageWindowId = config["global_message_window_id"];
        int globalMessageFont = config["global_message_window_font"];
        int globalMessageWidth = config["global_message_window_width"];
        int globalMessageHeight = config["global_message_window_height"];
        int globalMessageX = config["global_message_window_x"];
        int globalMessageY = config["global_message_window_y"];

        return new GlobalMessageConfig(globalMessageWindowId, globalMessageFont, globalMessageWidth, globalMessageHeight, 
            globalMessageX, globalMessageY);
    }
}