namespace Game.Items;

using Game.Entities.Base;
using Game.Entities.Skills;

/// <summary>
/// The equipment class. Contains an item reference. Inherits from entity attributes.
/// </summary>
public sealed class Equipment : EntityElement {
    /// <summary>
    /// Constructor for equipments.
    /// </summary>
    /// <param name="turnEffectSpriteId">The turn effect sprite of the equipment's turn effect.</param>
    /// <param name="accuracyModifier">The accuracy modifier for the entity element.</param>
    /// <param name="evasionModifier">The evasion modifier for the entity element.</param>
    /// <param name="equipmentSlotId">The slot id of the equipment.</param>
    /// <param name="equipmentTypeId">The type id of the equipment.</param>
    /// <param name="attack">The attack of the equipment.</param>
    /// <param name="magicAttack">The magic attack of the equipment.</param>
    /// <param name="defence">The attack of the equipment.</param>
    /// <param name="magicDefence">The magic attack of the equipment.</param>
    /// <param name="stats">The stats for the entity element.</param>
    /// <param name="elements">The elements for the entity element.</param>
    /// <param name="turnScript">The turn script for the entity element.</param>
    /// <param name="sealedSkills">The list of sealed skills of the entity element.</param>
    /// <param name="sealedAbilities">The list of sealed abilities of the entity element.</param>
    /// <param name="addedAbilities">The list of added abilities of the entity element.</param>
    /// <param name="item">The item reference of the equipment.</param>
    public Equipment(int id, int turnEffectSpriteId, int accuracyModifier, int evasionModifier, int equipmentSlotId,
        int equipmentTypeId, int attack, int magicAttack, int defence, int magicDefence, int[] stats, int[] elements,
        string turnScript, List<Skill> sealedSkills, List<Ability> sealedAbilities, List<Ability> addedAbilities,
        Item item) : base(turnEffectSpriteId, accuracyModifier, evasionModifier, stats, elements,
        turnScript, sealedSkills, sealedAbilities, addedAbilities) {
        this.id = id;
        this.equipmentSlotId = equipmentSlotId;
        this.equipmentTypeId = equipmentTypeId;
        this.attack = attack;
        this.magicAttack = magicAttack;
        this.defence = defence;
        this.magicDefence = magicDefence;
        this.item = item;
    }
    
    /// <summary>
    /// Getter for the id of the equipment uses.
    /// </summary>
    /// <returns>The equipments id.</returns>
    public int GetId() {
        return id;
    }

    /// <summary>
    /// Getter for the slot id that the equipment uses.
    /// </summary>
    /// <returns>The equipments slot id.</returns>
    public int GetEquipmentSlot() {
        return equipmentSlotId;
    }

    /// <summary>
    /// Getter for the type id that the equipment uses.
    /// </summary>
    /// <returns>The equipments type id.</returns>
    public int GetEquipmentType() {
        return equipmentTypeId;
    }

    /// <summary>
    /// Getter for the equipment's attack change.
    /// </summary>
    /// <returns>The equipments attack change.</returns>
    public int GetEquipmentAttack() {
        return attack;
    }

    /// <summary>
    /// Getter for the equipment's magic attack change.
    /// </summary>
    /// <returns>The equipments magic attack change.</returns>
    public int GetEquipmentMagicAttack() {
        return magicAttack;
    }

    /// <summary>
    /// Getter for the equipment's defense change.
    /// </summary>
    /// <returns>The equipments defense change.</returns>
    public int GetEquipmentDefence() {
        return defence;
    }

    /// <summary>
    /// Getter for the equipment's magic defence change.
    /// </summary>
    /// <returns>The equipments magic defence change.</returns>
    public int GetEquipmentMagicDefence() {
        return magicDefence;
    }

    /// <summary>
    /// Getter for the equipments item reference.
    /// </summary>
    /// <returns>The item reference.</returns>
    public Item GetItem() {
        return item;
    }
    
    private readonly int id, equipmentSlotId, equipmentTypeId, attack, magicAttack, defence, magicDefence;
    private readonly Item item;
}