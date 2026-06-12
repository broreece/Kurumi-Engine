// Game.
using Game.Scripts.Base;
using Game.Scripts.Context.Capabilities.Interfaces.Map;
using Game.Scripts.Context.Core;

namespace Game.Scripts.Steps.Map;

public sealed class CheckActorState : ConditionalScriptStep 
{
    private readonly string _actorKey;
    private readonly int _actorState;

    private bool _conditionMet;

    public CheckActorState(string actorKey, int actorState, string? nextIfFalse) : base() 
    {
        _actorKey = actorKey;
        _actorState = actorState;
        NextIfFalse = nextIfFalse;
    }

    public override void Activate(ScriptContext scriptContext) 
    {
        IMovementActions movementActions = scriptContext.GetCapability<IMovementActions>();
        _conditionMet = movementActions.ActorIsInState(_actorKey, _actorState);
    }

    protected override bool IsConditionMet() => _conditionMet;
}