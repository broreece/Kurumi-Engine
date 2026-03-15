namespace Config.Runtime.Game;

/// <summary>
/// The config class for the game config.
/// </summary>
public sealed class GameConfig {
    /// <summary>
    /// The constructor for the game config.
    /// </summary>
    /// <param name="maxPartySize">The max party size config.</param>
    /// <param name="maxEnemyFormationSize">The max enemy formation size config.</param>
    /// <param name="saveFiles">The max number of sace files config.</param>
    /// <param name="agilityStatIndex">The stat index of agility, used in the battle state.</param>
    public GameConfig(int maxPartySize, int maxEnemyFormationSize, int saveFiles, int agilityStatIndex) {
        this.maxPartySize = maxPartySize;
        this.maxEnemyFormationSize = maxEnemyFormationSize;
        this.saveFiles = saveFiles;
        this.agilityStatIndex = agilityStatIndex;
    }

    /// <summary>
    /// Getter for the max party size config.
    /// </summary>
    /// <returns>The max party size config.</returns>
    public int GetMaxPartySize() {
        return maxPartySize;
    }

    /// <summary>
    /// Getter for the max enemy formation size config.
    /// </summary>
    /// <returns>The max enemy formation size config.</returns>
    public int GetMaxEnemyFormationSize() {
        return maxEnemyFormationSize;
    }

    /// <summary>
    /// Getter for the max number of save files config.
    /// </summary>
    /// <returns>The max number of save files config.</returns>
    public int GetSaveFiles() {
        return saveFiles;
    }

    /// <summary>
    /// Getter for the agility stat index config.
    /// </summary>
    /// <returns>The agility stat index config.</returns>
    public int GetAgilityStatIndex() {
        return agilityStatIndex;
    }

    private readonly int maxPartySize, maxEnemyFormationSize, saveFiles, agilityStatIndex;
}