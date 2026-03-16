namespace Engine.Maps.Core;

using Engine.Maps.Serialization;
using Game.Map.Actors.ActorTypes;
using Game.Map.Actors.Base;
using Game.Map.Core;
using Game.Party;
using Game.Map.Tiles;
using Registry.TileObjects;
using Utils.Exceptions;
using System.Text.Json;
using Registry.Actors;

/// <summary>
/// The map manager class, loads and stores string file name locations for maps.
/// </summary>
public sealed class MapManager {
    public MapManager(string registryPath) {
        // Load json file.
        string json;
        Dictionary<string, string> data;
        try {
            json = File.ReadAllText(registryPath);
            data = JsonSerializer.Deserialize<Dictionary<string, string>>(json) ?? throw new Exception();;
        } 
        catch (Exception) {
            throw new MissingJsonFileException($"Registry path: {registryPath} not found or invalid format");
        }

        // Store file names in array.
        mapFileNames = [.. data.Values];
    }

    /// <summary>
    /// Deseralizes a specified map id and then returns that map object.
    /// </summary>
    /// <param name="party">The game's party.</param>
    /// <param name="actorRegistry">The actor registry.</param>
    /// <param name="tileObjectRegistry">The tile objects registry.</param>
    /// <exception cref="MissingJsonFileException">Error thrown if a .json data file is missing.</exception>
    public Map LoadMap(Party party, ActorRegistry actorRegistry, TileObjectRegistry tileObjectRegistry) {
        // Load from passed file.
        MapData mapData;
        try {
            string fullRegistryPath = Path.Combine(
                AppContext.BaseDirectory,
                mapFileNames[party.GetCurrentMapId()]
            );
            var json = File.ReadAllText(fullRegistryPath);
            mapData = JsonSerializer.Deserialize<MapData>(json) ?? 
                throw new Exception();
        }
        catch (Exception) {
            throw new MissingJsonFileException($"Map file: {mapFileNames[party.GetCurrentMapId()]} could not be found or contains an invalid format");
        }
        
        // Load base values.
        string mapName = mapData.Name;
        int tileSheetId = mapData.TileSheetId;
        int backgroundArtId = mapData.BackgroundArtId;
        int width = mapData.Width;
        int height = mapData.Height;

        // Set tiles and actors.
        Tile[,] tiles = new Tile[width, height];
        IActorHandler[,] actors = new IActorHandler[width, height];
        List<IActorHandler> listActors = [];
        bool[,] animatedTiles = new bool[width, height];
        int size = mapData.Tiles.Count;

        // Create a list of position providers as keys and grided actors as values.
        List<Actor> storedGridedActors = [];

        for (int index = 0; index < size; index ++) {
            List<TileObject> objects = [];
            TileData tileData = mapData.Tiles.ElementAt(index);
            string tileObjects = tileData.Objects;
            // Loop over every character in the string getting the tile object ID.
            while (tileObjects.Contains(',')) {
                int currentObject = int.Parse(tileObjects[0.. tileObjects.IndexOf(',')]);
                objects.Add(tileObjectRegistry.GetTileObject(currentObject - 1));
                tileObjects = tileObjects[(tileObjects.IndexOf(',') + 1)..];
            }
            if (int.Parse(tileObjects) != 0) {
                objects.Add(tileObjectRegistry.GetTileObject(int.Parse(tileObjects) - 1));
            }

            tiles[tileData.XIndex, tileData.YIndex] = new Tile(objects);
            animatedTiles[tileData.XIndex, tileData.YIndex] = tiles[tileData.XIndex, tileData.YIndex].IsAnimated();
            // Check here if an actor exists.
            if (tileData.Actor.Contains(',')) {
                Actor actor = new(tileData.XIndex, tileData.YIndex, actorRegistry, tileData.Actor);
                actors[tileData.XIndex, tileData.YIndex] = actor;
                switch (actor.GetBehaviour()) {
                    case Behaviour.FollowsPath:
                        PathedActor pathedActor = new(actor);
                        actors[tileData.XIndex, tileData.YIndex] = pathedActor;
                        break;

                    case Behaviour.DumbTracking:
                        storedGridedActors.Add(actor);
                        break;

                    case Behaviour.SmartTracking:
                        storedGridedActors.Add(actor);
                        break;

                    default:
                        break;
                }
                listActors.Add(actors[tileData.XIndex, tileData.YIndex]);
            }
        }

        // Create the map.
        Map map = new(width, height, tileSheetId, backgroundArtId, mapName, animatedTiles, tiles, party);

        // Create grided actors.
        foreach (Actor actor in storedGridedActors) {
            GridedActor gridedActor = new(actor, party, map);
            actors[actor.GetXLocation(), actor.GetYLocation()] = gridedActor;
            listActors.Add(gridedActor);
        }

        map.AssignActors(actors, listActors);
        return map;
    }

    /// <summary>
    /// Gets a specified map file name.
    /// </summary>
    /// <param name="index">The index of the desired map file name.</param>
    /// <returns>The specific map file name.</returns>
    public string GetMapFileName(int index) {
        return mapFileNames[index];
    }

    private readonly string[] mapFileNames;
}
