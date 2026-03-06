namespace Game.Map.Actors.ActorTypes;

using Game.Map.ActorControllers;
using Game.Map.Actors.Base;
using Game.Map.Elements;
using Game.Map.Pathfinding;

/// <summary>
/// Grided actors are an object that holds an actor, map posistion target and a navigable grid.
/// </summary>
public sealed class GridedActor : ActorHandler {
    /// <summary>
    /// Constructor for the grided actor class, an actor handler that requires an navigable grid and a map posistion.
    /// </summary>
    /// <param name="actor">The existing actor object.</param>
    /// <param name="positionProvider">The position of which the tracked actor tracks.</param>
    /// <param name="navigationGrid">The navigation grid movement is based on.</param>
    public GridedActor(Actor actor, PositionProvider positionProvider, INavigationGrid navigationGrid) : base(actor) {
        actor.PushController(new TrackedActorController(actor.GetMovementSpeed(), actor.GetXLocation(), actor.GetYLocation(),
            actor.GetBehaviour(), positionProvider, navigationGrid));
    }
}