using Data.Definitions.Maps.Base;
using Data.Runtime.Actors.Controllers.Core;
using Data.Runtime.Maps.Core;
using Data.Runtime.Party.Core;

using Game.Scripts.Context.Capabilities.Interfaces.Map;

using Utils.Finishable;

namespace Game.Scripts.Context.Capabilities.Implementations.Maps;

public sealed class MovementActions : IMovementActions 
{
    private readonly Map _map;

    private readonly Party _party;

    public MovementActions(Map map, Party party) 
    {
        _map = map;
        _party = party;
    }

    public IFinishable ForceMoveActor(
        bool keepDirection, 
        bool lockMovement, 
        bool instant, 
        string actorKey, 
        List<int> path) 
    {
        var actor = _map.GetActor(actorKey);
        actor.MaintainFacing = keepDirection;

        var controller = new PathedController(canFinish: true, path) { Interval = actor.MovementSpeed };
        actor.AddController(controller);

        return controller;
    }

    public IFinishable ForceMoveParty(bool keepDirection, bool instant, IReadOnlyList<int> path) 
    {
        if (instant)
        {
            int xPositionChange = 0;
            int yPositionChange = 0;
            foreach (int movement in path)
            {
                switch (movement)
                {
                    case (int) Direction.North:
                        yPositionChange --;
                        break;

                    case (int) Direction.East:
                        xPositionChange ++;
                        break;

                    case (int) Direction.South:
                        yPositionChange ++;
                        break;

                    case (int) Direction.West:
                        xPositionChange --;
                        break;

                    default:
                        break;
                }
            }
            _party.XLocation += xPositionChange;
            _party.YLocation += yPositionChange;
            return new Finished();
        }
        else
        {
            // TODO: (ASE-01) - Change interval here to be equal to the set speed of the movement.
            var controller = new PathedController(canFinish: true, path) { Interval = 1 };
            _party.PathedController = controller;
            return controller;
        }
    }
}