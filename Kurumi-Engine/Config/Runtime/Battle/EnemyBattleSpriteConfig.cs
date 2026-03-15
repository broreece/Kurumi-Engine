namespace Config.Runtime.Battle;

/// <summary>
/// The config class for the enemy battle sprite config.
/// </summary>
public sealed class EnemyBattleSpriteConfig {
    /// <summary>
    /// The constructor for the enemy battle sprite config manager.
    /// </summary>
    /// <param name="enemyWidth">The width of the enemy battle sprite</param>
    /// <param name="enemyHeight">The height of the enemy battle sprite</param>
    public EnemyBattleSpriteConfig(int enemyWidth, int enemyHeight) {
        this.enemyWidth = enemyWidth;
        this.enemyHeight = enemyHeight;
    }

    /// <summary>
    /// Getter for the enemy width config.
    /// </summary>
    /// <returns>The enemy width config.</returns>
    public int GetEnemyWidth() {
        return enemyWidth;
    }

    /// <summary>
    /// Getter for the enemy height config.
    /// </summary>
    /// <returns>The enemy height config.</returns>
    public int GetEnemyHeight() {
        return enemyHeight;
    }

    private readonly int enemyWidth, enemyHeight;
}