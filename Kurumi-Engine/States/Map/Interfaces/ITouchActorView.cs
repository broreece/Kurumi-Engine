namespace States.Map.Interfaces;

/// <summary>
/// The public touch actor view interface. Used when touching actors on the map state.
/// </summary>
public interface ITouchActorView : IScriptAccessor {
    /// <summary>
    /// Returns if the actor can be activated on touch.
    /// </summary>
    /// <returns>True: The actor activates when the party makes contact with it.
    /// False: The actor does not react when the party touches it.</returns>
    public bool ActivatesOnTouch();
}