// Data.
using Data.Definitions.Items.Core;

// Infrastructure.
using Infrastructure.Database.Base;

namespace Game.Scripts.Context.Capabilities.Implementations.Universal.Core;

public sealed class ItemActionsFactory 
{
    private readonly IDictionary<int, int> _inventory;

    private readonly Registry<Item> _itemRegistry;
    private readonly Registry<ItemPool> _itemPoolRegistry;

    public ItemActionsFactory(
        IDictionary<int, int> inventory, 
        Registry<Item> itemRegistry, 
        Registry<ItemPool> itemPoolRegistry
    )
    {
        _inventory = inventory;
        _itemRegistry = itemRegistry;
        _itemPoolRegistry = itemPoolRegistry;
    }

    public ItemActions Create()
    {
        return new ItemActions(_inventory, _itemRegistry, _itemPoolRegistry);
    }
}