namespace Config.Loaders.Game;

using Config.Runtime.Game;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

/// <summary>
/// The config manager class for the game window config.
/// </summary>
public static class GameWindowConfigManager {
    /// <summary>
    /// The load function for the game window config manager.
    /// </summary>
    /// <param name="fileName">The filename of the config being opened.</param>
    /// <return>The game window config object.</return>
    public static GameWindowConfig Load(string fileName) {
        // Load the config file data.
        string yamlData = File.ReadAllText(fileName);
        var deserializer = new DeserializerBuilder().WithNamingConvention(UnderscoredNamingConvention.Instance).Build();
        var config = deserializer.Deserialize<Dictionary<string, string>>(yamlData);

        // Store config variables.
        int windowWidth = int.Parse(config["window_width"]);
        int windowHeight = int.Parse(config["window_height"]);
        string windowTitle = config["window_title"];

        return new GameWindowConfig(windowWidth, windowHeight, windowTitle);
    }
}