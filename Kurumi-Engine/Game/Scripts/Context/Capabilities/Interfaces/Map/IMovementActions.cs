using Game.Scripts.Context.Capabilities.Base;

using Utils.Interfaces;

namespace Game.Scripts.Context.Capabilities.Interfaces.Map;

public interface IMovementActions : ICapability 
{
    public IFinishable ForceMoveActor(
        bool keepDirection, 
        bool lockMovement, 
        bool instant, 
        int actorIndex, 
        List<int> path);

    public void ForceMoveParty(bool keepDirection, bool instant, List<int> path);
}