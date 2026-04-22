using Infrastructure.Database.Repositories.Base;
using Infrastructure.Database.Repositories.Rows.Characters;
using Infrastructure.Database.Services;

using Microsoft.Data.Sqlite;

namespace Infrastructure.Database.Repositories.Core.Characters;

public sealed class CharacterRepository 
{
    public CharacterRepository(DatabaseService databaseService) 
    {
        _databaseService = databaseService;
    }

    public CharacterRow[] LoadAll() 
    {
        using SqliteDataReader sqlReader = _databaseService.Query(
            @"SELECT id, name, description, battle_sprite, field_sprite, menu_sprite
                FROM characters"
        );
        var rows = new List<CharacterRow>();
        while (sqlReader.Read()) 
        {
            ReaderRow reader = new(sqlReader);
            // Add each row then return the array of all rows.
            rows.Add(new CharacterRow() 
            {
                Id = reader.GetInt(Col.Id),
                Name = reader.GetString(Col.Name),
                Description = reader.GetString(Col.Description),
                BattleSprite = reader.GetString(Col.BattleSprite),
                FieldSprite = reader.GetString(Col.FieldSprite),
                MenuSprite = reader.GetString(Col.MenuSprite),
            });
        }
        return [.. rows];
    }

    private static class Col 
    {
        public const int Id = 0;
        public const int Name = 1;
        public const int Description = 2;
        public const int BattleSprite = 3;
        public const int FieldSprite = 4;
        public const int MenuSprite = 5;
    }

    private readonly DatabaseService _databaseService;
}