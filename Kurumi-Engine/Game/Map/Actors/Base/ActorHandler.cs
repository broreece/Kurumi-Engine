namespace Game.Map.Actors.Base;

using Game.Map.ActorControllers;
using Game.Map.Elements;

/// <summary>
/// Actor handlers are containers for actor objects. They exist to allow actors to exist independently from actor controllers but also
/// perform the same functions allowing for creation of actors before their AI is created.
/// </summary>
public abstract class ActorHandler : IActorHandler {
    /// <summary>
    /// Constructor for the actor handler class.
    /// </summary>
    /// <param name="actor">The existing actor object.</param>
    protected ActorHandler(IActorHandler actor) {
        // Assign actor value and copy script.
        this.actor = actor;
    }

    /// <summary>
    /// Function used to push a controller onto the actor.
    /// </summary>
    /// <param name="actorController">The new actor controller to go on top of the actor controllers.</param>
    public void PushController(ActorController actorController) {
        actor.PushController(actorController);
    }

    /// <summary>
    /// Function used to pop a controller from the actor.
    /// </summary>
    public void PopController() {
        actor.PopController();
    }

    /// <summary>
    /// Adds the current elapsed time to the current duration.
    /// </summary>
    /// <param name="elapsedTime">The time passed.</param>
    public void Update(int elapsedTime) {
        actor.Update(elapsedTime);
    }

    /// <summary>
    /// Executes the move calculated by the actor controller.
    /// </summary>
    /// <param name="newX">The new x location of the actor.</param>
    /// <param name="newY">The new y location of the actor.</param>
    public void ExecuteMove(int newX, int newY) {
        actor.ExecuteMove(newX, newY);
    }

    /// <summary>
    /// Sets a new interval to the actor controller. Used for randomizing actor walk cycles.
    /// </summary>
    /// <param name="newInterval">The new interval for actor handlers.</param>
    public void SetInterval(int newInterval) {
        actor.SetInterval(newInterval);
    }

    /// <summary>
    /// Sets the map elements direction.
    /// </summary>
    /// <param name="newDirection">The new direction of the map element.</param>
    public void SetDirection(Direction direction) {
        actor.SetDirection(direction);
    }

    /// <summary>
    /// Sets the map element's previous X location.
    /// </summary>
    /// <param name="oldX">The previous X coordinate of the map element.</param>
    public void SetOldXLocation(int oldXLocation) {
        actor.SetOldXLocation(oldXLocation);
    }

    /// <summary>
    /// Sets the position provider's X location.
    /// </summary>
    /// <param name="newX">The new X coordinate of the position provider.</param>
    public void SetXLocation(int xLocation) {
        actor.SetXLocation(xLocation);
    }

    /// <summary>
    /// Sets the map element's previous Y location.
    /// </summary>
    /// <param name="newX">The previous Y coordinate of the map element.</param>
    public void SetOldYLocation(int oldYLocation) {
        actor.SetOldYLocation(oldYLocation);
    }

    /// <summary>
    /// Sets the position provider's Y location.
    /// </summary>
    /// <param name="newX">The new Y coordinate of the position provider.</param>
    public void SetYLocation(int yLocation) {
        actor.SetYLocation(yLocation);
    }

    /// <summary>
    /// Sets the map element's current animation frame.
    /// </summary>
    /// <param name="newAnimationFrame">The new current animation frame of the map element.</param>
    public void SetCurrentAnimationFrame(int animationFrame) {
        actor.SetCurrentAnimationFrame(animationFrame);
    }

    /// <summary>
    /// Function used to return the move based on the current actor controller.
    /// </summary>
    /// <returns>The move to take based on the actor controller.</returns>
    public int GetMove() {
        return actor.GetMove();
    }

    /// <summary>
    /// Getter for the actor handlers interval.
    /// </summary>
    /// <returns>The interval of the actor handler.</returns>
    public int GetInterval() {
        return actor.GetInterval();
    }

    /// <summary>
    /// Getter for the field sprite id of the actor.
    /// </summary>
    /// <returns>The field sprite id of the actor.</returns>
    public int GetFieldSpriteId() {
        return actor.GetFieldSpriteId();
    }

    /// <summary>
    /// Gets the map element's previous X location.
    /// </summary>
    /// <returns>The map elements previous X coordinate.</returns>
    public int GetOldXLocation() {
        return actor.GetOldXLocation();
    }

    /// <summary>
    /// Gets the map element's X location.
    /// </summary>
    /// <returns>The map elements X coordinate.</returns>
    public int GetXLocation() {
        return actor.GetXLocation();
    }

    /// <summary>
    /// Gets the map element's previous Y location.
    /// </summary>
    /// <returns>The map elements previous Y coordinate.</returns>
    public int GetOldYLocation() {
        return actor.GetOldYLocation();
    }

    /// <summary>
    /// Gets the map element's Y location.
    /// </summary>
    /// <returns>The map elements Y coordinate.</returns>
    public int GetYLocation() {
        return actor.GetYLocation();
    }

    /// <summary>
    /// Getter for the map elements direction.
    /// </summary>
    /// <returns>The direction the map element is facing.</returns>
    public int GetDirection() {
        return actor.GetDirection();
    }

    /// <summary>
    /// Gets the map element's current animation frame.
    /// </summary>
    /// <returns>The map elements current animation frame.</returns>
    public int GetCurrentAnimationFrame() {
        return actor.GetCurrentAnimationFrame();
    }

    /// <summary>
    /// Getter for the actor's movement speed.
    /// </summary>
    /// <returns>The movement speed of the actor.</returns>
    public int GetMovementSpeed(){
        return actor.GetMovementSpeed();
    }

    /// <summary>
    /// Getter for the tracking range of the actor.
    /// </summary>
    /// <returns>The tracking range of the actor.</returns>
    public int GetTrackingRange() {
        return actor.GetTrackingRange();
    }

    /// <summary>
    /// Function that returns if the actor's duration is greater then the interval set.
    /// </summary>
    /// <returns>If the duration is greater then or equal to the fixed interval limit.</returns>
    public bool CanMove() {
        return actor.CanMove();
    }

    /// <summary>
    /// Returns if the actor is passable.
    /// </summary>
    /// <returns>True: The actor can be walked over.
    /// False: The actor is not passable.</returns>
    public bool IsPassable() {
        return actor.IsPassable();
    }

    /// <summary>
    /// Returns if the actor can be activated on touch.
    /// </summary>
    /// <returns>True: The actor activates when the party makes contact with it.
    /// False: The actor does not react when the party touches it.</returns>
    public bool ActivatesOnTouch() {
        return actor.ActivatesOnTouch();
    }

    /// <summary>
    /// Returns if the actor can be activated on action.
    /// </summary>
    /// <returns>True: The actor activates when the party interacts with the actor.
    /// False: The actor does not activate when the party interacts with the actor.</returns>
    public bool ActivatesOnAction() {
        return actor.ActivatesOnAction();
    }

    /// <summary>
    /// Returns if the actor can be activated automatically.
    /// </summary>
    /// <returns>True: The actor activates as soon as the map loads.
    /// False: The actor does not activate automatically.</returns>
    public bool ActivatesAutomatically() {
        return actor.ActivatesAutomatically();
    }

    /// <summary>
    /// Returns if the actor activates when the entity finds the player.
    /// </summary>
    /// <returns>True: The actor activates when it finds the player.
    /// False: The actor does not activate when it finds the player.</returns>
    public bool ActivatesOnFind() {
        return actor.ActivatesOnFind();
    }

    /// <summary>
    /// Getter for the actor's behaviour.
    /// </summary>
    /// <returns>The behaviour of the actor.</returns>
    public Behaviour GetBehaviour(){
        return actor.GetBehaviour();
    }

    /// <summary>
    /// Gets the actor's script.
    /// </summary>
    /// <returns>The script that the actor stores.</returns>
    public string GetScript(){
        return actor.GetScript();
    }

    protected readonly IActorHandler actor;
}