using Data.Runtime.Actors.Controllers.Base;
using Data.Runtime.Spatials;

namespace Data.Runtime.Actors.Controllers.Core;

public sealed class PathedController : Controller 
{
    private readonly bool _canFinish;
    private readonly IReadOnlyList<int> _actorPath;

    private int _currentPathStepIndex = 0;
    private bool _finished = false;

    public PathedController(bool canFinish, IReadOnlyList<int> actorPath) 
    {
        _canFinish = canFinish;
        _actorPath = actorPath;
    }

    public override bool IsFinished() => _finished;

    /// <summary>
    /// When pathed controllers execute a move increment their current path step index.
    /// </summary>
    public override void ExecuteMove() 
    {
        // If the pathed controller can finish set it to finish, otherwise reset the index back to the first step.
        if (_currentPathStepIndex >= _actorPath.Count - 1) 
        {
            _finished = _canFinish;
            _currentPathStepIndex = 0;
        } 
        else 
        {
            _currentPathStepIndex ++;
        }
        base.ExecuteMove();
    }

    public override int GetMove(IPositionProvider actorLocation) 
    {
        var nextStep = _actorPath[_currentPathStepIndex];
        return nextStep;
    }
}