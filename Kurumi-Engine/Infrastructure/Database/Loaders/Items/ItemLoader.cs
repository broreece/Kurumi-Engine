using ItemDefinition = Data.Definitions.Items.Core.Item;
using Infrastructure.Database.Interfaces;
using Infrastructure.Database.Repositories.Core.Items;
using Infrastructure.Database.Repositories.Rows.Items;
using Data.Definitions.Items.Factories;

namespace Infrastructure.Database.Loaders.Items;

public sealed class ItemLoader : IDataLoader<ItemDefinition> 
{
    private readonly ItemRepository _itemRepository;
    private readonly ItemFactory _itemFactory;

    public ItemLoader(ItemRepository itemRepository, ItemFactory itemFactory) 
    {
        _itemRepository = itemRepository;
        _itemFactory = itemFactory;
    }

    public IReadOnlyList<ItemDefinition> LoadAll() 
    {
        ItemRow[] rows = _itemRepository.LoadAll();
        var items = new ItemDefinition[rows.Length];
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
                row.UsableInBattle,
                row.UsableInMenu,
                row.TargetsParty,
                row.TargetsEnemy,
                row.TargetsAll,
                row.ConsumeOnUse
            );
        }
        return items;
    }
}