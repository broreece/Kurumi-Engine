namespace States.Map.Interfaces;

using Utils.Interfaces;

/// <summary>
/// The public tracking actor view interface. Used when checking tracked actors on the map state.
/// </summary>
public interface ITrackingActorView : IDirectionAccessor, ICoordinateAccessor {
    /// <summary>
    /// Getter for the tracking range of the actor.
    /// </summary>
    /// <returns>The tracking range of the actor.</returns>
    public int GetTrackingRange();
}