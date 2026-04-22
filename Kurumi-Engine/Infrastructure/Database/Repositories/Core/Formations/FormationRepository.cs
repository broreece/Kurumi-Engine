using Infrastructure.Database.Repositories.Base;
using Infrastructure.Database.Repositories.Rows.Formations;
using Infrastructure.Database.Services;

using Microsoft.Data.Sqlite;

namespace Infrastructure.Database.Repositories.Core.Formations;

public sealed class FormationRepository 
{
    private readonly DatabaseService _databaseService;

    public FormationRepository(DatabaseService databaseService) 
    {
        _databaseService = databaseService;
    }

    public FormationRow[] LoadAll() 
    {
        using SqliteDataReader sqlReader = _databaseService.Query(
            @"SELECT id, map_name, return_x, return_y, search_timer, item_pool_id, on_found_actor_info_id, default_actor_info_id, on_lose_script, on_win_script
                FROM enemy_formations"
        );
        var rows = new List<FormationRow>();
        while (sqlReader.Read()) 
        {
            ReaderRow reader = new(sqlReader);
            // Add each row then return the array of all rows.
            rows.Add(new FormationRow() 
            {
                Id = reader.GetInt(Col.Id),
                MapName = reader.GetString(Col.MapName),
                ReturnX = reader.GetInt(Col.ReturnX),
                ReturnY = reader.GetInt(Col.ReturnY),
                SearchTimer = reader.GetInt(Col.SearchTimer),
                ItemPoolId = reader.GetInt(Col.ItemPoolId),
                OnFoundActorInfoId = reader.GetInt(Col.OnFoundActorInfoId),
                DefaultActorInfoId = reader.GetInt(Col.DefaultActorInfoId),
                OnLoseScript = reader.GetNullableString(Col.OnLoseScript),
                OnWinScript = reader.GetNullableString(Col.OnWinScript)
            });
        }
        return [.. rows];
    }

    private static class Col 
    {
        public const int Id = 0;
        public const int MapName = 1;
        public const int ReturnX = 2;
        public const int ReturnY = 3;
        public const int SearchTimer = 4;
        public const int ItemPoolId = 5;
        public const int OnFoundActorInfoId = 6;
        public const int DefaultActorInfoId = 7;
        public const int OnLoseScript = 8;
        public const int OnWinScript = 9;
    }
}