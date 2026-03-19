namespace Game.Maps.ActorControllers;

using States.Map.Interfaces;

/// <summary>
/// The actor controller class, used to timing and movement of actors.
/// Inherited by pathed actor handlers and random actor handlers.
/// </summary>
public abstract class ActorController : IFinishable {
    /// <summary>
    /// Constructor for the actor controller.
    /// </summary>
    /// <param name="interval">The interval at which the actor moves.</param>
    /// <param name="xLocation">The stored x location of the actor.</param>
    /// <param name="yLocation">The stored y location of the actor.</param>
    protected ActorController(int interval, int xLocation, int yLocation) {
        // Clock and duration set initally.
        duration = 0;

        this.interval = interval;
        this.xLocation = xLocation;
        this.yLocation = yLocation;
    }

    /// <summary>
    /// Adds the current elapsed time to the current duration.
    /// </summary>
    /// <param name="elapsedTime">The time passed.</param>
    public void Update(int elapsedTime) {
        duration += elapsedTime;
    }

    /// <summary>
    /// Function used to return the move based on the current actor controller.
    /// </summary>
    /// <returns>The move to take based on the actor controller.</returns>
    public abstract int GetMove(); 

    /// <summary>
    /// Executes the move calculated by the actor controller.
    /// </summary>
    /// <param name="newX">The new x location of the actor.</param>
    /// <param name="newY">The new y location of the actor.</param>
    public virtual void ExecuteMove(int newX, int newY) {
        xLocation = newX;
        yLocation = newY;
    }

    /// <summary>
    /// Getter for the actor handlers interval.
    /// </summary>
    /// <returns>The interval of the actor handler.</returns>
    public int GetInterval() {
        return interval;
    }

    /// <summary>
    /// Sets a new interval to the actor controller. Used for randomizing actor walk cycles.
    /// </summary>
    /// <param name="newInterval">The new interval for actor handlers.</param>
    public void SetInterval(int newInterval) {
        interval = newInterval;
    }

    /// <summary>
    /// Getter for the x location of the actor controller.
    /// </summary>
    /// <returns>The x location of the actor controller.</returns>
    public int GetXLocation() {
        return xLocation;
    }

    /// <summary>
    /// Getter for the y location of the actor controller.
    /// </summary>
    /// <returns>The y location of the actor controller.</returns>
    public int GetYLocation() {
        return yLocation;
    }

    /// <summary>
    /// Function that returns if the actor's duration is greater then the interval set.
    /// </summary>
    /// <returns>If the duration is greater then or equal to the fixed interval limit.</returns>
    public bool CanMove() {
        return duration >= interval;
    }

    /// <summary>
    /// Function that returns if the actor controller has finished it's movements and should be popped from the actor controller stack.
    /// </summary>
    /// <returns>True if the controller has finished it's action, false otherwise.</returns>
    public virtual bool IsFinished() {
        return false;
    }

    protected int duration;
    protected int interval;
    protected int xLocation, yLocation;
}