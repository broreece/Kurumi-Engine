// Data.
using Data.Runtime.Maps.Base.Controllers.Base;
using Data.Runtime.Spatials;

namespace Data.Runtime.Maps.Base.Controllers.Core;

/// <summary>
/// Controller that moves an actor randomly. It maintains a base movement interval, which is reset after each move.
/// The interval is then randomized externally and reassigned to control the timing of the next movement.
/// </summary>
public sealed class RandomController : Controller 
{
    private bool _initialized;
    
    private float _fixedActorInterval;

    public override void ExecuteMove() 
    {
        if (!_initialized)
        {
            _fixedActorInterval = Interval;
            _initialized = true;
        }

        Interval = _fixedActorInterval;
        
        base.ExecuteMove();
    }

    public override int GetMove(IPositionProvider actorLocation) 
    {
        var random = new Random();
        var randomMove = random.Next(0, 4);

        return randomMove;
    }
}