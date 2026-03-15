namespace Config.Runtime.Map;

/// <summary>
/// The config class for the map config.
/// </summary>
public sealed class MapConfig {
    /// <summary>
    /// The constructor for the map config.
    /// </summary>
    /// <param name="maxTilesWide">The max tiles wide a map can be.</param>
    /// <param name="maxTilesHigh">The max tiles high a map can be.</param>
    /// <param name="randomActorMoveVariance">The random actor move variance.</param>
    public MapConfig(int maxTilesWide, int maxTilesHigh, int randomActorMoveVariance) {
        this.maxTilesWide = maxTilesWide;
        this.maxTilesHigh = maxTilesHigh;
        this.randomActorMoveVariance = randomActorMoveVariance;
    }

    /// <summary>
    /// Getter for the map max tiles wide config.
    /// </summary>
    /// <returns>The map max tiles wide config.</returns>
    public int GetMaxTilesWide() {
        return maxTilesWide;
    }

    /// <summary>
    /// Getter for the map max tiles high config.
    /// </summary>
    /// <returns>The map max tiles high config.</returns>
    public int GetMaxTilesHigh() {
        return maxTilesHigh;
    }

    /// <summary>
    /// Getter for the random actor movement variance config.
    /// </summary>
    /// <returns>The random actor movement variance config.</returns>
    public int GetActorMoveVariance() {
        return randomActorMoveVariance;
    }

    private readonly int maxTilesWide, maxTilesHigh, randomActorMoveVariance;
}