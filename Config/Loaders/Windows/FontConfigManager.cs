namespace Config.Loaders.Windows;

using Config.Runtime.Windows;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

/// <summary>
/// The config manager class for the font config.
/// </summary>
public static class FontConfigManager {
    /// <summary>
    /// The load function for the font config manager.
    /// </summary>
    /// <param name="fileName">The filename of the config being opened.</param>
    public static FontConfig Load(string fileName) {
        // Load the config file data.
        string yamlData = File.ReadAllText("Config\\yaml\\" + fileName);
        var deserializer = new DeserializerBuilder().WithNamingConvention(UnderscoredNamingConvention.Instance).Build();
        var config = deserializer.Deserialize<Dictionary<string, int>>(yamlData);

        // Store config variables.
        int fontSize = config["font_size"];

        return new FontConfig(fontSize);
    }
}