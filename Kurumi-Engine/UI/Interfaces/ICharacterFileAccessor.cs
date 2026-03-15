namespace UI.Interfaces;

using Save.Interfaces;

/// <summary>
/// Character file function accessor interface, used to access a character's information required for save files.
/// </summary>
public interface ICharacterFileAccessor : ISaveableCharacter {
    /// <summary>
    /// Getter for the field sprite id.
    /// </summary>
    /// <returns>The field sprite id.</returns>
    public int GetFieldSpriteId();
}