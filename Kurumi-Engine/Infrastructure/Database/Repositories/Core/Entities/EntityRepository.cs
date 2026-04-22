using Infrastructure.Database.Repositories.Base;
using Infrastructure.Database.Repositories.Rows.Entities;
using Infrastructure.Database.Services;

using Microsoft.Data.Sqlite;

namespace Infrastructure.Database.Repositories.Core.Entities;

public sealed class EntityRepository 
{
    private readonly DatabaseService _databaseService;

    public EntityRepository(DatabaseService databaseService) 
    {
        _databaseService = databaseService;
    }

    public EntityRow[] LoadAll() 
    {
        using SqliteDataReader sqlReader = _databaseService.Query(
            @"SELECT id, name, description, max_hp, battle_sprite_name
                FROM entities"
        );
        var rows = new List<EntityRow>();
        while (sqlReader.Read()) 
        {
            ReaderRow reader = new(sqlReader);
            // Add each row then return the array of all rows.
            rows.Add(new EntityRow() 
            {
                Id = reader.GetInt(Col.Id),
                Name = reader.GetString(Col.Name),
                Description = reader.GetString(Col.Description),
                MaxHP = reader.GetInt(Col.MaxHP),
                BattleSpriteName = reader.GetString(Col.BattleSpriteName)
            });
        }
        return [.. rows];
    }

    private static class Col 
    {
        public const int Id = 0;
        public const int Name = 1;
        public const int Description = 2;
        public const int MaxHP = 3;
        public const int BattleSpriteName = 4;
    }
}