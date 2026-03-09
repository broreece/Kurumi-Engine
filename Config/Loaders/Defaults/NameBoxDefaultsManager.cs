namespace Config.Loaders.Defaults;

using Config.Runtime.Defaults;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

/// <summary>
/// The defaults manager class for the name box defaults.
/// </summary>
public static class NameBoxDefaultsManager {
    /// <summary>
    /// The load function for the name box defaults manager.
    /// </summary>
    /// <param name="fileName">The filename of the defaults being opened.</param>
    public static NameBoxDefaults Load(string fileName) {
        // Load the config file data.
        string yamlData = File.ReadAllText("Config\\yaml\\" + fileName);
        var deserializer = new DeserializerBuilder().WithNamingConvention(UnderscoredNamingConvention.Instance).Build();
        var defaults = deserializer.Deserialize<Dictionary<string, int>>(yamlData);

        // Store defaults variables.
        int nameBoxWidth = defaults["name_box_width"];
        int nameBoxHeight = defaults["name_box_height"];;
        int nameBoxX = defaults["name_box_x"];
        int nameBoxY = defaults["name_box_y"];

        return new NameBoxDefaults(nameBoxWidth, nameBoxHeight, nameBoxX, nameBoxY);
    }
}