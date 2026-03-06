namespace Config.Runtime.Battle;

/// <summary>
/// The config class for the battle scene config.
/// </summary>
public sealed class BattleSceneConfig {
    /// <summary>
    /// The constructor function for the battle scene config.
    /// </summary>
    /// <param name="damageDisplayLength">The damage display length config.</param>
    public BattleSceneConfig(int damageDisplayLength) {
        this.damageDisplayLength = damageDisplayLength;
    }

    /// <summary>
    /// Getter for the damage display length config.
    /// </summary>
    /// <returns>The damage display length config.</returns>
    public int GetDamageDisplayLength() {
        return damageDisplayLength;
    }

    private readonly int damageDisplayLength;
}