namespace States.Map.Interfaces;

using Game.Map.Actors.Base;
using Utils.Interfaces;

/// <summary>
/// The public moveable actor interface, used for actors when calculate and making moves on a map.
/// </summary>
public interface IMoveableActor : ICoordinateAccessor {
    /// <summary>
    /// Adds the current elapsed time to the current duration.
    /// </summary>
    /// <param name="elapsedTime">The time passed.</param>
    public void Update(int elapsedTime);

    /// <summary>
    /// Sets a new interval to the actor controller. Used for randomizing actor walk cycles.
    /// </summary>
    /// <param name="newInterval">The new interval for actor handlers.</param>
    public void SetInterval(int newInterval);

    /// <summary>
    /// Executes the move calculated by the actor controller.
    /// </summary>
    /// <param name="newX">The new x location of the actor.</param>
    /// <param name="newY">The new y location of the actor.</param>
    public void ExecuteMove(int newX, int newY);

    /// <summary>
    /// Function that returns if the actor's duration is greater then the interval set.
    /// </summary>
    /// <returns>If the duration is greater then or equal to the fixed interval limit.</returns>
    public bool CanMove();

    /// <summary>
    /// Getter for the actor handlers interval.
    /// </summary>
    /// <returns>The interval of the actor handler.</returns>
    public int GetInterval();

    /// <summary>
    /// Function used to return the move based on the current actor controller.
    /// </summary>
    /// <returns>The move to take based on the actor controller.</returns>
    public int GetMove();

    /// <summary>
    /// Getter for the actor's behaviour.
    /// </summary>
    /// <returns>The behaviour of the actor.</returns>
    public Behaviour GetBehaviour();
}