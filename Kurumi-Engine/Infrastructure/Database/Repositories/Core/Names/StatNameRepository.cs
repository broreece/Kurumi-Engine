using Infrastructure.Database.Repositories.Base;
using Infrastructure.Database.Repositories.Rows.Entities;
using Infrastructure.Database.Services;

using Microsoft.Data.Sqlite;

namespace Infrastructure.Database.Repositories.Core.Names;

public sealed class StatNameRepository 
{
    private readonly DatabaseService _databaseService;

    public StatNameRepository(DatabaseService databaseService) 
    {
        _databaseService = databaseService;
    }

    public StatRow[] LoadAll() 
    {
        using SqliteDataReader sqlReader = _databaseService.Query(
            @"SELECT id, name, short_name
                FROM stats"
        );
        var rows = new List<StatRow>();
        while (sqlReader.Read()) 
        {
            ReaderRow reader = new(sqlReader);
            // Add each row then return the array of all rows.
            rows.Add(new StatRow() 
            {
                Id = reader.GetInt(Col.Id),
                Name = reader.GetString(Col.Name),
                ShortName = reader.GetString(Col.ShortName)
            });
        }
        return [.. rows];
    }

    private static class Col 
    {
        public const int Id = 0;
        public const int Name = 1;
        public const int ShortName = 2;
    }
}