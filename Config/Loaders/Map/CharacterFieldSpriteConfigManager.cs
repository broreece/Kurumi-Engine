namespace Config.Loaders.Map;

using Config.Runtime.Map;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

/// <summary>
/// The config manager class for the character field sprite config.
/// </summary>
public static class CharacterFieldSpriteConfigManager {
    /// <summary>
    /// The load function for the character field sprite config manager.
    /// </summary>
    /// <param name="fileName">The filename of the config being opened.</param>
    /// <return>The character field sprite config object.</return>
    public static CharacterFieldSpriteConfig Load(string fileName) {
        // Load the config file data.
        string yamlData = File.ReadAllText("Config\\yaml\\" + fileName);
        var deserializer = new DeserializerBuilder().WithNamingConvention(UnderscoredNamingConvention.Instance).Build();
        var config = deserializer.Deserialize<Dictionary<string, int>>(yamlData);

        // Store config variables.
        int characterWidth = config["character_width"];
        int characterHeight = config["character_height"];
        int walkAnimationFrames = config["walk_animation_frames"];
        int walkAnimationSpeed = config["walk_animation_speed"];

        return new CharacterFieldSpriteConfig(characterWidth, characterHeight, walkAnimationFrames, walkAnimationSpeed);
    }
}