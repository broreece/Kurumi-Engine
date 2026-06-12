// Data.
using Data.Definitions.Items.Core;
using Data.Definitions.Items.Factories;

// Infrastructure.
using Infrastructure.Database.Interfaces;
using Infrastructure.Database.Repositories.Core.Items;
using Infrastructure.Database.Repositories.Rows.Generic;
using Infrastructure.Database.Repositories.Rows.Items;

namespace Infrastructure.Database.Loaders.Items;

public sealed class ItemPoolLoader : IDataLoader<ItemPool> 
{
    private readonly ItemPoolRepository _itemPoolRepository;
    private readonly ItemPoolItemRepository _itemPoolItemRepository;

    private readonly ItemPoolFactory _itemPoolFactory;

    public ItemPoolLoader(
        ItemPoolRepository itemPoolRepository, 
        ItemPoolItemRepository itemPoolItemRepository, 
        ItemPoolFactory itemPoolFactory
    ) 
    {
        _itemPoolRepository = itemPoolRepository;
        _itemPoolItemRepository = itemPoolItemRepository;
        _itemPoolFactory = itemPoolFactory;
    }

    public IReadOnlyList<ItemPool> LoadAll() 
    {
        NameRow[] rows = _itemPoolRepository.LoadAll();
        ItemPoolItemRow[] itemPoolItemRows = _itemPoolItemRepository.LoadAll();

        Dictionary<int, IList<int>> itemPoolItems = [];
        foreach (ItemPoolItemRow itemPoolItemRow in itemPoolItemRows)
        {
            int itemPoolId = itemPoolItemRow.ItemPoolId;
            int itemId = itemPoolItemRow.ItemId;
            
            if (itemPoolItems.TryGetValue(itemPoolId, out IList<int>? itemIdsList))
            {
                itemIdsList.Add(itemId);
            }
            else
            {
                itemPoolItems[itemPoolId] = [itemId];
            }
        }

        var itemPools = new ItemPool[rows.Length];
        for (var index = 0; index < rows.Length; index ++) 
        {
            var row = rows[index];

            int id = row.Id;

            itemPools[index] = _itemPoolFactory.Create(
                id, 
                (IReadOnlyList<int>) itemPoolItems[id]
            );
        }
        return itemPools;
    }
}