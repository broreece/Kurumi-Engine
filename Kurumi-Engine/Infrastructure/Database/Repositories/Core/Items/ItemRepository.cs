// Infrastructure.
using Infrastructure.Database.Repositories.Base;
using Infrastructure.Database.Repositories.Rows.Items;
using Infrastructure.Database.Services;

// External libraries.
using Microsoft.Data.Sqlite;

namespace Infrastructure.Database.Repositories.Core.Items;

public sealed class ItemRepository 
{
    private readonly DatabaseService _databaseService;

    public ItemRepository(DatabaseService databaseService) 
    {
        _databaseService = databaseService;
    }

    public ItemRow[] LoadAll() {
        using SqliteDataReader sqlReader = _databaseService.Query(
            @"
            SELECT
                id,
                name,
                description,
                script,
                useable_in_battle,
                useable_in_menu,
                default_target_party,
                random_target,
                targets_all,
                consume_on_use,
                sprite_name,
                price,
                weight
            FROM items
            "
        );
        var rows = new List<ItemRow>();
        while (sqlReader.Read()) 
        {
            ReaderRow reader = new(sqlReader);
            // Add each row then return the array of all rows.
            rows.Add(new ItemRow() 
            {
                Id = reader.GetInt(Col.Id),
                Name = reader.GetString(Col.Name),
                Description = reader.GetString(Col.Description),
                Script = reader.GetNullableString(Col.Script),
                UseableInBattle = reader.GetBool(Col.UseableInBattle),
                UseableInMenu = reader.GetBool(Col.UseableInMenu),
                DefaultTargetParty = reader.GetBool(Col.DefaultTargetParty),
                RandomTarget = reader.GetBool(Col.RandomTarget),
                TargetsAll = reader.GetBool(Col.TargetsAll),
                ConsumeOnUse = reader.GetBool(Col.ConsumeOnUse),
                SpriteName = reader.GetString(Col.SpriteName),
                Price = reader.GetInt(Col.Price),
                Weight = reader.GetInt(Col.Weight)
            });
        }
        return [.. rows];
    }

    private static class Col 
    {
        public const int Id = 0;
        public const int Name = 1;
        public const int Description = 2;
        public const int Script = 3;
        public const int UseableInBattle = 4;
        public const int UseableInMenu = 5;
        public const int DefaultTargetParty = 6;
        public const int RandomTarget = 7;
        public const int TargetsAll = 8;
        public const int ConsumeOnUse = 9;
        public const int SpriteName = 10;
        public const int Price = 11;
        public const int Weight = 12;
    }
}