using Data.Definitions.Maps.Base;
using Data.Runtime.Actors.Controllers.Base;
using Data.Runtime.Spatials;

namespace Data.Runtime.Actors.Controllers.Core;

/// <summary>
/// Controller that moves an actor directly toward the party using a naive approach, ignoring obstacles and 
/// pathfinding constraints.
/// </summary>
public sealed class DumbTrackingController : TrackedController 
{
    public DumbTrackingController(IPositionProvider targetLocation) : base(targetLocation) {}

    public override int GetMove(IPositionProvider actorLocation) 
    {
        var direction = -1;

        // Load the actor's and target's X and Y coordinate.
        var actorXLocation = actorLocation.XLocation;
        var actorYLocation = actorLocation.YLocation;
        var targetXLocation = _targetLocation.XLocation;
        var targetYLocation = _targetLocation.YLocation;

        // Generate move.
        var xDiff = Math.Abs(actorXLocation - targetXLocation);
        var yDiff = Math.Abs(actorYLocation - targetYLocation);
        // If x distance is greater then y distance.
        if (xDiff > yDiff) 
        {
            direction = actorXLocation > targetXLocation ? (int) Direction.West : (int) Direction.East;
        }
        // If y distance is greater then x distance.
        else if (yDiff > xDiff) 
        {
            direction = actorYLocation > targetYLocation ? (int) Direction.North : (int) Direction.South;
        }
        // If y and x distance is same but there is a possible movement.
        else if (xDiff == yDiff && xDiff > 0) 
        {
            direction = actorXLocation > targetXLocation ? 3 : 1;
        }

        return direction;
    }
}