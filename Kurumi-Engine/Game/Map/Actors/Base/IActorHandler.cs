namespace Game.Map.Actors.Base;

using Game.Map.ActorControllers;
using Game.Map.Elements;
using Scenes.Map.Interfaces;

/// <summary>
/// The public actor handler, used to handle the actor's movement patterns, and state functions.
/// </summary>
public interface IActorHandler : IActorHandlerView {
    /// <summary>
    /// Function used to push a controller onto the actor.
    /// </summary>
    /// <param name="actorController">The new actor controller to go on top of the actor controllers.</param>
    public void PushController(ActorController actorController);

    /// <summary>
    /// Function used to pop a controller from the actor.
    /// </summary>
    public void PopController();

    /// <summary>
    /// Adds the current elapsed time to the current duration.
    /// </summary>
    /// <param name="elapsedTime">The time passed.</param>
    public void Update(int elapsedTime);

    /// <summary>
    /// Executes the move calculated by the actor controller.
    /// </summary>
    /// <param name="newX">The new x location of the actor.</param>
    /// <param name="newY">The new y location of the actor.</param>
    public void ExecuteMove(int newX, int newY);

    /// <summary>
    /// Sets a new interval to the actor controller. Used for randomizing actor walk cycles.
    /// </summary>
    /// <param name="newInterval">The new interval for actor handlers.</param>
    public void SetInterval(int newInterval);

    /// <summary>
    /// Sets the map elements direction.
    /// </summary>
    /// <param name="newDirection">The new direction of the map element.</param>
    public void SetDirection(Direction direction);

    /// <summary>
    /// Sets the map element's previous X location.
    /// </summary>
    /// <param name="oldX">The previous X coordinate of the map element.</param>
    public void SetOldXLocation(int oldXLocation);

    /// <summary>
    /// Sets the position provider's X location.
    /// </summary>
    /// <param name="newX">The new X coordinate of the position provider.</param>
    public void SetXLocation(int xLocation);

    /// <summary>
    /// Sets the map element's previous Y location.
    /// </summary>
    /// <param name="newX">The previous Y coordinate of the map element.</param>
    public void SetOldYLocation(int oldYLocation);

    /// <summary>
    /// Sets the position provider's Y location.
    /// </summary>
    /// <param name="newX">The new Y coordinate of the position provider.</param>
    public void SetYLocation(int yLocation);

    /// <summary>
    /// Function used to return the move based on the current actor controller.
    /// </summary>
    /// <returns>The move to take based on the actor controller.</returns>
    public int GetMove();

    /// <summary>
    /// Getter for the actor handlers interval.
    /// </summary>
    /// <returns>The interval of the actor handler.</returns>
    public int GetInterval();

    /// <summary>
    /// Getter for the actor's movement speed.
    /// </summary>
    /// <returns>The movement speed of the actor.</returns>
    public int GetMovementSpeed();

    /// <summary>
    /// Getter for the tracking range of the actor.
    /// </summary>
    /// <returns>The tracking range of the actor.</returns>
    public int GetTrackingRange();

    /// <summary>
    /// Function that returns if the actor's duration is greater then the interval set.
    /// </summary>
    /// <returns>If the duration is greater then or equal to the fixed interval limit.</returns>
    public bool CanMove();

    /// <summary>
    /// Returns if the actor is passable.
    /// </summary>
    /// <returns>True: The actor can be walked over.
    /// False: The actor is not passable.</returns>
    public bool IsPassable();

    /// <summary>
    /// Returns if the actor can be activated on touch.
    /// </summary>
    /// <returns>True: The actor activates when the party makes contact with it.
    /// False: The actor does not react when the party touches it.</returns>
    public bool ActivatesOnTouch();

    /// <summary>
    /// Returns if the actor can be activated on action.
    /// </summary>
    /// <returns>True: The actor activates when the party interacts with the actor.
    /// False: The actor does not activate when the party interacts with the actor.</returns>
    public bool ActivatesOnAction();

    /// <summary>
    /// Returns if the actor can be activated automatically.
    /// </summary>
    /// <returns>True: The actor activates as soon as the map loads.
    /// False: The actor does not activate automatically.</returns>
    public bool ActivatesAutomatically();

    /// <summary>
    /// Returns if the actor activates when the entity finds the player.
    /// </summary>
    /// <returns>True: The actor activates when it finds the player.
    /// False: The actor does not activate when it finds the player.</returns>
    public bool ActivatesOnFind();

    /// <summary>
    /// Getter for the actor's behaviour.
    /// </summary>
    /// <returns>The behaviour of the actor.</returns>
    public Behaviour GetBehaviour();

    /// <summary>
    /// Gets the actor's script.
    /// </summary>
    /// <returns>The script that the actor stores.</returns>
    public string GetScript();
}