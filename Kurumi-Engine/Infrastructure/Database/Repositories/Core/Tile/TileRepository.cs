using Infrastructure.Database.Repositories.Base;
using Infrastructure.Database.Repositories.Rows.Tiles;
using Infrastructure.Database.Services;

using Microsoft.Data.Sqlite;

namespace Infrastructure.Database.Repositories.Core.Tiles;

public sealed class TileRepository 
{
    private readonly DatabaseService _databaseService;

    public TileRepository(DatabaseService databaseService) 
    {
        _databaseService = databaseService;
    }

    public TileRow[] LoadAll() 
    {
        using SqliteDataReader sqlReader = _databaseService.Query(
            @"SELECT id, art_id, animated, passable
                FROM tiles"
        );
        var rows = new List<TileRow>();
        while (sqlReader.Read()) 
        {
            ReaderRow reader = new(sqlReader);
            // Add each row then return the array of all rows.
            rows.Add(new TileRow() 
            {
                Id = reader.GetInt(Col.Id),
                ArtId = reader.GetInt(Col.ArtId),
                Animated = reader.GetBool(Col.Animated),
                Passable = reader.GetBool(Col.Passable)
            });
        }
        return [.. rows];
    }

    private static class Col 
    {
        public const int Id = 0;
        public const int ArtId = 1;
        public const int Animated = 2;
        public const int Passable = 3;
    }
}