// Game.
using Game.Scripts.Base;
using Game.Scripts.Context.Capabilities.Interfaces.Map;
using Game.Scripts.Context.Core;

namespace Game.Scripts.Steps.Map;

public sealed class ChangeActorPassability : ScriptStep 
{
    private readonly string _actorKey;
    private readonly bool _passability;

    public ChangeActorPassability(string actorKey, bool passability) : base() 
    {
        _actorKey = actorKey;
        _passability = passability;
    }

    public override void Activate(ScriptContext scriptContext) 
    {
        IMovementActions movementActions = scriptContext.GetCapability<IMovementActions>();
        movementActions.ChangeActorPassability(_actorKey, _passability);
    }
}