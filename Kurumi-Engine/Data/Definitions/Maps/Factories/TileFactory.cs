using Data.Definitions.Maps.Core;

namespace Data.Definitions.Maps.Factories;

public sealed class TileFactory 
{
    public Tile Create(int id, int artId, bool animated, bool passable, bool seeThrough) 
    {
        return new Tile() { Id = id, ArtId = artId, Animated = animated, Passable = passable, SeeThrough = seeThrough };
    }
}