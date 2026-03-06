namespace States.Map.Core;

using Config.Runtime.Map;
using Engine.Runtime;
using Game.Map.ActorControllers;
using Game.Map.Actors.Base;
using Game.Map.Core;
using Game.Map.Elements;
using Scripts.MapScripts.Base;
using States.Base;
using States.Map.Exceptions;
using States.Map.Input;
using States.Map.Interfaces;

/// <summary>
/// The map state class. Contains the dynamic duties of changing a map during the map scene.
/// </summary>
public sealed class MapState : StateBase, IMapInputController {
    /// <summary>
    /// The map state constructor class.
    /// </summary>
    /// <param name="gameContext">The game context required to pass to scripts.</param>
    /// <param name="mapSceneView">The map scene view object.</param>
    /// <param name="mapConfig">The map config object.</param>
    /// <param name="map">The map object.</param>
    public MapState(GameContext gameContext, IMapSceneView mapSceneView, MapConfig mapConfig, Map map) : base(gameContext) {
        // Load map state view and map.
        this.mapSceneView = mapSceneView;
        this.map = map;

        // Load stored values from config.
        mapMaxTilesWide = mapConfig.GetMaxTilesWide();
        mapMaxTilesHigh = mapConfig.GetMaxTilesHigh();

        // Variables used during forced movement.
        forcedMovePath = [];
        forcedMoveIndex = 0;
        keepsDirection = false;
        isForcedMoving = false;
        isActorForcedMoving = false;

        // Acctivate all auto actors.
        foreach (IActorHandler autoActor in map.GetAutoActors()) {
            ActivateScript(new MapScript(autoActor.GetScript()));
        }
    }

    /// <summary>
    /// The update overriden void from the state base class. Updates all map elements that can be updated.
    /// </summary>
    public override void Update() {
        // Update the forced moving clock if forced moving is in progress.
        if (isForcedMoving) {
            forcedMoveElapsedTime += mapSceneView.TickForcedMovementClock();
            if (forcedMoveElapsedTime >= forcedMoveDuration) {
                // In the case of the party we check the movement speed for each move.
                if (!isActorForcedMoving) {
                    Direction move = (Direction) forcedMovePath[forcedMoveIndex];
                    MoveParty(move, keepsDirection);
                    forcedMoveIndex ++;
                    if (forcedMoveIndex == forcedMovePath.Count) {
                        isForcedMoving = false;
                        gameContext.ResumeInput();
                    }
                }
                // In case of actor we just finish the process when the elapsed time reaches the limit.
                else {
                    isForcedMoving = false;
                    gameContext.ResumeInput();
                }
            }
        }

        // Check actors and if they should move.
        int index = 0;
        foreach (IActorHandler currentHandler in map.GetListActors()) {
            int timePassed = mapSceneView.TickActorControllerClock(index);
            currentHandler.Update(timePassed);
            if (currentHandler.CanMove()) {
                int move = currentHandler.GetMove();
                if (move != -1) {
                    // TODO: Maybe we can create/use a custom data type for both ints.
                    int[] newLocation = MoveActor(GetActorAtIndex(currentHandler.GetXLocation(), currentHandler.GetYLocation()), move, 
                        keepDirection: false);
                    if (currentHandler.GetBehaviour() == Behaviour.RandomMovement) {
                        currentHandler.SetInterval(mapSceneView.GenerateRandomInterval(currentHandler.GetInterval()));
                        currentHandler.ExecuteMove(newLocation[0], newLocation[1]);
                    }
                }
            }
            index ++;
        }
    }

    /// <summary>
    /// Performs an action checking if there is an actor in front of the party.
    /// </summary>
    public void Select() {
        // Store parties location.
        int partyXLocation = party.GetXLocation();
        int partyYLocation = party.GetYLocation();
        int facing = party.GetDirection();
        // This is hard coded for the map element facing enum, can be changed.
        int xDirection = facing % 2 == 0 ? 0 : (facing % 4 == 1 ? 1 : -1);
        int yDirection = facing % 2 == 1 ? 0 : (facing == 0 ? -1 : 1);

        // Check each scene actor in the map scene and check if the position matches with the parties facing place.
        foreach (IActorHandler currentActor in map.GetListActors()) {
            int xLocation = currentActor.GetXLocation();
            int yLocation = currentActor.GetYLocation();
            if (xLocation == partyXLocation + xDirection && yLocation == partyYLocation + yDirection) {
                if (currentActor.GetBehaviour() != Behaviour.StationaryDoesNotTurn) {
                    // Set the new facing direction of the actor using a custom formula to flip parties facing direction.
                    currentActor.SetDirection((Direction) ((facing + 2) % 4));
                }
                if (currentActor.ActivatesOnAction()) {
                    ActivateScript(new MapScript(currentActor.GetScript()));
                }
            }
        }
    }

    /// <summary>
    /// Function that opens the game menu.
    /// </summary>
    public void Escape() {
        gameContext.OpenMainMenu();
    }

    /// <summary>
    /// Function that attempts to move right.
    /// </summary>
    public void MoveRight() {
        MoveParty(Direction.East, keepDirection: false);
    }

    /// <summary>
    /// Function that attempts to move left.
    /// </summary>
    public void MoveLeft() {
        MoveParty(Direction.West, keepDirection: false);
    }

    /// <summary>
    /// Function that attempts to move down.
    /// </summary>
    public void MoveDown() {
        MoveParty(Direction.South, keepDirection: false);
    }

    /// <summary>
    /// Function that attempts to move up.
    /// </summary>
    public void MoveUp() {
        MoveParty(Direction.North, keepDirection: false);
    }

    /// <summary>
    /// Function used to handle party movement.
    /// </summary>
    /// <param name="direction">The direction the party is moving.</param>
    /// <param name="keepDirection">If the parties direction changes when moving.</param>
    public void MoveParty(Direction direction, bool keepDirection) {
        int xLocation = party.GetXLocation();
        int yLocation = party.GetYLocation();
        int rightMovement = (int) direction % 2 == 0 ? 0 : ((int) direction % 4 == 1 ? 1 : -1);
        int downMovement = (int) direction % 2 == 1 ? 0 : ((int) direction == 0 ? -1 : 1);
        // Check that the new location is not off the map, is on a passable tile and is not occupied by impassable actor.
        if (TileIsPassable(xLocation + rightMovement, yLocation + downMovement) &&
            ActorIsPassable(xLocation + rightMovement, yLocation + downMovement)) {
            party.SetOldXLocation(xLocation);
            party.SetOldYLocation(yLocation);
            party.SetXLocation(xLocation + rightMovement);
            party.SetYLocation(yLocation + downMovement);
            // Set animation to start playing.
            party.SetCurrentAnimationFrame(1);
            // Activates any touch actors on the location.
            IActorHandler? potentialActor = map.GetActorAt(xLocation + rightMovement, yLocation + downMovement);
            if (potentialActor != null && potentialActor.ActivatesOnTouch()) {
                ActivateScript(new MapScript(potentialActor.GetScript()));
            }
        }
        // Update direction if not keeping direction.
        if (!keepDirection) {
            party.SetDirection(direction);
        }
        mapSceneView.OnPartyMoved(xLocation + rightMovement, yLocation + downMovement);

        // Activates any on found actors.
        ActivateInRangeActors();
    }

    /// <summary>
    /// Function used to start the force move party condition, forces a movement while potentially maintaining direction. 
    /// </summary>
    /// <param name="keepsDirection">If the parties direction is maintained.</param>
    /// <param name="path">The path the party follows.</param>
    /// <param name="characterMovementSpeed">The speed that the party walks at.</param>
    public void StartForceMoveParty(bool keepsDirection, List<int> path, int characterMovementSpeed) {
        // Start the forced movement function.
        StartForcedMovement(false, keepsDirection, characterMovementSpeed);

        // Parties forced movement should store a path whereas actors update the controler. 
        forcedMovePath = path;
        forcedMoveIndex = 0;
    }

    /// <summary>
    /// Function used to move actors on the map.
    /// </summary>
    /// <param name="actorIndex">The index of the actor (Uses the list of all actors).</param>
    /// <param name="direction">The direction that the actor will move in.</param>
    /// <param name="keepDirection">If the actor's direction is maintained.</param>
    public int[] MoveActor(int actorIndex, int direction, bool keepDirection) {
        List<IActorHandler> actorsList = map.GetListActors();
        actorsList[actorIndex].SetDirection((Direction) direction);
        int xLocation = actorsList[actorIndex].GetXLocation();
        int yLocation = actorsList[actorIndex].GetYLocation();
        int rightMovement = (int) direction % 2 == 0 ? 0 : ((int) direction % 4 == 1 ? 1 : -1);
        int downMovement = (int) direction % 2 == 1 ? 0 : ((int) direction == 0 ? -1 : 1);
        if (TileIsPassable(xLocation + rightMovement, yLocation + downMovement)) {
            if (ActorDoesNotExist(xLocation + rightMovement, yLocation + downMovement)
                && !(xLocation + rightMovement == party.GetXLocation()
                && yLocation + downMovement == party.GetYLocation())) {
                actorsList[actorIndex].SetOldXLocation(xLocation);
                actorsList[actorIndex].SetOldYLocation(yLocation);
                actorsList[actorIndex].SetXLocation(xLocation + rightMovement);
                actorsList[actorIndex].SetYLocation(yLocation + downMovement);
                // Set animation to start playing.
                actorsList[actorIndex].SetCurrentAnimationFrame(1);
                mapSceneView.ResetActorWalkAnimationClock(actorIndex);
                map.SetActorAt(xLocation, yLocation, null);
                map.SetActorAt(xLocation + rightMovement, yLocation + downMovement, actorsList[actorIndex]);
            }
            else if (xLocation + rightMovement == party.GetXLocation()
                && yLocation + downMovement == party.GetYLocation()
                && actorsList[actorIndex].ActivatesOnTouch()) {
                ActivateScript(new MapScript(actorsList[actorIndex].GetScript()));
            }
        }
        if (actorsList[actorIndex].ActivatesOnFind() && InRangeActor(actorsList[actorIndex])) {
           ActivateScript(new MapScript(actorsList[actorIndex].GetScript()));
        }
        return [xLocation + rightMovement, yLocation + downMovement];
    }

    /// <summary>
    /// Function that checks all in range actors and activates any that are in range.
    /// </summary>
    public void ActivateInRangeActors() {
        foreach (IActorHandler currentActor in map.GetOnFoundActors()) {
            int actorX = currentActor.GetXLocation();
            int actorY = currentActor.GetYLocation();
            int halfWidth = (int) Math.Floor(mapMaxTilesWide / (decimal) 2.0);
            int halfHeight = (int) Math.Floor(mapMaxTilesHigh / (decimal) 2.0);
            int tilesRight = party.GetXLocation() - halfWidth;
            int tilesDown = party.GetYLocation() - halfHeight;

            // Check first if either x or y will be in range.
            bool insideX = map.GetWidth() <= mapMaxTilesWide;
            bool insideY = map.GetHeight() <= mapMaxTilesHigh;
            // Check again if it's already confirmed or if it is in range.
            insideX = insideX || (actorX >= tilesRight - 1 && actorX <= tilesRight + mapMaxTilesWide);
            insideY = insideY || (actorY >= tilesDown - 1 && actorY <= tilesDown + mapMaxTilesHigh);
            if (insideX && insideY && InRangeActor(currentActor)) {
                ActivateScript(new MapScript(currentActor.GetScript()));
            }
        }
    }

    /// <summary>
    /// Function used by the force move Actor step, forces a movement while potentially maintaining direction. 
    /// </summary>
    /// <param name="keepsDirection">If the actor's direction is maintained.</param>
    /// <param name="lockMovement">If the parties movement is locked during the actor's movement.</param>
    /// <param name="actorX">The actor's X location.</param>
    /// <param name="actorY">The actor's Y location.</param>
    /// <param name="path">The path the actor follows.</param>
    /// <exception cref="ActorMissingException">Error thrown if a actor is missing.</exception>
    public void ForceMoveActor(bool keepsDirection, bool lockMovement, bool instant, int actorX, int actorY,
        List<int> path) {
        // Load chosen actor or throw exception.
        IActorHandler? currentActor = map.GetActorAt(actorX, actorY) ??
            throw new ActorMissingException("Error found on map: " + map.GetMapName()
            + " At X: " + actorX + " And Y: " + actorY);

        // If movement needs to be locked activate freeze timer in scene and freeze controls.
        if (lockMovement && !instant) {
            StartForcedMovement(true, keepsDirection, currentActor.GetMovementSpeed() * path.Count);
        }

        // If instant, move the actor in the path specified.
        if (instant) {
            foreach (int direction in path) {
                int xLocation = currentActor.GetXLocation();
                int yLocation = currentActor.GetYLocation();
                int rightMovement = direction % 2 == 0 ? 0 : (direction % 4 == 1 ? 1 : -1);
                int downMovement = direction % 2 == 1 ? 0 : (direction == 0 ? -1 : 1);
                // Set location.
                currentActor.SetOldXLocation(xLocation);
                currentActor.SetOldYLocation(yLocation);
                currentActor.SetXLocation(xLocation + rightMovement);
                currentActor.SetYLocation(yLocation + downMovement);
                map.SetActorAt(xLocation, yLocation, null);
                map.SetActorAt(xLocation + rightMovement, yLocation + downMovement, currentActor);
                if (!keepsDirection) {
                    currentActor.SetDirection((Direction) direction);
                }
            }
        }
        else {
            // If not instant update actor's controller to be pathed.
            currentActor.PushController(new PathedActorController(currentActor.GetMovementSpeed(), currentActor.GetXLocation(),
                currentActor.GetYLocation(), true, path));
        }
    }

    /// <summary>
    /// Function that gets the index of an actor at a given location.
    /// </summary>
    /// <param name="xLocation">The x location being checked.</param>
    /// <param name="yLocation">The y location being checked.</param>
    /// <exception cref="ActorMissingException">Error thrown if no actor is found at the index specified.</exception>
    public int GetActorAtIndex(int xLocation, int yLocation) {
        int index = 0;
        foreach (IActorHandler actor in map.GetListActors()) {
            if (actor.GetXLocation() == xLocation && actor.GetYLocation() == yLocation) {
                return index;
            }
            index ++;
        }
        throw new ActorMissingException();
    }

    private void StartForcedMovement(bool isActor, bool keepsDirection, int duration) {
        // Freeze input and start timer.
        gameContext.FreezeInput();
        mapSceneView.ResetForcedMovementClock();
        isForcedMoving = true;

        // Parameter variables.
        this.keepsDirection = keepsDirection;
        isActorForcedMoving = isActor;
        forcedMoveDuration = duration;
    }

    /// <summary>
    /// Helper function used to reduce duplicated code when activating an script.
    /// </summary>
    /// <param name="scriptToActivate">The script that will be activated.</param>
    private void ActivateScript(MapScript scriptToActivate) {
        scriptToActivate.Activate(gameContext);
    }

    /// <summary>
    /// Function used to check if a tile is passable and in the ranges of the map.
    /// </summary>
    /// <param name="xLocation">The x location being checked.</param>
    /// <param name="yLocation">The y location being checked.</param>
    /// <returns>True: The tile is in the range of the map and is passable, False otherwise.</returns>
    private bool TileIsPassable(int xLocation, int yLocation) {
        return xLocation < map.GetWidth() && yLocation < map.GetHeight() &&
                xLocation >= 0 && yLocation >= 0 && map.GetTile(xLocation, yLocation).IsPassable();
    }

    /// <summary>
    /// Function used to check if an actor does not exist at a coordinate.
    /// </summary>
    /// <param name="xLocation">The x location being checked.</param>
    /// <param name="yLocation">The y location being checked.</param>
    /// <returns>True: An actor does not exist at a coordinate, False otherwise.</returns>
    private bool ActorDoesNotExist(int xLocation, int yLocation) {
        return map.GetActorAt(xLocation, yLocation) == null;
    }

    /// <summary>
    /// Helper function used to check if an actor is passable.
    /// </summary>
    /// <param name="xLocation">The x location being checked.</param>
    /// <param name="yLocation">The y location being checked.</param>
    /// <returns>True: The actor is passable, False otherwise.</returns>
    private bool ActorIsPassable(int xLocation, int yLocation) {
        IActorHandler? potentialActor = map.GetActorAt(xLocation, yLocation);
        if (potentialActor != null) {
            return potentialActor.IsPassable();
        }
        return true;
    }

    /// <summary>
    /// Checks if the party is in range of a specified actor.
    /// </summary>
    /// <param name="actor">The actor being checked.</param>
    /// <returns>If the party is in range of the specified actor.</returns>
    private bool InRangeActor(IActorHandler actor) {
        // Load inital variables.
        int partyX = party.GetXLocation();
        int partyY = party.GetYLocation();
        int actorX = actor.GetXLocation();
        int actorY = actor.GetYLocation();
        bool inRange = false;

        // Check the direction the actor is facing, then check based on direction if it's in range.
        switch ((Direction) actor.GetDirection()) {
            case Direction.North:
                // We can change the width or height in front of the actor here.
                // Check X is within 3 tiles in front of actor.
                if (partyX >= actorX - 1 && partyX <= actorX + 1) {
                    // Check Y is within the view range of the actor.
                    if (partyY >= actorY - actor.GetTrackingRange() && partyY < actorY) {
                        inRange = true;
                    }
                }
                break;

            case Direction.East:
                // Check Y is within 3 tiles in front of actor.
                if (partyY >= actorY - 1 && partyY <= actorY + 1) {
                    // Check X is within the view range of the actor.
                    if (partyX <= actorX + actor.GetTrackingRange() && partyX > actorX) {
                        inRange = true;
                    }
                }
                break;

            case Direction.South:
                // Check X is within 3 tiles in front of actor.
                if (partyX >= actorX - 1 && partyX <= actorX + 1) {
                    // Check Y is within the view range of the actor.
                    if (partyY <= actorY + actor.GetTrackingRange() && partyY > actorY) {
                        inRange = true;
                    }
                }
                break;

            case Direction.West:
                // Check Y is within 3 tiles in front of actor.
                if (partyY >= actorY - 1 && partyY <= actorY + 1) {
                    // Check X is within the view range of the actor.
                    if (partyX >= actorX - actor.GetTrackingRange() && partyX < actorX) {
                        inRange = true;
                    }
                }
                break;

            default:
                break;
        }

        return inRange;
    }

    // Associated restricted map scene view object.
    private readonly IMapSceneView mapSceneView;

    // Current map loaded in the state.
    private readonly Map map;

    // Map config.
    private readonly int mapMaxTilesWide, mapMaxTilesHigh;

    // Variables used during forced party / actor movement.
    private List<int> forcedMovePath;
    private int forcedMoveIndex, forcedMoveDuration, forcedMoveElapsedTime;
    private bool keepsDirection, isForcedMoving, isActorForcedMoving;
}
