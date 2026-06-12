// Data.
using Data.Definitions.Items.Core;

// Game.
using Game.Scripts.Context.Capabilities.Interfaces.Universal;

// Infrastructure.
using Infrastructure.Database.Base;

namespace Game.Scripts.Context.Capabilities.Implementations.Universal.Core;

public sealed class ItemActions : IItemActions 
{
    private readonly IDictionary<int, int> _inventory;

    private readonly Registry<Item> _itemRegistry;
    private readonly Registry<ItemPool> _itemPoolRegistry;

    internal ItemActions(
        IDictionary<int, int> inventory, 
        Registry<Item> itemRegistry, 
        Registry<ItemPool> itemPoolRegistry
    )
    {
        _inventory = inventory;
        _itemRegistry = itemRegistry;
        _itemPoolRegistry = itemPoolRegistry;
    }

    public string AddItemFromPool(int poolId) 
    {
        var itemIds = _itemPoolRegistry.Get(poolId).ItemIds;

        // Generate a random item from the pool.
        var random = new Random();
        int itemId = itemIds[random.Next(itemIds.Count)];

        // If item already exists in dictionary increment amount, else add it to the dictionary.
        if (_inventory.TryGetValue(itemId, out int value))
        {
            _inventory[itemId] = value + 1;
        }
        else
        {
            _inventory[itemId] = 1;
        }

        return _itemRegistry.Get(itemId).Name;
    }

    public bool ContainsMoreThenOfItem(int itemId, int compareAmount)
    {
        if (_inventory.TryGetValue(itemId, out int amount))
        {
            return amount > compareAmount;
        } 
        return compareAmount < 0;
    }

    public bool ContainsLessThenOfItem(int itemId, int compareAmount)
    {
        if (_inventory.TryGetValue(itemId, out int amount))
        {
            return amount < compareAmount;
        } 
        return compareAmount > 0;
    }

    public bool ContainsSameAmountOfItem(int itemId, int compareAmount)
    {
        if (_inventory.TryGetValue(itemId, out int amount))
        {
            return amount == compareAmount;
        } 
        return compareAmount == 0;
    }

    public bool ContainsDifferentAmountOfItem(int itemId, int compareAmount)
    {
        if (_inventory.TryGetValue(itemId, out int amount))
        {
            return amount != compareAmount;
        } 
        return compareAmount != 0;
    }
}