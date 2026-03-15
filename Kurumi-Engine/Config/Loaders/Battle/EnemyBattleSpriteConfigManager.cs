namespace Config.Loaders.Battle;

using Config.Runtime.Battle;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

/// <summary>
/// The config manager class for the enemy battle sprite config.
/// </summary>
public static class EnemyBattleSpriteConfigManager {
    /// <summary>
    /// The load function for the enemy battle sprite config manager.
    /// </summary>
    /// <param name="fileName">The filename of the config being opened.</param>
    /// <return>The enemy battle sprite config object.</return>
    public static EnemyBattleSpriteConfig Load(string fileName) {
        // Load the config file data.
        string yamlData = File.ReadAllText(fileName);
        var deserializer = new DeserializerBuilder().WithNamingConvention(UnderscoredNamingConvention.Instance).Build();
        var config = deserializer.Deserialize<Dictionary<string, int>>(yamlData);

        // Store config variables.
        int enemyWidth = config["enemy_width"];
        int enemyHeight = config["enemy_height"];

        return new EnemyBattleSpriteConfig(enemyWidth, enemyHeight);
    }
}