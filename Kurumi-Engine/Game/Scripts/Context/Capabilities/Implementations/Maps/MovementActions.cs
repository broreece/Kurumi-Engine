using Game.Scripts.Context.Capabilities.Interfaces.Map;
using Data.Runtime.Actors.Controllers.Core;
using Data.Runtime.Maps.Core;

namespace Game.Scripts.Context.Capabilities.Implementations.Maps;

public sealed class MovementActions : IMovementActions 
{
    private readonly Map _map;

    public MovementActions(Map map) 
    {
        _map = map;
    }

    public void ForceMoveActor(bool keepDirection, bool lockMovement, bool instant, int actorIndex, List<int> path) 
    {
        var actor = _map.Actors[actorIndex];
        actor.MaintainFacing = keepDirection;
        actor.AddController(new PathedController(canFinish: true, path) { Interval = actor.ActorInfo.MovementSpeed });
    }

    public void ForceMoveParty(bool keepDirection, bool instant, List<int> path) 
    {
        // TODO: Implement here.
    }
}