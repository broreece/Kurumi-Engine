namespace Game.Party;

using Game.Entities.PlayableCharacter;
using Game.Items;
using Game.Maps.Elements;
using Save.Interfaces;
using Scenes.Battle.Interfaces;
using Utils.Exceptions;

/// <summary>
/// The party class, contains information regarding the following:
///     - Gold.
///     - Location.
///     - Items.
///     - Party members.
/// </summary>
public sealed class Party : MapElement, IPartyAccessor, IPartyDynamicDataAccessor {
    /// <summary>
    /// Constructor for the party class.
    /// </summary>
    /// <param name="currentMapId">The current map of the party.</param>
    /// <param name="partyMembers">The array of playable characters in the party.</param>
    /// <param name="inventory">The inventory of the party.</param>
    /// <param name="xLocation">X location of the party.</param>
    /// <param name="yLocation">Y location of the party.</param>
    /// <param name="direction">The direction the party is facing.</param>
    /// <param name="visible">If the party is visible.</param>
    public Party(int currentMapId, PlayableCharacter[] partyMembers, List<InventoryItem> inventory, int xLocation, 
        int yLocation, int direction, bool visible) : base(xLocation, yLocation, direction, visible) {
        this.currentMapId = currentMapId;
        this.partyMembers = partyMembers;
        this.inventory = inventory;
    }

    /// <summary>
    /// Returns a playable character at a given character index.
    /// </summary>
    /// <param name="characterIndex">The index within the party to get the playable character from.</param>
    /// <returns>The playable character at the specified index.</returns>
    public PlayableCharacter GetPartyMember(int characterIndex) {
        return partyMembers[characterIndex];
    }

    /// <summary>
    /// Gets the lead party members field sprite id, used when switching party members on a map scene.
    /// </summary>
    /// <returns>The first party members field sprite id.</returns>
    /// <exception cref="MissingPartyDataException">Error thrown if party members do not exist.</exception>
    public int GetLeadFieldSpriteId() {
        for (int partyIndex = 0; partyIndex < partyMembers.Length; partyIndex ++) {
            if (partyMembers[partyIndex] != null) {
                return partyMembers[partyIndex].GetFieldSpriteId();
            }
        }
        throw new MissingPartyDataException("No characters in party");
    }

    /// <summary>
    /// Getter for the current maps id.
    /// </summary>
    /// <returns>The current map id, used when saving party information.</returns>
    public int GetCurrentMapId() {
        return currentMapId;
    }

    /// <summary>
    /// Sets the parties current map id.
    /// </summary>
    /// <param name="newX">The new Y coordinate of the party.</param>
    public void SetCurrentMapId(int newMapId) {
        currentMapId = newMapId;
    }

    /// <summary>
    /// Gets the array of party members.
    /// </summary>
    /// <returns>Array of party members.</returns>
    public PlayableCharacter[] GetPartyMembers() {
        return partyMembers;
    }

    /// <summary>
    /// Returns an array of the party members sprites.
    /// </summary>
    /// <returns>The array of party member sprites.</returns>
    public int[] GetPartyBattleSprites() {
        int[] sprites = new int[partyMembers.Length];
        int index = 0;
        foreach (PlayableCharacter playableCharacter in partyMembers) {
            if (playableCharacter != null) {
                sprites[index] = playableCharacter.GetSpriteId();
            } 
            else {
                // If the character isn't set don't create a sprite.
                sprites[index] = -1;
            }
            index ++;
        }
        return sprites;
    }

    /// <summary>
    /// Sets the array of party members.
    /// </summary>
    /// <param name="">Array of party members.</param>
    public void SetPartyMembers(PlayableCharacter[] newParty) {
        partyMembers = newParty;
    }

    /// <summary>
    /// Getter for the parties inventory.
    /// </summary>
    /// <returns>The inventory of the party.</returns>
    public List<InventoryItem> GetInventory() {
        return inventory;
    }

    /// <summary>
    /// Setter for the parties inventory.
    /// </summary>
    /// <param name="inventory">The new inventory of the party.</param>
    public void SetInventory(List<InventoryItem> inventory) {
        this.inventory = inventory;
    }

    private int currentMapId;
    private PlayableCharacter[] partyMembers;
    private List<InventoryItem> inventory;
}
