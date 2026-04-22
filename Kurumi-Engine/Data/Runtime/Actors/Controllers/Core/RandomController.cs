using Data.Runtime.Actors.Controllers.Base;
using Data.Runtime.Spatials;

namespace Data.Runtime.Actors.Controllers.Core;

/// <summary>
/// Controller that moves an actor randomly. It maintains a base movement interval, which is reset after each move.
/// The interval is then randomized externally and reassigned to control the timing of the next movement.
/// </summary>
public sealed class RandomController : Controller 
{
    private readonly int _fixedActorInterval;

    public RandomController() 
    {
        _fixedActorInterval = Interval;
    }

    public override void ExecuteMove() 
    {
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