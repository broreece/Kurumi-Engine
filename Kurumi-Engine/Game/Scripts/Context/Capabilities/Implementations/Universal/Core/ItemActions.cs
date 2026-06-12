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

    private readonly Registry<ItemPool> _itemPoolRegistry;

    internal ItemActions(IDictionary<int, int> inventory, Registry<ItemPool> itemPoolRegistry)
    {
        _inventory = inventory;
        _itemPoolRegistry = itemPoolRegistry;
    }

    public void AddItemFromPool(int poolId) 
    {
        var itemIds = _itemPoolRegistry.Get(poolId).ItemIds;

        // Generate a random item from the pool.
        var random = new Random();
        int itemId = itemIds[random.Next(itemIds.Count)];

        // If item already exists in dictionary increment amount, else add it to the dictionary.
        if (_inventory.ContainsKey(itemId))
        {
            _inventory[itemId] = _inventory[itemId] + 1;
        }
        else
        {
            _inventory[itemId] = 1;
        }
    }
}