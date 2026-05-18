using Data.Definitions.Maps.Core;
using Data.Definitions.Maps.Factories;
using Infrastructure.Database.Interfaces;
using Infrastructure.Database.Repositories.Core.Tiles;
using Infrastructure.Database.Repositories.Rows.Tiles;

namespace Infrastructure.Database.Loaders.Tiles;

public sealed class TileLoader : IDataLoader<Tile> 
{
    private readonly TileRepository _tileRepository;
    private readonly TileFactory _tileFactory;

    public TileLoader(TileRepository tileRepository, TileFactory tileFactory) 
    {
        _tileRepository = tileRepository;
        _tileFactory = tileFactory;
    }

    public IReadOnlyList<Tile> LoadAll() 
    {
        TileRow[] rows = _tileRepository.LoadAll();
        var tiles = new Tile[rows.Length];
        for (var index = 0; index < rows.Length; index ++) 
        {
            var row = rows[index];
            tiles[index] = _tileFactory.Create(
                row.Id,
                row.ArtId,
                row.Animated,
                row.Passable,
                row.SeeThrough
            );
        }
        return tiles;
    }
}