namespace Config.Runtime.Battle;

/// <summary>
/// The config class for the battle background sprite config.
/// </summary>
public sealed class BattleBackgroundSpriteConfig {
    /// <summary>
    /// The constructor function for the battle background sprite config.
    /// </summary>
    /// <param name="battleBackgroundWidth">The battle background width config.</param>
    /// <param name="battleBackgroundHeight">The battle background height config.</param>
    public BattleBackgroundSpriteConfig(int battleBackgroundWidth, int battleBackgroundHeight) {
        this.battleBackgroundWidth = battleBackgroundWidth;
        this.battleBackgroundHeight = battleBackgroundHeight;
    }

    /// <summary>
    /// Getter for the battle background width config.
    /// </summary>
    /// <returns>The battle background width config.</returns>
    public int GetBattleBackgroundWidth() {
        return battleBackgroundWidth;
    }

    /// <summary>
    /// Getter for the battle background height config.
    /// </summary>
    /// <returns>The battle background height config.</returns>
    public int GetBattleBackgroundHeight() {
        return battleBackgroundHeight;
    }

    private readonly int battleBackgroundWidth, battleBackgroundHeight;
}