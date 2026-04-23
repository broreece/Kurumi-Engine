using Data.Definitions.Maps.Core;
using Data.Runtime.Actors.Core;
using Data.Runtime.Maps.Core;
using Data.Runtime.Spatials;
using Infrastructure.Database.Base;

namespace Engine.Systems.Navigation.Core;

/// <summary>
/// Used to determine if a movement is possible by different methods such as if the tile is passable, if the 
/// coordinates are in range or all actors occupying the tile are passable.
/// </summary>
public sealed class NavigationGrid 
{
    private readonly Map _map;
    private readonly Registry<Tile> _tileRegistry;
    private readonly IPositionProvider _partyPosition;

    internal NavigationGrid(Map map, Registry<Tile> tileRegistry, IPositionProvider partyPosition) 
    {
        _map = map;
        _tileRegistry = tileRegistry;
        _partyPosition = partyPosition;
    }

    public bool IsNavigable(int xLocation, int yLocation) 
    {
        var tileObjects = _map.GetTileObjectsAt(xLocation, yLocation);
        var actors = _map.GetActorsAt(xLocation, yLocation);

        return InMapRange(xLocation, yLocation) && IsTilePassable(tileObjects) && AreActorsPassable(actors) && 
            NotPartyLocation(xLocation, yLocation);
    }

    private bool InMapRange(int xLocation, int yLocation) 
    {
        return xLocation >= 0 && xLocation < _map.Width && yLocation >= 0 && yLocation < _map.Height;
    }

    private bool IsTilePassable(IReadOnlyList<int> tileObjects) 
    {
        foreach (int tileObject in tileObjects) 
        {
            if (!_tileRegistry.Get(tileObject).Passable) 
            {
                return false;
            }
        }
        return true;
    }

    private bool AreActorsPassable(IReadOnlyList<Actor> actors) 
    {
        foreach (var actor in actors) 
        {
            if (!actor.ActorInfo.Passable) 
            {
                return false;
            }
        }
        return true;
    }

    private bool NotPartyLocation(int xLocation, int yLocation) {
        return xLocation != _partyPosition.XLocation || yLocation != _partyPosition.YLocation;
    }
}