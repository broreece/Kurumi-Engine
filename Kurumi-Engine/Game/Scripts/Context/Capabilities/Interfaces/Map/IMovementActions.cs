using Game.Scripts.Context.Capabilities.Base;

namespace Game.Scripts.Context.Capabilities.Interfaces.Map;

public interface IMovementActions : ICapability 
{
    public void ForceMoveActor(bool keepDirection, bool lockMovement, bool instant, int actorIndex, List<int> path);

    public void ForceMoveParty(bool keepDirection, bool instant, List<int> path);
}