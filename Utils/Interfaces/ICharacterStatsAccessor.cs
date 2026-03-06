namespace Utils.Interfaces;

/// <summary>
/// The character stats interface, contains methods for getting the HP and MP stat values for playable characters.
/// </summary>
public interface ICharacterStatsAccessor {
    /// <summary>
    /// Getter for the namable elements names.
    /// </summary>
    /// <returns>The name of the savable element.</returns>
    public string GetName();

    /// <summary>
    /// Getter for the entities max hp value.
    /// </summary>
    /// <returns>The max hp value of the entity.</returns>
    public int GetMaxHp();

    /// <summary>
    /// Getter for the current hp of the character.
    /// </summary>
    /// <returns>The current hp of the character.</returns>
    public int GetCurrentHp();

    /// <summary>
    /// Getter for the max mp of the character.
    /// </summary>
    /// <returns>The max mp of the character.</returns>
    public int GetMaxMp();

    /// <summary>
    /// Getter for the current mp of the character.
    /// </summary>
    /// <returns>The current mp of the character.</returns>
    public int GetCurrentMp();
}