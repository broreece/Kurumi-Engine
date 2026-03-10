namespace UI.Interfaces;

/// <summary>
/// Inventory item accessor interface, used to access an inventory item's information.
/// </summary>
public interface IInventoryItemAccessor {
    /// <summary>
    /// Getter for the amount.
    /// </summary>
    /// <returns>The amount being stored.</returns>
    public int GetAmount();

    /// <summary>
    /// Function used to get the inventory items name.
    /// </summary>
    /// <returns>The name of the item.</returns>
    public string GetItemName();

    /// <summary>
    /// Function used to get the inventory items description.
    /// </summary>
    /// <returns>The description of the item.</returns>
    public string GetItemDescription();

    // TODO: (MIC-01) Add things like get sprite id, get weight, get usable in menu, targets all here and in the inventory item class.
}