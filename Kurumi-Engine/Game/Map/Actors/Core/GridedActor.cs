namespace Game.Map.Actors.Core;

using Game.Map.ActorControllers;
using Game.Map.Actors.Base;
using Game.Map.Elements;
using Game.Map.Pathfinding;

/// <summary>
/// Grided actors are a type of actor that moves on a navigation grid, map posistion target and a navigable grid.
/// </summary>
public sealed class GridedActor : Actor {
    /// <summary>
    /// Constructor for the grided actor class, an actor that requires an navigable grid and a map posistion.
    /// </summary>
    /// <param name="xLocation">The x coordinate of the actor.</param>
    /// <param name="yLocation">The y coordinate of the actor.</param>
    /// <param name="actorInfo">The actor info of the actor.</param>
    /// <param name="direction">The direction of the actor.</param>
    /// <param name="visible">If the actor is visible.</param>
    /// <param name="positionProvider">The position of which the tracked actor tracks.</param>
    /// <param name="navigationGrid">The navigation grid movement is based on.</param>
    public GridedActor(int xLocation, int yLocation, ActorInfo actorInfo, int direction, bool visible,
        PositionProvider positionProvider, INavigationGrid navigationGrid) : 
        base(xLocation, yLocation, actorInfo, direction, visible) {
        PushController(new TrackedActorController(GetMovementSpeed(), xLocation, yLocation, GetBehaviour(), positionProvider, navigationGrid));
    }
}