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
        int choiceBoxWidth = defaults["choice_box_default_width"];
        int choiceBoxHeight = defaults["choice_box_default_height"];;
        int choiceBoxX = defaults["choice_box_default_x"];
        int choiceBoxY = defaults["choice_box_default_y"];

        return new ChoiceBoxDefaults(choiceBoxWidth, choiceBoxHeight, choiceBoxX, choiceBoxY);
    }
}