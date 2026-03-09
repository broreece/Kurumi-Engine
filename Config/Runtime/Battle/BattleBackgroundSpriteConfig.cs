namespace Config.Runtime.Battle;

/// <summary>
/// The config class for the battle background sprite config.
/// </summary>
public sealed class BattleBackgroundSpriteConfig {
    /// <summary>
    /// The constructor function for the battle background sprite config.
    /// </summary>
    /// <param name="width">The battle background width config.</param>
    /// <param name="height">The battle background height config.</param>
    public BattleBackgroundSpriteConfig(int width, int height) {
        this.width = width;
        this.height = height;
    }

    /// <summary>
    /// Getter for the battle background width config.
    /// </summary>
    /// <returns>The battle background width config.</returns>
    public int GetWidth() {
        return width;
    }

    /// <summary>
    /// Getter for the battle background height config.
    /// </summary>
    /// <returns>The battle background height config.</returns>
    public int GetHeight() {
        return height;
    }

    private readonly int width, height;
}