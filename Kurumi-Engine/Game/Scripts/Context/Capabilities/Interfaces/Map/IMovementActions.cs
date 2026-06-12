// Game.
using Game.Scripts.Context.Capabilities.Base;

// Utility.
using Utils.Finishable;

namespace Game.Scripts.Context.Capabilities.Interfaces.Map;

public interface IMovementActions : ICapability 
{
    public IFinishable ForceMoveActor(
        bool keepDirection, 
        bool lockMovement, 
        bool instant, 
        string actorKey, 
        List<int> path
    );

    public IFinishable ForceMoveParty(bool keepDirection, bool instant, IReadOnlyList<int> path);

    public void ChangeActorState(string actorKey, int state);

    public void ChangeActorPassability(string actorKey, bool passability);
}