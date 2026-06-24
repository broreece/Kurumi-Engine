// Data.
using Data.Definitions.Items.Core;

using Data.Models.Inventory;

// Game.
using Game.Scripts.Context.Capabilities.Interfaces.Universal;

// Infrastructure.
using Infrastructure.Database.Base;

namespace Game.Scripts.Context.Capabilities.Implementations.Universal.Core;

public sealed class ItemActions : IItemActions 
{
    private readonly Inventory _inventory;

    private readonly Registry<Item> _itemRegistry;
    private readonly Registry<ItemPool> _itemPoolRegistry;

    internal ItemActions(
        Inventory inventory, 
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

        _inventory.IncrementAmount(itemId);

        return _itemRegistry.Get(itemId).Name;
    }

    public bool ContainsMoreThenOfItem(int itemId, int compareAmount) => _inventory.GetAmountOf(itemId) > compareAmount;

    public bool ContainsLessThenOfItem(int itemId, int compareAmount) => _inventory.GetAmountOf(itemId) < compareAmount;

    public bool ContainsSameAmountOfItem(int itemId, int compareAmount) => _inventory.GetAmountOf(itemId) == compareAmount;

    public void RemoveItemFromInventory(int itemId, int amount) => _inventory.RemoveAmountOfItem(itemId, amount);
}