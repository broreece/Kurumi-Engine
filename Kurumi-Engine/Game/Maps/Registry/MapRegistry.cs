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
        // TODO: (MLE-01) Specify exception here..
        try 
        {
            var json = File.ReadAllText(registryPath);
            _mapFileNames = JsonSerializer.Deserialize<IReadOnlyDictionary<string, string>>(json) ?? 
                throw new Exception();
        } 
        catch (Exception) 
        {
            throw new JsonFileException($"Registry path: {registryPath} not found or invalid format");
        }
    }

    public string GetMapFileName(string mapName) => _mapFileNames[mapName];
}