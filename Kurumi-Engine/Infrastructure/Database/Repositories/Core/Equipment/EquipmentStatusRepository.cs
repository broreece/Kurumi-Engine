using Infrastructure.Database.Repositories.Base;
using Infrastructure.Database.Repositories.Rows.Generic;
using Infrastructure.Database.Services;

using Microsoft.Data.Sqlite;

namespace Infrastructure.Database.Repositories.Core.Equipment;

public sealed class EquipmentStatusRepository 
{
    private readonly DatabaseService _databaseService;

    public EquipmentStatusRepository(DatabaseService databaseService) 
    {
        _databaseService = databaseService;
    }

    public ObjectAttributeValueRow[] LoadAll() 
    {
        using SqliteDataReader sqlReader = _databaseService.Query(
            @"SELECT equipment_id, status_id, value
                FROM equipment_statuses"
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