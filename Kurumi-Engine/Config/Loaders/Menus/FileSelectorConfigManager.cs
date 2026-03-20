namespace Config.Loaders.Menus;

using Config.Runtime.Menus;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

/// <summary>
/// The config manager class for the file selector config.
/// </summary>
public static class FileSelectorConfigManager {
    /// <summary>
    /// The load function for the file selector config manager.
    /// </summary>
    /// <param name="fileName">The filename of the config being opened.</param>
    /// <return>The animated tile sheet config object.</return>
    public static FileSelectorConfig Load(string fileName) {
        // Load the config file data.
        string yamlData = File.ReadAllText(fileName);
        var deserializer = new DeserializerBuilder().WithNamingConvention(UnderscoredNamingConvention.Instance).Build();
        var config = deserializer.Deserialize<Dictionary<string, object>>(yamlData);

        // Store config variables.
        int fileSelectorWindowId = Convert.ToInt32(config["file_selector_window_id"]);
        int fileSelectorChoiceBoxId = Convert.ToInt32(config["file_selector_choice_box_id"]);
        int fontId = Convert.ToInt32(config["font_id"]);
        int fontSize = Convert.ToInt32(config["font_size"]);
        int maxFilesOneScreen = Convert.ToInt32(config["file_selector_max_saves_one_screen"]);
        int fileMessageWindowWidth = Convert.ToInt32(config["file_selector_message_window_width"]);
        int fileMessageWindowHeight = Convert.ToInt32(config["file_selector_message_window_height"]);
        int fileMessageWindowX = Convert.ToInt32(config["file_selector_message_window_x"]);
        int fileMessageWindowY = Convert.ToInt32(config["file_selector_message_window_y"]);
        string? saveMessageNullable = config["file_selector_save_message"].ToString();
        string? loadMessageNullable = config["file_selector_load_message"].ToString();
        int warningMessageWindowWidth = Convert.ToInt32(config["file_selector_warning_window_width"]);
        int warningMessageWindowHeight = Convert.ToInt32(config["file_selector_warning_window_height"]);
        int warningMessageWindowX = Convert.ToInt32(config["file_selector_warning_window_x"]);
        int warningMessageWindowY = Convert.ToInt32(config["file_selector_warning_window_y"]);
        string? warningMessageNullable = config["file_selector_warning_message"].ToString();
        int warningChoiceWindowWidth = Convert.ToInt32(config["file_selector_warning_choice_width"]);
        int warningChoiceWindowHeight = Convert.ToInt32(config["file_selector_warning_choice_height"]);
        int warningChoiceWindowX = Convert.ToInt32(config["file_selector_warning_choice_x"]);
        int warningChoiceWindowY = Convert.ToInt32(config["file_selector_warning_choice_y"]);

        // Check for string null error here as we don't want any warnings.
        string saveMessage = saveMessageNullable ?? "";
        string loadMessage = loadMessageNullable ?? "";
        string warningMessage = warningMessageNullable ?? "";
        return new FileSelectorConfig(fileSelectorWindowId, fileSelectorChoiceBoxId, fontId, fontSize, 
            maxFilesOneScreen, fileMessageWindowWidth, fileMessageWindowHeight, fileMessageWindowX, fileMessageWindowY, 
            saveMessage, loadMessage, warningMessageWindowWidth, warningMessageWindowHeight, warningMessageWindowX, 
            warningMessageWindowY, warningMessage, warningChoiceWindowWidth, warningChoiceWindowHeight, 
            warningChoiceWindowX, warningChoiceWindowY);
    }
}