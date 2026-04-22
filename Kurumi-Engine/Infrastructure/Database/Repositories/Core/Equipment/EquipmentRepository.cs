using Infrastructure.Database.Repositories.Base;
using Infrastructure.Database.Repositories.Rows.Equipment;
using Infrastructure.Database.Services;

using Microsoft.Data.Sqlite;

namespace Infrastructure.Database.Repositories.Core.Equipment;

public sealed class EquipmentRepository 
{
    private readonly DatabaseService _databaseService;

    public EquipmentRepository(DatabaseService databaseService) 
    {
        _databaseService = databaseService;
    }

    public EquipmentRow[] LoadAll() 
    {
        using SqliteDataReader sqlReader = _databaseService.Query(
            @"SELECT id, item_id, equipment_type, equipment_slot, accuracy_modifier, evasion_modifier, turn_effect_script
                FROM equipment"
        );
        var rows = new List<EquipmentRow>();
        while (sqlReader.Read()) 
        {
            ReaderRow reader = new(sqlReader);
            // Add each row then return the array of all rows.
            rows.Add(new EquipmentRow() 
            {
                Id = reader.GetInt(Col.Id),
                ItemId = reader.GetInt(Col.ItemId),
                EquipmentType = reader.GetInt(Col.EquipmentType),
                EquipmentSlot = reader.GetInt(Col.EquipmentSlot),
                Accuracy = reader.GetInt(Col.Accuracy),
                Evasion = reader.GetInt(Col.Evasion),
                TurnEffectScript = reader.GetNullableString(Col.TurnEffectScript)
            });
        }
        return [.. rows];
    }

    private static class Col 
    {
        public const int Id = 0;
        public const int ItemId = 1;
        public const int EquipmentType = 2;
        public const int EquipmentSlot = 3;
        public const int Accuracy = 4;
        public const int Evasion = 5;
        public const int TurnEffectScript = 6;
    }
}