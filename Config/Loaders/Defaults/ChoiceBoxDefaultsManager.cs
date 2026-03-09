namespace Config.Loaders.Defaults;

using Config.Runtime.Defaults;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

/// <summary>
/// The defaults manager class for the choice box defaults.
/// </summary>
public static class ChoiceBoxDefaultsManager {
    /// <summary>
    /// The load function for the choice box defaults manager.
    /// </summary>
    /// <param name="fileName">The filename of the defaults being opened.</param>
    public static ChoiceBoxDefaults Load(string fileName) {
        // Load the config file data.
        string yamlData = File.ReadAllText("Config\\yaml\\" + fileName);
        var deserializer = new DeserializerBuilder().WithNamingConvention(UnderscoredNamingConvention.Instance).Build();
        var defaults = deserializer.Deserialize<Dictionary<string, int>>(yamlData);

        // Store defaults variables.
        int windowId = defaults["window_id"];
        int selectionId = defaults["selection_id"];
        int fontId = defaults["font_id"];
        int fontSize = defaults["font_size"];
        int choiceBoxWidth = defaults["choice_box_width"];
        int choiceBoxHeight = defaults["choice_box_height"];
        int choiceBoxX = defaults["choice_box_x"];
        int choiceBoxY = defaults["choice_box_y"];

        return new ChoiceBoxDefaults(windowId, selectionId, fontId, fontSize, choiceBoxWidth, choiceBoxHeight, choiceBoxX, choiceBoxY);
    }
}