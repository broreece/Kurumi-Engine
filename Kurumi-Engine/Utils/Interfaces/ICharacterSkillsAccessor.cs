namespace Utils.Interfaces;

using Game.Entities.Skills;

/// <summary>
/// The character skills interface, contains methods for getting the skills and abilities for playable characters.
/// </summary>
public interface ICharacterSkillsAccessor {
    /// <summary>
    /// Function that returns the base abilities + added abilities - sealed abilities.
    /// </summary>
    /// <returns>The base list of abilities the character has.</returns>
    public List<Ability> GetBaseAbilities();
    
    /// <summary>
    /// Getter for the playable character's skills.
    /// </summary>
    /// <returns>The list of skills the playable character has.</returns>
    public List<Skill> GetSkills();
}