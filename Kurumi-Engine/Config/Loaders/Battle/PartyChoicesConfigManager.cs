namespace Config.Loaders.Battle;

using Config.Runtime.Battle;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

/// <summary>
/// The config manager class for the party choices battle config.
/// </summary>
public static class PartyChoicesConfigManager {
    /// <summary>
    /// The load function for the party choices battle config manager.
    /// </summary>
    /// <param name="fileName">The filename of the config being opened.</param>
    /// <return>The party choices battle config object.</return>
    public static PartyChoicesConfig Load(string fileName) {
        // Load the config file data.
        string yamlData = File.ReadAllText(fileName);
        var deserializer = new DeserializerBuilder().WithNamingConvention(UnderscoredNamingConvention.Instance).Build();
        var config = deserializer.Deserialize<Dictionary<string, string>>(yamlData);

        // Store config variables.
        bool itemsEnabled = int.Parse(config["items_enabled"]) == 1;
        bool runAwayEnabled = int.Parse(config["run_away_enabled"]) == 1;
        string itemsText = config["items_text"];
        string runAwayText = config["run_away_text"];

        return new PartyChoicesConfig(itemsEnabled, runAwayEnabled, itemsText, runAwayText);
    }
}
