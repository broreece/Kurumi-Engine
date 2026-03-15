namespace Config.Runtime.Battle;


/// <summary>
/// The config class for the character battle sprite config.
/// </summary>
public sealed class CharacterBattleSpriteConfig {
    /// <summary>
    /// The constructor for the character battle sprite config.
    /// </summary>
    /// <param name="characterWidth">The character's battle sprite width.</param>
    /// <param name="characterHeight">The character's battle sprite height.</param>
    public CharacterBattleSpriteConfig(int characterWidth, int characterHeight) {
        this.characterWidth = characterWidth;
        this.characterHeight = characterHeight;
    }

    /// <summary>
    /// Getter for the character width config.
    /// </summary>
    /// <returns>The character width config.</returns>
    public int GetCharacterWidth() {
        return characterWidth;
    }

    /// <summary>
    /// Getter for the character height config.
    /// </summary>
    /// <returns>The character height config.</returns>
    public int GetCharacterHeight() {
        return characterHeight;
    }

    private readonly int characterWidth, characterHeight;
}