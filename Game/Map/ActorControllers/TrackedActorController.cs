namespace Game.Map.ActorControllers;

using Game.Map.Actors.Base;
using Game.Map.Elements;
using Game.Map.Pathfinding;

/// <summary>
/// The tracked actor controller, used for map actors that track a certain location.
/// </summary>
public sealed class TrackedActorController : ActorController {
    /// <summary>
    /// Constructor for the tracked actor controller. A actor controller that takes an additional position provider object.
    /// </summary>
    /// <param name="interval">The interval at which the actor moves.</param>
    /// <param name="xLocation">The stored x location of the actor.</param>
    /// <param name="yLocation">The stored y location of the actor.</param>
    /// <param name="behaviour">The behaviour of the actor.</param>
    /// <param name="positionProvider">The position of which the tracked actor tracks.</param>
    /// <param name="navigationGrid">The navigation grid movement is based on.</param>
    public TrackedActorController(int interval, int xLocation, int yLocation, Behaviour behaviour, 
        PositionProvider positionProvider, INavigationGrid navigationGrid) : base(interval, xLocation, yLocation) {
            this.behaviour = behaviour;
            this.positionProvider = positionProvider;
            this.navigationGrid = navigationGrid;
    }

    /// <summary>
    /// Overriden get move function for actors that track a target.
    /// </summary>
    /// <returns>The optimal direction to move in.</returns>
    public override int GetMove() {
        // Load the actor's x and y coordinate.
        int direction = -1;

        // Check behaviour and find the move to make.
        switch (behaviour) {
            case Behaviour.DumbTracking:
                // Generate move.
                int xDiff = Math.Abs(xLocation - positionProvider.GetXLocation());
                int yDiff = Math.Abs(yLocation - positionProvider.GetYLocation());
                // If x distance is greater then y distance.
                if (xDiff > yDiff) {
                    direction = xLocation > positionProvider.GetXLocation() ? 3 : 1;
                }
                // If y distance is greater then x distance.
                else if (yDiff > xDiff) {
                    direction = yLocation > positionProvider.GetYLocation() ? 0 : 2;
                }
                // If y and x distance is same but there is a possible movement.
                else if (xDiff == yDiff && xDiff > 0) {
                    direction = xLocation > positionProvider.GetXLocation() ? 3 : 1;
                }
                break;

            default:
                direction = AStarSearch.LoadFastestPath(xLocation, yLocation, positionProvider.GetXLocation(), positionProvider.GetYLocation(),
                    navigationGrid);
                break;
        }

        duration = 0;
        return direction;
    }

    private readonly Behaviour behaviour;
    private readonly PositionProvider positionProvider;
    private readonly INavigationGrid navigationGrid;
}