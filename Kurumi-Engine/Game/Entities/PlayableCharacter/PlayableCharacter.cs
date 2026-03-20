namespace Game.Entities.PlayableCharacter;

using Game.Entities.Base;
using Game.Entities.Skills;
using Game.Items;
using Scenes.Battle.Interfaces;
using UI.Interfaces;

/// <summary>
/// The playable character class. Contains information relating to the playable characters. Inherits from entity.
/// </summary>
public sealed class PlayableCharacter : Entity, ICharacterFileAccessor, ICharacterSkillsNameAccessor {
    /// <summary>
    /// Playable character constructor.
    /// </summary>
    /// <param name="id">The id of the playable character.</param>
    /// <param name="name">The name of the playable character.</param>
    /// <param name="description">The description of the playable character.</param>
    /// <param name="maxHp">The max hp value of the playable character.</param>
    /// <param name="maxMp">The max mp value of the playable character.</param>
    /// <param name="spriteId">The battle sprite id of the playable character.</param>
    /// <param name="fieldSpriteId">The field sprite id of the playable character.</param>
    /// <param name="menuSpriteId">The menu sprite id of the playable character.</param>
    /// <param name="stats">The stats of the playable character.</param>
    /// <param name="elementalResistances">The playable character elemental resistances.</param>
    /// <param name="statusResistances">The entities status resistances.</param>
    /// <param name="growthRates">The growth rates of the playable character.</param>
    /// <param name="equipment">The equipment on the playable character.</param>
    /// <param name="baseAbilities">The base abilities of the playable character.</param>
    /// <param name="equipmentTypes">The equipment types the playable character can equip.</param>
    /// <param name="skills">The skills of the playable character.</param>
    /// <param name="statuses">The statuses of the playable character.</param> 
    public PlayableCharacter(int id, string name, string description, int currentHp, int maxHp,
        int currentMp, int maxMp, int spriteId, int fieldSpriteId, int menuSpriteId, 
        int[] stats, int[] elementalResistances, int[] statusResistances,
        Equipment?[] equipment, List<Ability> baseAbilities, List<int> equipmentTypes, List<Skill> skills) 
        : base(name, description, maxHp, spriteId, stats, elementalResistances, statusResistances, baseAbilities) {
        this.id = id;
        this.currentHp = currentHp;
        this.currentMp = currentMp;
        this.maxMp = maxMp;
        this.fieldSpriteId = fieldSpriteId;
        this.menuSpriteId = menuSpriteId;
        this.equipment = equipment;
        this.equipmentTypes = equipmentTypes;
        this.skills = skills;
    }

    /// <summary>
    /// Getter for a specific entity stat.
    /// </summary>
    /// <param name="index">The index of the stat in the stats array.</param>
    /// <returns>The specified stat in the stats array.</returns>
    public override int GetStat(int index) {
        int sumStat = base.GetStat(index);
        foreach (Equipment? individualEquipment in equipment) {
            if (individualEquipment != null) {
                sumStat += individualEquipment.GetStat(index);
            }
        }
        return sumStat;
    }

    /// <summary>
    /// Function that returns the base abilities + added abilities - sealed abilities.
    /// </summary>
    /// <returns>The base list of abilities the character has.</returns>
    public override List<Ability> GetBaseAbilities() {
        // Sort the list after changes.
        return GetFilteredAbilities();
    }

    /// <summary>
    /// Getter for the entities base ability.
    /// </summary>
    /// <param name="index">The index of the base ability.</param>
    /// <returns>The specified base ability.</returns>
    public override Ability GetBaseAbility(int index) {
        return GetFilteredAbilities()[index];
    }

    /// <summary>
    /// Getter for the max mp of the character.
    /// </summary>
    /// <returns>The max mp of the character.</returns>
    public int GetMaxMp() {
        return maxMp;
    }

    /// <summary>
    /// Getter for the current mp of the character.
    /// </summary>
    /// <returns>The current mp of the character.</returns>
    public int GetCurrentMp() {
        return currentMp;
    }

    /// <summary>
    /// Setter for the base abilities of the character.
    /// </summary>
    /// <param name="newAbilities">Sets the base abilities of the character.</param>
    public void SetBaseAbilities(List<Ability> newAbilities) {
        baseAbilities = newAbilities;
    }
    
    /// <summary>
    /// Getter for the character's id.
    /// </summary>
    /// <returns>The character's id.</returns>
    public int GetId() {
        return id;
    }

    /// <summary>
    /// Setter for the max mp of the character.
    /// </summary>
    /// <param name="newMaxMp">Sets the max mp of the character.</param>
    public void SetMaxMp(int newMaxMp) {
        maxMp = newMaxMp;
    }

    /// <summary>
    /// Setter for the current mp of the character.
    /// </summary>
    /// <param name="newHp">Sets the current mp of the character.</param>
    public void SetCurrentMP(int newMp) {
        currentMp = newMp;
    }

    /// <summary>
    /// Getter for the field sprite id.
    /// </summary>
    /// <returns>The field sprite id.</returns>
    public int GetFieldSpriteId() {
        return fieldSpriteId;
    }

    /// <summary>
    /// Getter for the menu sprite id.
    /// </summary>
    /// <returns>The menu sprite id.</returns>
    public int GetMenuSpriteId() {
        return menuSpriteId;
    }

    /// <summary>
    /// Getter for the characters equipment.
    /// </summary>
    /// <returns>The characters equipment.</returns>
    public Equipment?[] GetEquipment() {
        return equipment;
    }

    /// <summary>
    /// Setter for the playable character's equipment.
    /// </summary>
    /// <param name="newEquipment">The new array of equipment the playable character has.</param>
    public void SetEquipment(Equipment?[] newEquipment) {
        equipment = newEquipment;
    }

    /// <summary>
    /// Getter for the playable character's possible equipment types.
    /// </summary>
    /// <returns>The list of equipment types the playable character can use.</returns>
    public List<int> GetEquipmentTypes() {
        return equipmentTypes;
    }

    /// <summary>
    /// Setter for the playable character's possible equipment types.
    /// </summary>
    /// <param name="newEquipmentTypes">The new list of equipment types that the playable character can use.</param>
    public void SetEquipmentTypes(List<int> newEquipmentTypes) {
        equipmentTypes = newEquipmentTypes;
    }

    /// <summary>
    /// Getter for the playable character's skills.
    /// </summary>
    /// <returns>The list of skills the playable character has.</returns>
    public List<Skill> GetSkills() {
        return skills;
    }

    /// <summary>
    /// Setter for the playable character's skills.
    /// </summary>
    /// <param name="newSkills">The new list of skills the playable character has.</param>
    public void SetSkills(List<Skill> newSkills) {
        skills = newSkills;
    }

    /// <summary>
    /// Function used to return the list of the names of all the characters skills.
    /// </summary>
    /// <returns>A list of the characters skills names.</returns>
    public List<string> GetSkillNames() {
        List<string> names = [];
        foreach (Skill skill in skills) {
            names.Add(skill.GetName());
        }
        return names;
    }

    /// <summary>
    /// Function used to return the list of the names of all the characters base abilities.
    /// </summary>
    /// <returns>A list of the characters base abilities names.</returns>
    public List<string> GetBaseAbilityNames() {
        List<string> names = [];
        foreach (Ability ability in baseAbilities) {
            names.Add(ability.GetName());
        }
        return names;
    }

    /// <summary>
    /// Private function that returns a list of abilities after equipment filters them.
    /// </summary>
    /// <returns>The list of final abilities.</returns>
    private List<Ability> GetFilteredAbilities() {
        // Use base abilities as the base.
        List<Ability> finalAbilities = baseAbilities;
        foreach (Equipment ? currentEquipment in equipment) {
            if (currentEquipment != null) {
                // Add all added abilities and remove all sealed abilities.
                finalAbilities.AddRange(currentEquipment.GetAddedAbilities());
                finalAbilities.RemoveAll(removedAbility => 
                    currentEquipment.GetSealedAbilities().Contains(removedAbility));
            }
        }
        return [.. finalAbilities.OrderByDescending(currentAbility => currentAbility.GetId())];
    }

    private readonly int id;
    private int currentMp, maxMp;
    private readonly int fieldSpriteId, menuSpriteId;
    private Equipment?[] equipment;
    private List<int> equipmentTypes;
    private List<Skill> skills;
}