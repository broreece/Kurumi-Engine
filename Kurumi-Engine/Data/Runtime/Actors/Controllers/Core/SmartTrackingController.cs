using Data.Runtime.Actors.Controllers.Base;
using Data.Runtime.Spatials;
using Engine.Systems.Navigation.Core;
using Engine.Systems.Pathfinding;

namespace Data.Runtime.Actors.Controllers.Core;

public sealed class SmartTrackingController : TrackedController 
{
    private readonly NavigationGrid _navigationGrid;

    private readonly int _maxRange;

    public SmartTrackingController(IPositionProvider targetLocation, NavigationGrid navigationGrid, int maxRange) : 
        base(targetLocation) 
    {
        _navigationGrid = navigationGrid;
        _maxRange = maxRange;
    }

    public override int GetMove(IPositionProvider actorLocation) 
    {
        return new AStarSearch().LoadFastestPath(
            actorLocation.XLocation, 
            actorLocation.YLocation, 
            _targetLocation.XLocation, 
            _targetLocation.YLocation, 
            _navigationGrid,
            _maxRange
        );
    }
}