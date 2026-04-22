using Infrastructure.Database.Repositories.Base;
using Infrastructure.Database.Repositories.Rows.Items;
using Infrastructure.Database.Services;

using Microsoft.Data.Sqlite;

namespace Infrastructure.Database.Repositories.Core.Items;

public sealed class ItemPoolItemRepository 
{
    private readonly DatabaseService _databaseService;

    public ItemPoolItemRepository(DatabaseService databaseService) 
    {
        _databaseService = databaseService;
    }

    public ItemPoolItemRow[] LoadAll() 
    {
        using SqliteDataReader sqlReader = _databaseService.Query(
            @"SELECT item_pool_id, item_id
                FROM item_pool_items"
        );
        var rows = new List<ItemPoolItemRow>();
        while (sqlReader.Read()) 
        {
            ReaderRow reader = new(sqlReader);
            // Add each row then return the array of all rows.
            rows.Add(new ItemPoolItemRow() 
            {
                ItemPoolId = reader.GetInt(Col.ItemPoolId),
                ItemId = reader.GetInt(Col.ItemId),
            });
        }
        return [.. rows];
    }

    private static class Col 
    {
        public const int ItemPoolId = 0;
        public const int ItemId = 1;
    }
}