namespace Save.Interfaces;

using Game.Entities.Status;

/// <summary>
/// Interface used to be able to access a statuseable entities ID and list of statuses.
/// </summary>
public interface IStatusAccessor : IIDAccessor {
    /// <summary>
    /// Getter for the entities statuses.
    /// </summary>
    /// <returns>The statuses of the entity.</returns>
    public List<Status> GetStatuses();
}