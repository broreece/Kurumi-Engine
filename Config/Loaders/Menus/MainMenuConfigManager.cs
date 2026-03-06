namespace Config.Loaders.Menus;

using Config.Runtime.Menus;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

/// <summary>
/// The config manager class for the main menu config.
/// </summary>
public static class MainMenuConfigManager {
    /// <summary>
    /// The load function for the main menu config manager.
    /// </summary>
    /// <param name="fileName">The filename of the config being opened.</param>
    /// <return>The animated tile sheet config object.</return>
    public static MainMenuConfig Load(string fileName) {
        // Load the config file data.
        string yamlData = File.ReadAllText("Config\\yaml\\" + fileName);
        var deserializer = new DeserializerBuilder().WithNamingConvention(UnderscoredNamingConvention.Instance).Build();
        var config = deserializer.Deserialize<Dictionary<string, int>>(yamlData);

        // Store config variables.
        int windowId = Convert.ToInt32(config["window_id"]);
        int choiceBoxId = Convert.ToInt32(config["choice_box_id"]);
        int fontId = Convert.ToInt32(config["font_id"]);
        int fontSize = Convert.ToInt32(config["font_size"]);
        int selectionWindowWidth = Convert.ToInt32(config["selection_window_width"]);
        int selectionWindowHeight = Convert.ToInt32(config["selection_window_height"]);
        int selectionWindowX = Convert.ToInt32(config["selection_window_x"]);
        int selectionWindowY = Convert.ToInt32(config["selection_window_y"]);
        int selectionWindowSpacing = Convert.ToInt32(config["selection_window_spacing"]);
        int infoWindowWidth = Convert.ToInt32(config["info_window_width"]);
        int infoWindowHeight = Convert.ToInt32(config["info_window_height"]);
        int infoWindowX = Convert.ToInt32(config["info_window_x"]);
        int infoWindowY = Convert.ToInt32(config["info_window_y"]);
        int partyWindowWidth = Convert.ToInt32(config["party_window_width"]);
        int partyWindowHeight = Convert.ToInt32(config["party_window_height"]);
        int partyWindowX = Convert.ToInt32(config["party_window_x"]);
        int partyWindowY = Convert.ToInt32(config["party_window_y"]);

        return new MainMenuConfig(windowId, choiceBoxId, fontId, fontSize, selectionWindowWidth, selectionWindowHeight, selectionWindowX, 
            selectionWindowY, selectionWindowSpacing, infoWindowWidth, infoWindowHeight, infoWindowX, infoWindowY, partyWindowWidth, 
            partyWindowHeight, partyWindowX, partyWindowY);
    }
}