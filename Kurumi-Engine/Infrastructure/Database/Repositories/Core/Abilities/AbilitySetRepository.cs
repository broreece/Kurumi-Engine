using Infrastructure.Database.Repositories.Base;
using Infrastructure.Database.Repositories.Rows.Generic;
using Infrastructure.Database.Services;

using Microsoft.Data.Sqlite;

namespace Infrastructure.Database.Repositories.Core.Abilities;

public sealed class AbilitySetRepository 
{
    private readonly DatabaseService _databaseService;

    public AbilitySetRepository(DatabaseService databaseService) 
    {
        _databaseService = databaseService;
    }

    public NameRow[] LoadAll() 
    {
        using SqliteDataReader sqlReader = _databaseService.Query(
            @"SELECT id, name
                FROM ability_sets"
        );
        var rows = new List<NameRow>();
        while (sqlReader.Read()) {
            ReaderRow reader = new(sqlReader);
            // Add each row then return the array of all rows.
            rows.Add(new NameRow() 
            {
                Id = reader.GetInt(Col.Id),
                Name = reader.GetString(Col.Name),
            });
        }
        return [.. rows];
    }

    private static class Col 
    {
        public const int Id = 0;
        public const int Name = 1;
    }
}