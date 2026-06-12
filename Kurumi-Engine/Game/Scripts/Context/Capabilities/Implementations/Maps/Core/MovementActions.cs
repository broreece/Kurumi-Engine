// Data.
using Data.Definitions.Maps.Base;

using Data.Runtime.Maps.Base.Controllers.Core;
using Data.Runtime.Maps.Core;
using Data.Runtime.Parties.Core;

// Game.
using Game.Scripts.Context.Capabilities.Interfaces.Map;

// Utility.
using Utils.Finishable;

namespace Game.Scripts.Context.Capabilities.Implementations.Maps.Core;

public sealed class MovementActions : IMovementActions 
{
    private readonly Party _party;

    private readonly Map _map;

    internal MovementActions(Party party, Map map) 
    {
        _map = map;
        _party = party;
    }

    public IFinishable ForceMoveActor(
        bool keepDirection, 
        bool lockMovement, 
        bool instant, 
        string actorKey, 
        List<int> path
    ) 
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
                    case (int) SpriteState.North:
                        yPositionChange --;
                        break;

                    case (int) SpriteState.East:
                        xPositionChange ++;
                        break;

                    case (int) SpriteState.South:
                        yPositionChange ++;
                        break;

                    case (int) SpriteState.West:
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

    public void ChangeActorState(string actorKey, int state)
    {
        var actor = _map.GetActor(actorKey);
        actor.SpriteState = state;
    }

    public void ChangeActorPassability(string actorKey, bool passability)
    {
        var actor = _map.GetActor(actorKey);
        actor.Passable = passability;
    }

    public bool ActorIsInState(string actorKey, int actorState)
    {
        var actor = _map.GetActor(actorKey);
        return actor.SpriteState == actorState;
    }
}