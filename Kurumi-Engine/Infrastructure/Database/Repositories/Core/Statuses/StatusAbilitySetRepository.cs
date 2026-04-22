using Infrastructure.Database.Repositories.Base;
using Infrastructure.Database.Repositories.Rows.Generic;
using Infrastructure.Database.Services;

using Microsoft.Data.Sqlite;

namespace Infrastructure.Database.Repositories.Core.Statuses;

public sealed class StatusAbilitySetRepository 
{
    private readonly DatabaseService _databaseService;

    public StatusAbilitySetRepository(DatabaseService databaseService) 
    {
        _databaseService = databaseService;
    }

    public AbilitySealRow[] LoadAll() 
    {
        using SqliteDataReader sqlReader = _databaseService.Query(
            @"SELECT status_id, ability_set_id, sealed
                FROM status_ability_sets"
        );
        var rows = new List<AbilitySealRow>();
        while (sqlReader.Read()) {
            ReaderRow reader = new(sqlReader);
            // Add each row then return the array of all rows.
            rows.Add(new AbilitySealRow() 
            {
                SourceId = reader.GetInt(Col.SourceId),
                AbilityRefId = reader.GetInt(Col.AbilityRefId),
                Sealed = reader.GetBool(Col.Sealed)
            });
        }
        return [.. rows];
    }

    private static class Col 
    {
        public const int SourceId = 0;
        public const int AbilityRefId = 1;
        public const int Sealed = 2;
    }
}