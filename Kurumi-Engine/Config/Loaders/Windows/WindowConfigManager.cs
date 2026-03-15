namespace Config.Loaders.Windows;

using Config.Runtime.Windows;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

/// <summary>
/// The config manager class for the window config.
/// </summary>
public static class WindowConfigManager {
    /// <summary>
    /// The load function for the window config manager.
    /// </summary>
    /// /// <param name="fileName">The filename of the config being opened.</param>
    public static WindowConfig Load(string fileName) {
        // Load the config file data.
        string yamlData = File.ReadAllText(fileName);
        var deserializer = new DeserializerBuilder().WithNamingConvention(UnderscoredNamingConvention.Instance).Build();
        var config = deserializer.Deserialize<Dictionary<string, int>>(yamlData);

        // Store config variables.
        int windowFileWidth = config["window_file_width"];
        int windowFileHeight = config["window_file_height"];
        int maxLinesPerWindow = config["max_lines_per_window"];
        int choiceSelectionFileWidth = config["choice_selection_file_width"];
        int choiceSelectionFileHeight = config["choice_selection_file_height"];

        return new WindowConfig(windowFileWidth, windowFileHeight, maxLinesPerWindow, choiceSelectionFileWidth, choiceSelectionFileHeight);
    }
}