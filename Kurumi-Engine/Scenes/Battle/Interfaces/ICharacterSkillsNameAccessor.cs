namespace Scenes.Battle.Interfaces;

/// <summary>
/// The character skills name accessor interface. Used to access a playable characters skills and abilities.
/// </summary>
public interface ICharacterSkillsNameAccessor {
    /// <summary>
    /// Function used to return the list of the names of all the characters skills.
    /// </summary>
    /// <returns>A list of the characters skills names.</returns>
    public List<string> GetSkillNames();

    /// <summary>
    /// Function used to return the list of the names of all the characters base abilities.
    /// </summary>
    /// <returns>A list of the characters base abilities names.</returns>
    public List<string> GetBaseAbilityNames();
}