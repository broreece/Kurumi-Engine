namespace Config.Loaders.Battle;

using Config.Runtime.Battle;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

/// <summary>
/// The config manager class for the character battle sprite config.
/// </summary>
public static class CharacterBattleSpriteConfigManager {
    /// <summary>
    /// The load function for the character battle sprite config manager.
    /// </summary>
    /// <param name="fileName">The filename of the config being opened.</param>
    /// <return>The character battle sprite config object.</return>
    public static CharacterBattleSpriteConfig Load(string fileName) {
        // Load the config file data.
        string yamlData = File.ReadAllText(fileName);
        var deserializer = new DeserializerBuilder().WithNamingConvention(UnderscoredNamingConvention.Instance).Build();
        var config = deserializer.Deserialize<Dictionary<string, int>>(yamlData);

        // Store config variables.
        int characterWidth = config["character_width"];
        int characterHeight = config["character_height"];

        return new CharacterBattleSpriteConfig(characterWidth, characterHeight);
    }
}