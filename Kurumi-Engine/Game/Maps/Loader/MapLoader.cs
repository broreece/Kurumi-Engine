using System.Text.Json;

using Data.Models.Maps;

using Game.Maps.Exceptions;

using Infrastructure.Exceptions.Base;

namespace Game.Maps.Loader;

/// <summary>
/// Can load a new map state using a passed map name.
/// </summary>
public sealed class MapLoader 
{
    private static readonly string _mapFolder = "Assets\\Maps";

    public MapModel LoadMap(string mapFilePath) 
    {
        try 
        {
            var mapPath = Path.Combine(
                AppContext.BaseDirectory,
                _mapFolder,
                mapFilePath
            );
            var json = File.ReadAllText(mapPath);
            return JsonSerializer.Deserialize<MapModel>(json) ?? 
                throw new InvalidMapFormatException($"The map at: {mapPath} was in an invalid format.");
        }
        catch (Exception exception) when (exception is not InvalidMapFormatException) 
        {
            throw new JsonFileException($"Map at: {mapFilePath} was not found, or an error occured opening " + 
                "the file.");
        }
    }
}
