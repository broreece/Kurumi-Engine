namespace Game.Maps.Actors.Core;

using Game.Maps.ActorControllers;
using Game.Maps.Actors.Base;
using Game.Maps.Elements;
using Scenes.Map.Interfaces;
using Scripts.MapScripts.Base;
using States.Map.Interfaces;

/// <summary>
/// Actors are map elements that contain a list of possible scripts as well as information surronding it's appearence 
/// and how it's activated.
/// </summary>
public class Actor : MapElement, IActorView, IActionActorView, ITouchActorView, ITrackingActorWithScriptView, 
    IMoveableActor, IPassabilityAccessor {
    /// <summary>
    /// Constructor for the actor class.
    /// </summary>
    /// <param name="xLocation">The x coordinate of the actor.</param>
    /// <param name="yLocation">The y coordinate of the actor.</param>
    /// <param name="actorInfo">The actor info of the actor.</param>
    /// <param name="direction">The direction of the actor.</param>
    /// <param name="visible">If the actor is visible.</param>
    public Actor(int xLocation, int yLocation, ActorInfo actorInfo, int direction, bool visible) 
        : base(xLocation, yLocation, direction, visible) {
        this.actorInfo = actorInfo;

        // Check the behaviour of the actor.
        actorControllers = new Stack<ActorController>();
        switch ((Behaviour) actorInfo.GetBehaviour()) {
            case Behaviour.RandomMovement:
                actorControllers.Push(new RandomActorController(actorInfo.GetMovementSpeed(), xLocation, yLocation));
                break;

            case Behaviour.FollowsPath:
                actorControllers.Push(new PathedActorController(GetMovementSpeed(), xLocation, yLocation, 
                    forcedMovement: false, actorInfo.GetPath()));
                break;

            default:
                break;
        }
    }

    /// <summary>
    /// Function used to push a controller onto the actor.
    /// </summary>
    /// <param name="actorController">The new actor controller to go on top of the actor controllers.</param>
    public void PushController(ActorController actorController) {
        actorControllers.Push(actorController);
    }

    /// <summary>
    /// Function used to pop a controller from the actor.
    /// </summary>
    public void PopController() {
        actorControllers.Pop();
    }

    /// <summary>
    /// Adds the current elapsed time to the current duration.
    /// </summary>
    /// <param name="elapsedTime">The time passed.</param>
    public void Update(int elapsedTime) {
        if (actorControllers.Count > 0) {
            actorControllers.Peek().Update(elapsedTime);
        }
    }

    /// <summary>
    /// Executes the move calculated by the actor controller.
    /// </summary>
    /// <param name="newX">The new x location of the actor.</param>
    /// <param name="newY">The new y location of the actor.</param>
    public void ExecuteMove(int newX, int newY) {
        actorControllers.Peek().ExecuteMove(newX, newY);
    }

    /// <summary>
    /// Sets a new interval to the actor controller. Used for randomizing actor walk cycles.
    /// </summary>
    /// <param name="newInterval">The new interval for actor handlers.</param>
    public void SetInterval(int newInterval) {
        actorControllers.Peek().SetInterval(newInterval);
    }

    /// <summary>
    /// Setter for the movement speed of the actor.
    /// </summary>
    /// <param name="newMovementSpeed">The new movement speed of the actor.</param>
    public void SetMovementSpeed(int newMovementSpeed) {
        actorInfo.SetMovementSpeed(newMovementSpeed);
    }

    /// <summary>
    /// Setter for the tracking range of the actor.
    /// </summary>
    /// <param name="newTrackingRange">The new tracking range of the actor.</param>
    public void SetTrackingRange(int newTrackingRange) {
        actorInfo.SetTrackingRange(newTrackingRange);
    }

    /// <summary>
    /// Sets if the actor should appear below the party.
    /// </summary>
    /// <param name="newBelowParty">Sets if the field sprite id of the actor appears below the party.</param>
    public void SetBelowParty(bool newBelowParty) {
        actorInfo.SetBelowParty(newBelowParty);
    }

    /// <summary>
    /// Sets if the actor is passable.
    /// </summary>
    /// <param name="newPassable">The new passability of the actor.</param>
    public void SetPassable(bool newPassable) {
        actorInfo.SetPassable(newPassable);
    }

    /// <summary>
    /// Sets if the actor can be activated on touch.
    /// </summary>
    /// <param name="newOnTouch">The new setting for if the actor can be activated on touch.</param>
    public void SetOnTouch(bool newOnTouch) {
        actorInfo.SetOnTouch(newOnTouch);
    }

    /// <summary>
    /// Sets if the actor will activate on find.
    /// </summary>
    /// <param name="newOnFind">The new setting for if the actor can activate when player walks into found 
    /// range.</param>
    public void SetOnFind(bool newOnFind) {
        actorInfo.SetOnFind(newOnFind);
    }

    /// <summary>
    /// Sets if the actor can be activated automatically.
    /// </summary>
    /// <param name="newAuto">The new setting for if the actor can be activated automatically.</param>
    public void SetAuto(bool newAuto) {
        actorInfo.SetAuto(newAuto);
    }

    /// <summary>
    /// Sets if the actor can be activated on interaction.
    /// </summary>
    /// <param name="newOnAction">The new setting for if the actor can be activated when the party interacts with 
    /// it.</param>
    public void SetOnAction(bool newOnAction) {
        actorInfo.SetOnAction(newOnAction);
    }

    /// <summary>
    /// Getter for the field sprite ID of the actor.
    /// </summary>
    /// <returns>The field sprite ID of the actor.</returns>
    public int GetFieldSpriteId() {
        return actorInfo.GetFieldSpriteId();
    }

    /// <summary>
    /// Getter for the width of the actor.
    /// </summary>
    /// <returns>The field sprite width of the actor.</returns>
    public int GetWidth() {
        return actorInfo.GetWidth();
    }

    /// <summary>
    /// Getter for the height of the actor.
    /// </summary>
    /// <returns>The field sprite height of the actor.</returns>
    public int GetHeight() {
        return actorInfo.GetHeight();
    }
    
    /// <summary>
    /// Getter for the actor's movement speed.
    /// </summary>
    /// <returns>The movement speed of the actor.</returns>
    public int GetMovementSpeed() {
        return actorInfo.GetMovementSpeed();
    }
    
    /// <summary>
    /// Function used to return the move based on the current actor controller.
    /// </summary>
    /// <returns>The move to take based on the actor controller.</returns>
    public int GetMove() {
        if (actorControllers.Peek().IsFinished()) {
            actorControllers.Pop();
        }
        if (actorControllers.Count == 0) {
            return -1;
        }
        return actorControllers.Peek().GetMove();
    }

    /// <summary>
    /// Getter for the actor handlers interval.
    /// </summary>
    /// <returns>The interval of the actor handler.</returns>
    public int GetInterval() {
        return actorControllers.Peek().GetInterval();
    }

    /// <summary>
    /// Getter for the tracking range of the actor.
    /// </summary>
    /// <returns>The tracking range of the actor.</returns>
    public int GetTrackingRange() {
        return actorInfo.GetTrackingRange();
    }

    /// <summary>
    /// Function that returns if the actor's duration is greater then the interval set.
    /// </summary>
    /// <returns>If the duration is greater then or equal to the fixed interval limit.</returns>
    public bool CanMove() {
        if (actorControllers.Count > 0) {
            return actorControllers.Peek().CanMove();
        }
        return false;
    }

    /// <summary>
    /// Returns if the actor's field sprite appears below the party.
    /// </summary>
    /// <returns>True: The field sprite will be drawn on the map scene before the party.
    /// False: The field sprite is drawn after the party, will appear above.</returns>
    public bool IsBelowParty() {
        return actorInfo.IsBelowParty();
    }

    /// <summary>
    /// Returns if the actor is passable.
    /// </summary>
    /// <returns>True: The actor can be walked over.
    /// False: The actor is not passable.</returns>
    public bool IsPassable() {
        return actorInfo.IsPassable();
    }

    /// <summary>
    /// Returns if the actor can be activated on touch.
    /// </summary>
    /// <returns>True: The actor activates when the party makes contact with it.
    /// False: The actor does not react when the party touches it.</returns>
    public bool ActivatesOnTouch() {
        return actorInfo.ActivatesOnTouch();
    }

    /// <summary>
    /// Returns if the actor can be activated automatically.
    /// </summary>
    /// <returns>True: The actor activates as soon as the map loads.
    /// False: The actor does not activate automatically.</returns>
    public bool ActivatesAutomatically() {
        return actorInfo.ActivatesAutomatically();
    }

    /// <summary>
    /// Returns if the actor can be activated on action.
    /// </summary>
    /// <returns>True: The actor activates when the party interacts with the actor.
    /// False: The actor does not activate when the party interacts with the actor.</returns>
    public bool ActivatesOnAction() {
        return actorInfo.ActivatesOnAction();
    }
    
    /// <summary>
    /// Returns if the actor activates when the entity finds the player.
    /// </summary>
    /// <returns>True: The actor activates when it finds the player.
    /// False: The actor does not activate when it finds the player.</returns>
    public bool ActivatesOnFind() {
        return actorInfo.ActivatesOnFind();
    }

    /// <summary>
    /// Getter for the actor's behaviour.
    /// </summary>
    /// <returns>The behaviour of the actor.</returns>
    public Behaviour GetBehaviour() {
        return (Behaviour) actorInfo.GetBehaviour();
    }

    /// <summary>
    /// Getter for the actor's linked script.
    /// </summary>
    /// <returns>A script (if any) that is linked to the actor.</returns>
    public MapScript GetScript() {
        return actorInfo.GetScript();
    }

    // Info and controller stack.
    private readonly ActorInfo actorInfo;
    private readonly Stack<ActorController> actorControllers;
}