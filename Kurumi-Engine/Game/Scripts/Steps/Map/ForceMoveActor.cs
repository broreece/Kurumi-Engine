using Game.Scripts.Base;
using Game.Scripts.Context.Capabilities.Interfaces.Map;
using Game.Scripts.Context.Core;

using Utils.Finishable;

namespace Game.Scripts.Steps.Map;

public sealed class ForceMoveActor : ScriptStep 
{
    private readonly bool _keepDirection, _lockMovement, _instant;
    private readonly string _actorKey;
    private readonly List<int> _path;

    private IFinishable? _finishableController;

    public ForceMoveActor(
        bool keepDirection, 
        bool lockMovement, 
        bool instant, 
        string actorKey, 
        List<int> path) : base() 
    {
        _keepDirection = keepDirection;
        _lockMovement = lockMovement;
        _instant = instant;
        _actorKey = actorKey;
        _path = path;
    }
    
    public override void Activate(ScriptContext scriptContext) 
    {
        IMovementActions mapNavigationActions = scriptContext.GetCapability<IMovementActions>();
        _finishableController = mapNavigationActions.ForceMoveActor(
            _keepDirection, 
            _lockMovement, 
            _instant, 
            _actorKey, 
            _path);
    }

    public override bool Waiting() => _finishableController != null && !_finishableController.IsFinished();
}