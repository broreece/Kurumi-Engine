using Infrastructure.Database.Repositories.Base;
using Infrastructure.Database.Repositories.Rows.Statuses;
using Infrastructure.Database.Services;

using Microsoft.Data.Sqlite;

namespace Infrastructure.Database.Repositories.Core.Statuses;

public sealed class StatusRepository 
{
    private readonly DatabaseService _databaseService;

    public StatusRepository(DatabaseService databaseService) 
    {
        _databaseService = databaseService;
    }

    public StatusRow[] LoadAll() 
    {
        using SqliteDataReader sqlReader = _databaseService.Query(
            @"SELECT id, name, description, sprite_name, priority, accuracy_modifier, evasion_modifier, turn_length, cure_at_battle_end, can_act, turn_effect_script
                FROM statuses"
        );
        var rows = new List<StatusRow>();
        while (sqlReader.Read()) 
        {
            ReaderRow reader = new(sqlReader);
            // Add each row then return the array of all rows.
            rows.Add(new StatusRow() 
            {
                Id = reader.GetInt(Col.Id),
                Name = reader.GetString(Col.Name),
                Description = reader.GetString(Col.Description),
                SpriteName = reader.GetString(Col.SpriteName),
                Priority = reader.GetInt(Col.Priority),
                Accuracy = reader.GetInt(Col.Accuracy),
                Evasion = reader.GetInt(Col.Evasion),
                TurnLength = reader.GetInt(Col.TurnLength),
                CureAtBattleEnd = reader.GetBool(Col.CureAtBattleEnd),
                CanAct = reader.GetBool(Col.CanAct),
                TurnEffectScript = reader.GetNullableString(Col.TurnEffectScript)
            });
        }
        return [.. rows];
    }

    private static class Col 
    {
        public const int Id = 0;
        public const int Name = 1;
        public const int Description = 2;
        public const int SpriteName = 3;
        public const int Priority = 4;
        public const int Accuracy = 5;
        public const int Evasion = 6;
        public const int TurnLength = 7;
        public const int CureAtBattleEnd = 8;
        public const int CanAct = 9;
        public const int TurnEffectScript = 10;
    }
}