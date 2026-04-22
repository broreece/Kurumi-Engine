using Infrastructure.Database.Repositories.Base;
using Infrastructure.Database.Repositories.Rows.Formations;
using Infrastructure.Database.Services;

using Microsoft.Data.Sqlite;

namespace Infrastructure.Database.Repositories.Core.Formations;

public sealed class EnemyBattleScriptRepository 
{
    private readonly DatabaseService _databaseService;

    public EnemyBattleScriptRepository(DatabaseService databaseService) 
    {
        _databaseService = databaseService;
    }

    public EnemyBattleScriptRow[] LoadAll() 
    {
        using SqliteDataReader sqlReader = _databaseService.Query(
            @"SELECT id, enemy_formation_enemy_id, script_name, target, start_turn, frequency
                FROM enemy_formation_enemy_battle_scripts"
        );
        var rows = new List<EnemyBattleScriptRow>();
        while (sqlReader.Read()) 
        {
            ReaderRow reader = new(sqlReader);
            // Add each row then return the array of all rows.
            rows.Add(new EnemyBattleScriptRow() 
            {
                Id = reader.GetInt(Col.Id),
                EnemyFormationEnemyId = reader.GetInt(Col.EnemyFormationEnemyId),
                ScriptName = reader.GetString(Col.ScriptName),
                Target = reader.GetInt(Col.Target),
                StartTurn = reader.GetInt(Col.StartTurn),
                Frequency = reader.GetInt(Col.Frequency)
            });
        }
        return [.. rows];
    }

    private static class Col 
    {
        public const int Id = 0;
        public const int EnemyFormationEnemyId = 1;
        public const int ScriptName = 2;
        public const int Target = 3;
        public const int StartTurn = 4;
        public const int Frequency = 5;
    }
}