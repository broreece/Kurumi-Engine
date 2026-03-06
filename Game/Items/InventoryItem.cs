namespace Game.Items;

using UI.Interfaces;

/// <summary>
/// Inventory item class that contains an item and an amount.
/// </summary>
public sealed class InventoryItem : IInventoryItemAccessor {
    /// <summary>
    /// Inventory item class constructor.
    /// </summary>
    /// <param name="item">The item being stored.</param>
    /// <param name="amount">The amount of the item.</param>
    public InventoryItem(Item item, int amount) {
        this.item = item;
        this.amount = amount;
    }

    /// <summary>
    /// Getter for the item.
    /// </summary>
    /// <returns>The item being stored.</returns>
    public Item GetItem() {
        return item;
    }

    /// <summary>
    /// Getter for the amount.
    /// </summary>
    /// <returns>The amount being stored.</returns>
    public int GetAmount() {
        return amount;
    }

    /// <summary>
    /// Function used to get the inventory items name.
    /// </summary>
    /// <returns>The name of the item.</returns>
    public string GetItemName() {
        return item.GetName();
    }

    /// <summary>
    /// Function used to get the inventory items description.
    /// </summary>
    /// <returns>The description of the item.</returns>
    public string GetItemDescription() {
        return item.GetDescription();
    }

    /// <summary>
    /// Setter for the amount.
    /// </summary>
    /// <param name="amount">The new amount of the item.</param>
    public void SetAmount(int amount) {
        this.amount = amount;
    }

    public readonly Item item;
    public int amount;
}