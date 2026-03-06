namespace Config.Loaders.Battle;

using Config.Runtime.Battle;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

/// <summary>
/// The config manager class for the battle background sprite config.
/// </summary>
public static class BattleBackgroundSpriteConfigManager {
    /// <summary>
    /// The load function for the battle background sprite config manager.
    /// </summary>
    /// <param name="fileName">The filename of the config being opened.</param>
    /// <return>The battle background config object.</return>
    public static BattleBackgroundSpriteConfig Load(string fileName) {
        // Load the config file data.
        string yamlData = File.ReadAllText("Config\\yaml\\" + fileName);
        var deserializer = new DeserializerBuilder().WithNamingConvention(UnderscoredNamingConvention.Instance).Build();
        var config = deserializer.Deserialize<Dictionary<string, int>>(yamlData);

        // Store config variables.
        int battleBackgroundWidth = config["battle_background_width"];
        int battleBackgroundHeight = config["battle_background_height"];

        return new BattleBackgroundSpriteConfig(battleBackgroundWidth, battleBackgroundHeight);
    }
}