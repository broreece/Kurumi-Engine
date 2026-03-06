namespace UI.Interfaces;

/// <summary>
/// Dimension accessor interface, used to access a character's width and height.
/// </summary>
public interface ICharacterDimensionsAccessor {
    /// <summary>
    /// Getter for the character width stored in the character config.
    /// </summary>
    /// <returns>The character width in config.</returns>
    public int GetCharacterWidth();

    /// <summary>
    /// Getter for the character height stored in the character config.
    /// </summary>
    /// <returns>The character height in config.</returns>
    public int GetCharacterHeight();
}
