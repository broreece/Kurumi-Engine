using System.Text.Json;

using Infrastructure.Exceptions.Base;

namespace Game.Maps.Registry;

/// <summary>
/// Loads and stores string file name locations for maps.
/// </summary>
public sealed class MapRegistry 
{
    private readonly IReadOnlyDictionary<string, string> _mapFileNames;

    public MapRegistry(string registryPath) 
    {
        // Load json file.
        try 
        {
            var json = File.ReadAllText(registryPath);
            _mapFileNames = JsonSerializer.Deserialize<IReadOnlyDictionary<string, string>>(json) ?? 
                throw new RegistryFormatException($"JSON file: {registryPath} is incorrect format");
        } 
        catch (Exception exception) when (exception is not RegistryFormatException) 
        {
            throw new JsonFileException($"Registry path: {registryPath} was not found, or an error occured opening " + 
                "the file.");
        }
    }

    public string GetMapFileName(string mapName) => _mapFileNames[mapName];
}