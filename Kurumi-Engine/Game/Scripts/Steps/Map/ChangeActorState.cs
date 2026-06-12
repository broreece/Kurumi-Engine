// Game.
using Game.Scripts.Base;
using Game.Scripts.Context.Capabilities.Interfaces.Map;
using Game.Scripts.Context.Core;

namespace Game.Scripts.Steps.Map;

public sealed class ChangeActorState : ScriptStep 
{
    private readonly string _actorKey;
    private readonly int _actorState;

    public ChangeActorState(string actorKey, int actorState) : base() 
    {
        _actorKey = actorKey;
        _actorState = actorState;
    }

    public override void Activate(ScriptContext scriptContext) 
    {
        IMovementActions movementActions = scriptContext.GetCapability<IMovementActions>();
        movementActions.ChangeActorState(_actorKey, _actorState);
    }
}