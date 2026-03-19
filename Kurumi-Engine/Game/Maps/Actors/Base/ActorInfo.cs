namespace Game.Maps.Actors.Base;

/// <summary>
/// The actor info class, contains actor static information stored in the game database.
/// </summary>
public class ActorInfo {
    /// <summary>
    /// Constructor for the actor information class.
    /// </summary>
    /// <param name="behaviour">The behaviour of the actor.</param>
    /// <param name="actorSprite">The actor sprite of the actor.</param>
    /// <param name="movementSpeed">The movement speed of the actor.</param>
    /// <param name="trackingRange">The tracking range of the actor.</param>
    /// <param name="belowParty">If the actor appears below the party.</param>
    /// <param name="passable">If the actor is passive.</param>
    /// <param name="onTouch">If the actor's script activates on touch.</param>
    /// <param name="auto">If the actor's script activates automatically.</param>
    /// <param name="onAction">If the actor's script activates on action.</param>
    /// <param name="onFind">If the actor's script activates on find.</param>
    /// <param name="path">The path of the actor.</param>
    /// <param name="scriptId">The linked script to the actor.</param>
    public ActorInfo(int behaviour, ActorSprite actorSprite, int movementSpeed, int trackingRange, bool belowParty, bool passable, bool onTouch, 
        bool auto, bool onAction, bool onFind, List<int> path, int scriptId) {
        this.behaviour = behaviour;
        this.actorSprite = actorSprite;
        this.movementSpeed = movementSpeed;
        this.trackingRange = trackingRange;
        this.belowParty = belowParty;
        this.passable = passable;
        this.onTouch = onTouch;
        this.auto = auto;
        this.onAction = onAction;
        this.onFind = onFind;
        this.path = path;
        this.scriptId = scriptId;
    }
        
    /// <summary>
    /// Setter for the movement speed of the actor.
    /// </summary>
    /// <param name="newMovementSpeed">The new movement speed of the actor.</param>
    public void SetMovementSpeed(int newMovementSpeed) {
        movementSpeed = newMovementSpeed;
    }

    /// <summary>
    /// Setter for the tracking range of the actor.
    /// </summary>
    /// <param name="newTrackingRange">The new tracking range of the actor.</param>
    public void SetTrackingRange(int newTrackingRange) {
        trackingRange = newTrackingRange;
    }

    /// <summary>
    /// Sets if the actor should appear below the party.
    /// </summary>
    /// <param name="newBelowParty">Sets if the field sprite id of the actor appears below the party.</param>
    public void SetBelowParty(bool newBelowParty) {
        belowParty = newBelowParty;
    }

    /// <summary>
    /// Sets if the actor is passable.
    /// </summary>
    /// <param name="newPassable">The new passability of the actor.</param>
    public void SetPassable(bool newPassable) {
        passable = newPassable;
    }

    /// <summary>
    /// Sets if the actor can be activated on touch.
    /// </summary>
    /// <param name="newOnTouch">The new setting for if the actor can be activated on touch.</param>
    public void SetOnTouch(bool newOnTouch) {
        onTouch = newOnTouch;
    }

    /// <summary>
    /// Sets if the actor will activate on find.
    /// </summary>
    /// <param name="newRandomMovement">The new setting for if the actor can activate when player walks into found range.</param>
    public void SetOnFind(bool newOnFind) {
        onFind = newOnFind;
    }

    /// <summary>
    /// Sets if the actor can be activated automatically.
    /// </summary>
    /// <param name="newAuto">The new setting for if the actor can be activated automatically.</param>
    public void SetAuto(bool newAuto) {
        auto = newAuto;
    }

    /// <summary>
    /// Sets if the actor can be activated on interaction.
    /// </summary>
    /// <param name="newOnAction">The new setting for if the actor can be activated when the party interacts with it.</param>
    public void SetOnAction(bool newOnAction) {
        onAction = newOnAction;
    }

    /// <summary>
    /// Getter for the field sprite ID of the actor.
    /// </summary>
    /// <returns>The field sprite ID of the actor.</returns>
    public int GetFieldSpriteId() {
        return actorSprite.GetSpriteId();
    }

    /// <summary>
    /// Getter for the width of the actor.
    /// </summary>
    /// <returns>The field sprite width of the actor.</returns>
    public int GetWidth() {
        return actorSprite.GetWidth();
    }

    /// <summary>
    /// Getter for the height of the actor.
    /// </summary>
    /// <returns>The field sprite height of the actor.</returns>
    public int GetHeight() {
        return actorSprite.GetHeight();
    }

    /// <summary>
    /// Getter for the actor's movement speed.
    /// </summary>
    /// <returns>The movement speed of the actor.</returns>
    public int GetMovementSpeed() {
        return movementSpeed;
    }

    /// <summary>
    /// Getter for the tracking range of the actor.
    /// </summary>
    /// <returns>The tracking range of the actor.</returns>
    public int GetTrackingRange() {
        return trackingRange;
    }

    /// <summary>
    /// Getter for the actor's behaviour.
    /// </summary>
    /// <returns>The behaviour of the actor.</returns>
    public int GetBehaviour() {
        return behaviour;
    }

    /// <summary>
    /// Getter for the actor's path.
    /// </summary>
    /// <returns>The path of the actor.</returns>
    public List<int> GetPath() {
        return path;
    }

    /// <summary>
    /// Getter for the actor's linked script.
    /// </summary>
    /// <returns>The script ID linked to the actor.</returns>
    public int GetScriptId() {
        return scriptId;
    }

    /// <summary>
    /// Returns if the actor's field sprite appears below the party.
    /// </summary>
    /// <returns>True: The field sprite will be drawn on the map scene before the party.
    /// False: The field sprite is drawn after the party, will appear above.</returns>
    public bool IsBelowParty() {
        return belowParty;
    }

    /// <summary>
    /// Returns if the actor is passable.
    /// </summary>
    /// <returns>True: The actor can be walked over.
    /// False: The actor is not passable.</returns>
    public bool IsPassable() {
        return passable;
    }

    /// <summary>
    /// Returns if the actor can be activated on touch.
    /// </summary>
    /// <returns>True: The actor activates when the party makes contact with it.
    /// False: The actor does not react when the party touches it.</returns>
    public bool ActivatesOnTouch() {
        return onTouch;
    }

    /// <summary>
    /// Returns if the actor can be activated automatically.
    /// </summary>
    /// <returns>True: The actor activates as soon as the map loads.
    /// False: The actor does not activate automatically.</returns>
    public bool ActivatesAutomatically() {
        return auto;
    }

    /// <summary>
    /// Returns if the actor can be activated on action.
    /// </summary>
    /// <returns>True: The actor activates when the party interacts with the actor.
    /// False: The actor does not activate when the party interacts with the actor.</returns>
    public bool ActivatesOnAction() {
        return onAction;
    }
    
    /// <summary>
    /// Returns if the actor activates when the entity finds the player.
    /// </summary>
    /// <returns>True: The actor activates when it finds the player.
    /// False: The actor does not activate when it finds the player.</returns>
    public bool ActivatesOnFind() {
        return onFind;
    }

    // Actor Variables.
    private readonly ActorSprite actorSprite;
    private readonly int behaviour, scriptId;
    private readonly List<int> path;
    private int movementSpeed, trackingRange;
    private bool belowParty, passable, onTouch, auto, onAction, onFind;
}