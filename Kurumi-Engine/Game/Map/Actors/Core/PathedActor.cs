namespace Game.Map.Actors.Core;

using Game.Map.ActorControllers;
using Registry.Actors;

/// <summary>
/// Pathed actors are an object that holds an actor and a path.
/// </summary>
public sealed class PathedActor : Actor {
    /// <summary>
    /// Constructor for the pathed actor class.
    /// </summary>
    /// <param name="xLocation">The x coordinate of the actor.</param>
    /// <param name="yLocation">The y coordinate of the actor.</param>
    /// <param name="actorInfoId">The actor sprite ID of the actor.</param>
    /// <param name="direction">The direction of the actor.</param>
    /// <param name="visible">If the actor is visible.</param>
    /// <param name="actorInfoRegistry">The actor info registry object.</param>
    /// <param name="path">The pathed actors path.</param>
    public PathedActor(int xLocation, int yLocation, int actorInfoId, int direction, bool visible, ActorInfoRegistry actorInfoRegistry,
        List<int> path) : base(xLocation, yLocation, actorInfoId, direction, visible, actorInfoRegistry) {
        // Add base controller.
        PushController(new PathedActorController(GetMovementSpeed(), xLocation, yLocation, forcedMovement: false, path));
    }
}