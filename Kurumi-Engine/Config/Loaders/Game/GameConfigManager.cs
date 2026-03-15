namespace Config.Loaders.Game;

using Config.Runtime.Game;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

/// <summary>
/// The config manager class for the game config.
/// </summary>
public static class GameConfigManager {
    /// <summary>
    /// The load function for the game config manager.
    /// </summary>
    /// <param name="fileName">The filename of the config being opened.</param>
    /// <return>The game config object.</return>
    public static GameConfig Load(string fileName) {
        // Load the config file data.
        string yamlData = File.ReadAllText(fileName);
        var deserializer = new DeserializerBuilder().WithNamingConvention(UnderscoredNamingConvention.Instance).Build();
        var config = deserializer.Deserialize<Dictionary<string, int>>(yamlData);

        // Store config variables.
        int maxPartySize = config["max_party_size"];
        int maxEnemyFormationSize = config["max_enemy_formation_size"];
        int saveFiles = config["save_files"];
        int agilityStatIndex = config["agility_stat_index"];

        return new GameConfig(maxPartySize, maxEnemyFormationSize, saveFiles, agilityStatIndex);
    }
}