namespace Config.Loaders.Battle;

using Config.Runtime.Battle;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

/// <summary>
/// The config manager class for the battle scene config.
/// </summary>
public static class BattleSceneConfigManager {
    /// <summary>
    /// The load function for the battle scene config manager.
    /// </summary>
    /// <param name="fileName">The filename of the config being opened.</param>
    /// <return>The battle scene config object.</return>
    public static BattleSceneConfig Load(string fileName) {
        // Load the config file data.
        string yamlData = File.ReadAllText(fileName);
        var deserializer = new DeserializerBuilder().WithNamingConvention(UnderscoredNamingConvention.Instance).Build();
        var config = deserializer.Deserialize<Dictionary<string, int>>(yamlData);

        // Store config variables.
        int damageDisplayLength = config["damage_display_length"];
        return new BattleSceneConfig(damageDisplayLength);
    }
}