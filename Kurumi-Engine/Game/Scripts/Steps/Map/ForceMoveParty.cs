using Game.Scripts.Base;
using Game.Scripts.Context.Capabilities.Interfaces.Map;
using Game.Scripts.Context.Core;

using Utils.Finishable;

namespace Game.Scripts.Steps.Map;

public sealed class ForceMoveParty : ScriptStep 
{
    private readonly bool _keepDirection, _instant;
    private readonly List<int> _path;

    private IFinishable? _finishableController;

    public ForceMoveParty(bool keepDirection, bool instant, List<int> path) : base() 
    {
        _keepDirection = keepDirection;
        _instant = instant;
        _path = path;
    }

    public override void Activate(ScriptContext scriptContext) 
    {
        IMovementActions mapNavigationActions = scriptContext.GetCapability<IMovementActions>();
        _finishableController = mapNavigationActions.ForceMoveParty(_keepDirection, _instant, _path);
    }

    public override bool Waiting() => !_finishableController!.IsFinished();
}