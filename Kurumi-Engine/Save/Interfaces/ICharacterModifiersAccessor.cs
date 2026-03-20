namespace Save.Interfaces;

using Game.Items;
using Utils.Interfaces;

/// <summary>
/// Interface used to be able to access a character modifiers such as elemental resistances, equipment, stats and 
/// skills.
/// </summary>
public interface ICharacterModifiersAccessor : ICharacterSkillsAccessor, ICharacterStatsAccessor, IIDAccessor {
    /// <summary>
    /// Getter for the entities stats.
    /// </summary>
    /// <returns>The stats of the entity.</returns>
    public int[] GetStats();

    /// <summary>
    /// Getter for the entities elemental resistances.
    /// </summary>
    /// <returns>The elemental resistances of the entity.</returns>
    public int[] GetElementalResistances();

    /// <summary>
    /// Getter for the entities status resistances.
    /// </summary>
    /// <returns>The status resistances of the entity.</returns>
    public int[] GetStatusResistances();

    /// <summary>
    /// Getter for the playable character's possible equipment types.
    /// </summary>
    /// <returns>The list of equipment types the playable character can use.</returns>
    public List<int> GetEquipmentTypes();

    /// <summary>
    /// Getter for the characters equipment.
    /// </summary>
    /// <returns>The characters equipment.</returns>
    public Equipment?[] GetEquipment();
}