using Infrastructure.Database.Repositories.Base;
using Infrastructure.Database.Repositories.Rows.Formations;
using Infrastructure.Database.Services;

using Microsoft.Data.Sqlite;

namespace Infrastructure.Database.Repositories.Core.Formations;

public sealed class EnemyRepository 
{
    private readonly DatabaseService _databaseService;

    public EnemyRepository(DatabaseService databaseService) 
    {
        _databaseService = databaseService;
    }

    public EnemyRow[] LoadAll() 
    {
        using SqliteDataReader sqlReader = _databaseService.Query(
            @"SELECT id, enemy_formation_id, enemy_id, x_location, y_location, main_part, on_kill_script
                FROM enemy_formation_enemies"
        );
        var rows = new List<EnemyRow>();
        while (sqlReader.Read()) 
        {
            ReaderRow reader = new(sqlReader);
            // Add each row then return the array of all rows.
            rows.Add(new EnemyRow() 
            {
                Id = reader.GetInt(Col.Id),
                EnemyFormationId = reader.GetInt(Col.EnemyFormationId),
                EnemyId = reader.GetInt(Col.EnemyId),
                XLocation = reader.GetInt(Col.XLocation),
                YLocation = reader.GetInt(Col.YLocation),
                MainPart = reader.GetInt(Col.MainPart),
                OnKillScript = reader.GetNullableString(Col.OnKillScript),
            });
        }
        return [.. rows];
    }

    private static class Col 
    {
        public const int Id = 0;
        public const int EnemyFormationId = 1;
        public const int EnemyId = 2;
        public const int XLocation = 3;
        public const int YLocation = 4;
        public const int MainPart = 5;
        public const int OnKillScript = 6;
    }
}