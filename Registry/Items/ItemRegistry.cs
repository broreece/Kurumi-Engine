namespace Registry.Items;

using Game.Items;

/// <summary>
/// The item data registry, contains data about the items.
/// </summary>
public sealed class ItemRegistry {
    /// <summary>
    /// Constructor for the item data registry.
    /// </summary>
    /// <param name="items">The items array.</param>
    public ItemRegistry(Item[] items) {
        this.items = items;
    }

    /// <summary>
    /// Getter for the items data.
    /// </summary>
    /// <returns>The array of the game's items.</returns>
    public Item[] GetItems() {
        return items;
    }

    /// <summary>
    /// Getter for a specific item from the item array.
    /// </summary>
    /// <param name="index">The index of the item in the items array.</param>
    /// <returns>The specified item.</returns>
    public Item GetItem(int index) {
        return items[index];
    }

    private readonly Item[] items;
}