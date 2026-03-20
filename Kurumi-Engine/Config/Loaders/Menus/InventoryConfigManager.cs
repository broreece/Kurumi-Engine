namespace Config.Loaders.Menus;

using Config.Runtime.Menus;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

/// <summary>
/// The config manager class for the inventory config.
/// </summary>
public static class InventoryConfigManager {
    /// <summary>
    /// The load function for the inventory config manager.
    /// </summary>
    /// <param name="fileName">The filename of the config being opened.</param>
    /// <return>The animated tile sheet config object.</return>
    public static InventoryConfig Load(string fileName) {
        // Load the config file data.
        string yamlData = File.ReadAllText(fileName);
        var deserializer = new DeserializerBuilder().WithNamingConvention(UnderscoredNamingConvention.Instance).Build();
        var config = deserializer.Deserialize<Dictionary<string, int>>(yamlData);

        // Store config variables.
        int inventoryWindowId = Convert.ToInt32(config["inventory_window_id"]);
        int inventoryChoiceBoxId = Convert.ToInt32(config["inventory_choice_box_id"]);
        int fontId = Convert.ToInt32(config["font_id"]);
        int fontSize = Convert.ToInt32(config["font_size"]);
        int inventoryWindowWidth = Convert.ToInt32(config["inventory_items_window_width"]);
        int inventoryWindowHeight = Convert.ToInt32(config["inventory_items_window_height"]);
        int inventoryWindowX = Convert.ToInt32(config["inventory_items_window_x"]);
        int inventoryWindowY = Convert.ToInt32(config["inventory_items_window_y"]);
        int descriptionWindowWidth = Convert.ToInt32(config["inventory_item_description_width"]);
        int descriptionWindowHeight = Convert.ToInt32(config["inventory_item_description_height"]);
        int descriptionWindowX = Convert.ToInt32(config["inventory_item_description_x"]);
        int descriptionWindowY = Convert.ToInt32(config["inventory_item_description_y"]);

        return new InventoryConfig(inventoryWindowId, inventoryChoiceBoxId, fontId, fontSize, inventoryWindowWidth, 
            inventoryWindowHeight, inventoryWindowX, inventoryWindowY, descriptionWindowWidth, descriptionWindowHeight, 
            descriptionWindowX, descriptionWindowY);
    }
}