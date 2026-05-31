// Data.
using Data.Definitions.Items.Core;
using Data.Definitions.Items.Factories;

// Infrastructure.
using Infrastructure.Database.Interfaces;
using Infrastructure.Database.Repositories.Core.Items;
using Infrastructure.Database.Repositories.Rows.Items;

namespace Infrastructure.Database.Loaders.Items;

public sealed class ItemLoader : IDataLoader<Item> 
{
    private readonly ItemRepository _itemRepository;
    private readonly ItemFactory _itemFactory;

    public ItemLoader(ItemRepository itemRepository, ItemFactory itemFactory) 
    {
        _itemRepository = itemRepository;
        _itemFactory = itemFactory;
    }

    public IReadOnlyList<Item> LoadAll() 
    {
        ItemRow[] rows = _itemRepository.LoadAll();
        var items = new Item[rows.Length];
        for (var index = 0; index < rows.Length; index ++) 
        {
            var row = rows[index];
            items[index] = _itemFactory.Create(
                row.Id, 
                row.Price, 
                row.Weight, 
                row.SpriteName, 
                row.Script, 
                row.Name, 
                row.Description, 
                row.UseableInBattle, 
                row.UseableInMenu, 
                row.DefaultTargetParty, 
                row.RandomTarget, 
                row.TargetsAll, 
                row.ConsumeOnUse
            );
        }
        return items;
    }
}