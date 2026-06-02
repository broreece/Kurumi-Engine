// Data.
using Data.Runtime.Spatials;

// Utility.
using Utils.Finishable;

namespace Data.Runtime.Maps.Base.Controllers.Base;

/// <summary>
/// Base class for actor controllers, responsible for handling movement timing and execution.
/// Derived controllers implement specific movement behaviors.
/// </summary>
public abstract class Controller : IFinishable
{
    private float _elapsedTime = 0;

    public required int Interval { get; set; }

    public bool CanMove => _elapsedTime >= Interval;

    // Overriden by tracked controllers which return true.
    public virtual bool IsTrackedController => false;

    // Overriden by pathed controllers which can allow finishing of movements.
    public virtual bool IsFinished() => false;

    public void Update(float deltaTime) =>_elapsedTime += deltaTime;

    // Overriden by pathed controllers which also increment a movement index when moving.
    public virtual void ExecuteMove() =>_elapsedTime = 0;

    // Overriden by derived controllers for custom logic. Returns -1 if no move found.
    public virtual int GetMove(IPositionProvider actorLocation) => -1;
}