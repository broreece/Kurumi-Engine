using Infrastructure.Database.Repositories.Base;
using Infrastructure.Database.Repositories.Rows.Generic;
using Infrastructure.Database.Services;

using Microsoft.Data.Sqlite;

namespace Infrastructure.Database.Repositories.Core.Statuses;

public sealed class StatusElementRepository {
    private readonly DatabaseService _databaseService;

    public StatusElementRepository(DatabaseService databaseService) {
        _databaseService = databaseService;
    }

    public ObjectAttributeValueRow[] LoadAll() {
        using SqliteDataReader sqlReader = _databaseService.Query(
            @"SELECT status_id, element_id, value
                FROM status_elements"
        );
        var rows = new List<ObjectAttributeValueRow>();
        while (sqlReader.Read()) 
        {
            ReaderRow reader = new(sqlReader);
            // Add each row then return the array of all rows.
            rows.Add(new ObjectAttributeValueRow() 
            {
                ObjectId = reader.GetInt(Col.ObjectId),
                AttributeId = reader.GetInt(Col.AttributeId),
                Value = reader.GetInt(Col.Value)
            });
        }
        return [.. rows];
    }

    private static class Col 
    {
        public const int ObjectId = 0;
        public const int AttributeId = 1;
        public const int Value = 2;
    }
}