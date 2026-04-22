using Infrastructure.Database.Repositories.Base;
using Infrastructure.Database.Repositories.Rows.Actors;
using Infrastructure.Database.Services;

using Microsoft.Data.Sqlite;

namespace Infrastructure.Database.Repositories.Core.Actors;

public sealed class ActorSpriteRepository 
{
    private readonly DatabaseService _databaseService;

    public ActorSpriteRepository(DatabaseService databaseService) 
    {
        _databaseService = databaseService;
    }

    public ActorSpriteRow[] LoadAll() 
    {
        using SqliteDataReader sqlReader = _databaseService.Query(
            @"SELECT id, sprite_name, width, height
                FROM actor_sprites"
        );
        var rows = new List<ActorSpriteRow>();
        while (sqlReader.Read()) 
        {
            ReaderRow reader = new(sqlReader);
            // Add each row then return the array of all rows.
            rows.Add(new ActorSpriteRow 
            {
                Id = reader.GetInt(Col.Id),
                SpriteName = reader.GetString(Col.SpriteName),
                Width = reader.GetInt(Col.Width),
                Height = reader.GetInt(Col.Height),
                
            });
        }
        return [.. rows];
    }

    private static class Col 
    {
        public const int Id = 0;
        public const int SpriteName = 1;
        public const int Width = 2;
        public const int Height = 3;
    }
}