using System.Text.Json;

using Data.Models.Maps;
using Infrastructure.Exceptions.Base;

namespace Game.Maps.Loader;

/// <summary>
/// Can load a new map state using a passed map name.
/// </summary>
public sealed class MapLoader 
{
    public MapModel LoadMap(string mapFilePath) 
    {
        try 
        {
            var fullRegistryPath = Path.Combine(
                AppContext.BaseDirectory,
                mapFilePath
            );
            var json = File.ReadAllText(fullRegistryPath);
            // TODO: (MLE-01) We can change this exception to a custom exception to specify the issue here.
            return JsonSerializer.Deserialize<MapModel>(json) ?? throw new Exception();
        }
        catch (Exception) 
        {
            throw new JsonFileException($"Map file: {mapFilePath} could not be found or contains an invalid format");
        }
    }
}
