using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Config.Loader;

/// <summary>
/// Contains functionality to load and deserialize 
/// </summary>
public static class ConfigLoader 
{
    private static readonly IDeserializer _deserializer =
        new DeserializerBuilder().WithNamingConvention(UnderscoredNamingConvention.Instance).Build();

    /// <summary>
    /// Function that takes a file path and returns a deserialized config object stored at the file path.
    /// </summary>
    /// <param name="filePath">The full path to the config yaml file.</param>
    /// <typeparam name="T">The type of config being loaded.</typeparam>
    /// <returns>A new config of the type of the load function used.</returns>
    public static T Load<T>(string filePath) 
    {
        var yaml = File.ReadAllText(filePath);
        return _deserializer.Deserialize<T>(yaml);
    }
}