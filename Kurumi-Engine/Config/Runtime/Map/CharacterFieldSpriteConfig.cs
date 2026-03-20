namespace Config.Runtime.Map;

using UI.Interfaces;

/// <summary>
/// The config class for the character field sprite config.
/// </summary>
public sealed class CharacterFieldSpriteConfig : ICharacterDimensionsAccessor {
    /// <summary>
    /// The constructor for the character field sprite config.
    /// </summary>
    /// <param name="characterWidth">The character field sprite width.</param>
    /// <param name="characterHeight">The character field sprite height.</param>
    /// <param name="walkAnimationFrames">The character field sprite walk animation frames.</param>
    /// <param name="walkAnimationSpeed">The character field sprite walk animation speed.</param>
    public CharacterFieldSpriteConfig(int characterWidth, int characterHeight, int walkAnimationFrames, 
        int walkAnimationSpeed) {
        this.characterWidth = characterWidth;
        this.characterHeight = characterHeight;
        this.walkAnimationFrames = walkAnimationFrames;
        this.walkAnimationSpeed = walkAnimationSpeed;
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

    /// <summary>
    /// Getter for the character walk animation frames config.
    /// </summary>
    /// <returns>The character walk animation frames config.</returns>
    public int GetWalkAnimationFrames() {
        return walkAnimationFrames;
    }

    /// <summary>
    /// Getter for the character walk animation speed config.
    /// </summary>
    /// <returns>The character walk animation speed config.</returns>
    public int GetWalkAnimationSpeed() {
        return walkAnimationSpeed;
    }

    private readonly int characterWidth, characterHeight, walkAnimationFrames, walkAnimationSpeed;
}