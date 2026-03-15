namespace Save.Interfaces;

using Game.Entities.PlayableCharacter;
using Game.Items;

/// <summary>
/// Interface used to be able to access party location, members and inventory.
/// </summary>
public interface IPartyDynamicDataAccessor : IPartyLocationAccessor {
    /// <summary>
    /// Gets the array of party members.
    /// </summary>
    /// <returns>Array of party members.</returns>
    public PlayableCharacter[] GetPartyMembers();

    /// <summary>
    /// Getter for the parties inventory.
    /// </summary>
    /// <returns>The inventory of the party.</returns>
    public List<InventoryItem> GetInventory();
}