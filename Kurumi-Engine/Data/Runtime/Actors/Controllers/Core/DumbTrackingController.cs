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
    private readonly int _maxRange;

    public DumbTrackingController(IPositionProvider targetLocation, int maxRange) : base(targetLocation)
    {
        _maxRange = maxRange;
    }

    public override int GetMove(IPositionProvider actorLocation) 
    {
        // Load the actor's and target's X and Y coordinate.
        var actorXLocation = actorLocation.XLocation;
        var actorYLocation = actorLocation.YLocation;
        var targetXLocation = _targetLocation.XLocation;
        var targetYLocation = _targetLocation.YLocation;

        // Check if out of range first to avoid unnecesary logic.
        var xDifference = actorXLocation > targetXLocation ? 
            actorXLocation - targetXLocation : targetXLocation - actorXLocation;
        var yDifference = actorYLocation > targetYLocation ? 
            actorYLocation - targetYLocation : targetYLocation - actorYLocation;
        if (xDifference + yDifference > _maxRange)
        {
            return -1;
        }

        // Generate move.
        var xDiff = Math.Abs(actorXLocation - targetXLocation);
        var yDiff = Math.Abs(actorYLocation - targetYLocation);
        // If x distance is greater then y distance.
        if (xDiff > yDiff) 
        {
            return actorXLocation > targetXLocation ? (int) Direction.West : (int) Direction.East;
        }
        // If y distance is greater then x distance.
        else if (yDiff > xDiff) 
        {
            return actorYLocation > targetYLocation ? (int) Direction.North : (int) Direction.South;
        }
        // If y and x distance is same but there is a possible movement.
        else if (xDiff == yDiff && xDiff > 0) 
        {
            return actorXLocation > targetXLocation ? 3 : 1;
        }

        // Actor and target share same location.
        return -1;
    }
}