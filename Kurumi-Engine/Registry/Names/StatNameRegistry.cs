namespace Registry.Names;

/// <summary>
/// The stat name data registry, contains data about the stat names.
/// </summary>
public sealed class StatNameRegistry {
    /// <summary>
    /// Constructor for the stat name data registry.
    /// </summary>
    /// <param name="bothNames">The 2D array containing both stat long and short names.</param>
    public StatNameRegistry(string[,] bothNames) {
        // Load 2D array including stat short names then break up the array into long and short names.
        int results = bothNames.GetLength(0);
        statNames = new string[results];
        statShortNames = new string[results];
        for (int index = 0; index < results; index ++) {
            statNames[index] = bothNames[index, 0];
            statShortNames[index] = bothNames[index, 1];
        }
    }

    /// <summary>
    /// Getter for a specific stat name.
    /// </summary>
    /// <param name="index">The index of the desired stat name.</param>
    /// <returns>The stat name at the index.</returns>
    public string GetStatName(int index) {
        return statNames[index];
    }
    
    /// <summary>
    /// Getter for the array of stat short names.
    /// </summary>
    /// <returns>The array of stat short names.</returns>
    public string[] GetStatShortNames() {
        return statShortNames;
    }

    /// <summary>
    /// Getter for a specific stat short name.
    /// </summary>
    /// <param name="index">The index of the desired stat short name.</param>
    /// <returns>The stat short name stored at the index.</returns>
    public string GetStatShortName(int index) {
        return statShortNames[index];
    }

    private readonly string[] statNames, statShortNames;
}