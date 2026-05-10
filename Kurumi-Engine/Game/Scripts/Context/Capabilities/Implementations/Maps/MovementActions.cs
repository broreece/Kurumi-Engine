using Data.Runtime.Actors.Controllers.Core;
using Data.Runtime.Maps.Core;

using Game.Scripts.Context.Capabilities.Interfaces.Map;

using Utils.Interfaces;

namespace Game.Scripts.Context.Capabilities.Implementations.Maps;

public sealed class MovementActions : IMovementActions 
{
    private readonly Map _map;

    public MovementActions(Map map) 
    {
        _map = map;
    }

    public IFinishable ForceMoveActor(
        bool keepDirection, 
        bool lockMovement, 
        bool instant, 
        int actorIndex, 
        List<int> path) 
    {
        var actor = _map.Actors[actorIndex];
        actor.MaintainFacing = keepDirection;

        var controller = new PathedController(canFinish: true, path) { Interval = actor.ActorInfo.MovementSpeed };
        actor.AddController(controller);

        return controller;
    }

    public void ForceMoveParty(bool keepDirection, bool instant, List<int> path) 
    {
        // TODO: Implement here.
    }
}