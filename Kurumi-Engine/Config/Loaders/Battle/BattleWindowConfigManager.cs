namespace Config.Loaders.Battle;

using Config.Runtime.Battle;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

/// <summary>
/// The config manager class for the battle window config.
/// </summary>
public static class BattleWindowConfigManager {
    /// <summary>
    /// The load function for the battle window config manager.
    /// </summary>
    /// <param name="fileName">The filename of the config being opened.</param>
    /// <return>The battle window config object.</return>
    public static BattleWindowConfig Load(string fileName) {
        // Load the config file data.
        string yamlData = File.ReadAllText(fileName);
        var deserializer = new DeserializerBuilder().WithNamingConvention(UnderscoredNamingConvention.Instance).Build();
        var config = deserializer.Deserialize<Dictionary<string, int>>(yamlData);

        // Store config variables.
        int windowId = config["window_id"];
        int choiceBoxId = config["choice_box_id"];
        int fontId = config["font_id"];
        int fontSize = config["font_size"];
        int infoWindowWidth = config["info_window_width"];
        int infoWindowHeight = config["info_window_height"];
        int infoWindowX = config["info_window_x"];
        int infoWindowY = config["info_window_y"];
        int selectionWindowWidth = config["selection_window_width"];
        int selectionWindowHeight = config["selection_window_height"];
        int selectionWindowX = config["selection_window_x"];
        int selectionWindowY = config["selection_window_y"];
        int partyX = config["party_x_placement"];
        int partyY = config["party_y_placement"];

        return new BattleWindowConfig(windowId, choiceBoxId, fontId, fontSize, infoWindowWidth, infoWindowHeight, 
            infoWindowX, infoWindowY, selectionWindowWidth, selectionWindowHeight, selectionWindowX, selectionWindowY, 
            partyX, partyY);
    }
}