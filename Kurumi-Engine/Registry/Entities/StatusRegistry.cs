namespace Registry.Entities;

using Game.Entities.Status;

/// <summary>
/// The status data registry, contains data about the statuses.
/// </summary>
public sealed class StatusRegistry {
    /// <summary>
    /// Constructor for the status data registry.
    /// </summary>
    /// <param name="statuses">The statuses array.</param>
    public StatusRegistry(Status[] statuses) {
        this.statuses = statuses;
    }

    /// <summary>
    /// Getter for a specific status in the statuses array.
    /// </summary>
    /// <param name="index">The index of the specific status to get.</param>
    /// <returns>The specific status.</returns>
    public Status GetStatus(int index) {
        return statuses[index];
    }

    private readonly Status[] statuses;
}