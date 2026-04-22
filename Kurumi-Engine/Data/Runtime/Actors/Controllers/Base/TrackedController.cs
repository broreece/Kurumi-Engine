using Data.Runtime.Spatials;

namespace Data.Runtime.Actors.Controllers.Base;

/// <summary>
/// Represents an actor with a target location. The movement strategy used to reach that location is defined by 
/// derived controller implementations.
/// </summary>
public abstract class TrackedController : Controller 
{
    protected readonly IPositionProvider _targetLocation;

    protected TrackedController(IPositionProvider targetLocation) 
    {
        _targetLocation = targetLocation;
    }

    public override bool IsTrackedController() => true;
}