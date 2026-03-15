namespace Game.Items;

using Game.Core;

/// <summary>
/// The item class a type of nameable element.
/// Contains, price, usablity situations, and effect.
/// </summary>
public class Item : PresentationData {
    /// <summary>
    /// Constructor for items.
    /// </summary>
    /// <param name="id">The id of the item.</param>
    /// <param name="name">The name of the item.</param>
    /// <param name="description">The description of the item.</param>
    /// <param name="effect">The effect of the item.</param>
    /// <param name="usableInBattle">If the item can be used in battle.</param>
    /// <param name="usableInMenu">If the item can be used in menu.</param>
    /// <param name="targetsParty">If the item can target the party.</param>
    /// <param name="targetsEnemies">If the item can target enemies.</param>
    /// <param name="targetsAll">If the item targets all.</param>
    /// <param name="consumeOnUse">If the item is consumed on use.</param>
    /// <param name="spriteId">The sprite id of the item.</param>
    /// <param name="price">The item's price.</param> 
    /// <param name="weight">The item's weight.</param> 
    public Item(int id, string name, string description, string effect, bool usableInBattle, bool usableInMenu,
        bool targetsParty, bool targetsEnemies, bool targetsAll, bool consumeOnUse, int spriteId, int price,
        int weight): base(name, description, spriteId) {
        this.id = id;
        this.effect = effect;
        this.usableInBattle = usableInBattle;
        this.usableInMenu = usableInMenu;
        this.targetsParty = targetsParty;
        this.targetsEnemies = targetsEnemies;
        this.targetsAll = targetsAll;
        this.consumeOnUse = consumeOnUse;
        this.price = price;
        this.weight = weight;
    }

    /// <summary>
    /// Getter for the item's effect.
    /// </summary>
    /// <returns>The item's effect.</returns>
    public string GetEffect() {
        return effect;
    }

    /// <summary>
    /// Getter for the item's usability in battle.
    /// </summary>
    /// <returns>If the item can be used in battle.</returns>
    public bool IsUsableInBattle() {
        return usableInBattle;
    }

    /// <summary>
    /// Getter for the item's usability in menu.
    /// </summary>
    /// <returns>If the item can be used in menu.</returns>
    public bool IsUsableInMenu() {
        return usableInMenu;
    }

    /// <summary>
    /// Getter for if the item can target the party.
    /// </summary>
    /// <returns>If the item can target the party.</returns>
    public bool CanTargetParty() {
        return targetsParty;
    }

    /// <summary>
    /// Getter for if the item can target the enemy.
    /// </summary>
    /// <returns>If the item can target the enemy.</returns>
    public bool CanTargetEnemy() {
        return targetsEnemies;
    }

    /// <summary>
    /// Getter for if the item can target all.
    /// </summary>
    /// <returns>If the item can target all.</returns>
    public bool CanTargetAll() {
        return targetsAll;
    }

    /// <summary>
    /// Getter for if the item is consumed on use.
    /// </summary>
    /// <returns>If the item is cosumed on use.</returns>
    public bool IsConsumedOnUse() {
        return consumeOnUse;
    }

    /// <summary>
    /// Getter for the item's id.
    /// </summary>
    /// <returns>The item's id.</returns>
    public int GetId() {
        return id;
    }

    /// <summary>
    /// Getter for the item's price.
    /// </summary>
    /// <returns>The item's price.</returns>
    public int GetPrice() {
        return price;
    }
    
    /// <summary>
    /// Getter for the item's weight.
    /// </summary>
    /// <returns>The item's weight.</returns>
    public int GetWeight() {
        return weight;
    }

    private readonly string effect;
    private readonly bool usableInBattle, usableInMenu, targetsParty, targetsEnemies, targetsAll, consumeOnUse;
    private readonly int id, price, weight;
}