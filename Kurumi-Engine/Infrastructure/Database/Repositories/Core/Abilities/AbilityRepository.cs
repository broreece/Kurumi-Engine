using Infrastructure.Database.Repositories.Base;
using Infrastructure.Database.Repositories.Rows.Abilities;
using Infrastructure.Database.Services;

using Microsoft.Data.Sqlite;

namespace Infrastructure.Database.Repositories.Core.Abilities;

public sealed class AbilityRepository 
{
    private readonly DatabaseService _databaseService;

    public AbilityRepository(DatabaseService databaseService) 
    {
        _databaseService = databaseService;
    }

    public AbilityRow[] LoadAll() 
    {
        using SqliteDataReader sqlReader = _databaseService.Query(
            @"SELECT id, name, description, script_name, element_id, cost, uses_mp, battle_sprite_animation_name
                FROM abilities"
        );
        var rows = new List<AbilityRow>();
        while (sqlReader.Read()) 
        {
            ReaderRow reader = new(sqlReader);
            // Add each row then return the array of all rows.
            rows.Add(new AbilityRow() 
            {
                Id = reader.GetInt(Col.Id),
                Name = reader.GetString(Col.Name),
                Description = reader.GetString(Col.Description),
                ScriptName = reader.GetNullableString(Col.ScriptName),
                ElementId = reader.GetInt(Col.ElementId),
                Cost = reader.GetInt(Col.Cost),
                UsesMp = reader.GetBool(Col.UsesMp),
                BattleSpriteAnimationName = reader.GetString(Col.BattleSpriteAnimationName)
            });
        }
        return [.. rows];
    }

    private static class Col 
    {
        public const int Id = 0;
        public const int Name = 1;
        public const int Description = 2;
        public const int ScriptName = 3;
        public const int ElementId = 4;
        public const int Cost = 5;
        public const int UsesMp = 6;
        public const int BattleSpriteAnimationName = 7;
    }
}