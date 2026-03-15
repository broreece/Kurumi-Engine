namespace States.Map.Interfaces;

/// <summary>
/// The public map scene view interface, used to control visibility between scene and state.
/// </summary>
public interface IMapSceneView {
    /// <summary>
    /// Function used to determine action when a party moves.
    /// </summary>
    /// <param name="x">The x location being moved to.</param>
    /// <param name="y">The y location being moved to.</param>
    public void OnPartyMoved(int x, int y);

    /// <summary>
    /// Resets a provided actor walk animation clock.
    /// </summary>
    /// <param name="actorIndex">The actor index of the walk animation clock to reset.</param>
    public void ResetActorWalkAnimationClock(int actorIndex);

    /// <summary>
    /// Function used to reset the forced movement clock.
    /// </summary>
    public void ResetForcedMovementClock();

    /// <summary>
    /// Function used to tick the movement clock when the party is being forced to move.
    /// </summary>
    public int TickForcedMovementClock();

    /// <summary>
    /// Ticks a specified actor handler clock by resetting the clock and returning it's stored time.
    /// </summary>
    /// <param name="index">The actor handler's index.</param>
    /// <returns>The time passed since last tick.</returns>
    public int TickActorControllerClock(int index);

    /// <summary>
    /// Function used to generate a random interval by using the standard value and the map scenes variance.
    /// </summary>
    /// <param name="standardValue">The standard value before randomization.</param>
    /// <returns>A new randomized value.</returns>
    public int GenerateRandomInterval(int standardValue);
}