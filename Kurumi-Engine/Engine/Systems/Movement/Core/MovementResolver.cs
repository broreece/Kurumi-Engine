using Data.Definitions.Maps.Base;
using Data.Runtime.Actors.Core;
using Data.Runtime.Maps.Core;
using Data.Runtime.Spatials;
using Engine.Systems.Navigation.Core;

namespace Engine.Systems.Movement.Core;

/// <summary>
/// System used to resolve movements from the party and actors on the map.
/// </summary>
public sealed class MovementResolver 
{
    private readonly Map _map;
    private readonly NavigationGrid _navigationGrid;

    internal MovementResolver(Map map, NavigationGrid navigationGrid) 
    {
        _map = map;
        _navigationGrid = navigationGrid;
    }

    public void TryMove(IMutablePositionProvider mutablePositionProvider, int direction) 
    {
        int xChange = direction == (int) Direction.West ? -1 : direction == (int) Direction.East ? 1 : 0;
        int yChange = direction == (int) Direction.South ? 1 : direction == (int) Direction.North ? -1 : 0;
        var oldX = mutablePositionProvider.XLocation;
        var oldY = mutablePositionProvider.YLocation;
        var newX = mutablePositionProvider.XLocation + xChange;
        var newY = mutablePositionProvider.YLocation + yChange;
        
        if (_navigationGrid.IsNavigable(newX, newY)) 
        {
            mutablePositionProvider.LastX = oldX;
            mutablePositionProvider.LastY = oldY;
            mutablePositionProvider.XLocation = newX;
            mutablePositionProvider.YLocation = newY;

            // Start the mutable position providers walk animation and update actor grid.
            mutablePositionProvider.StartMovement();
            if (mutablePositionProvider is Actor actor) 
            {
                _map.RemoveActorAt(actor, oldX, oldY);
                _map.AddActorTo(actor, newX, newY);

                // Edge case when an actor is being forced to move without changing the facing direction.
                // TODO: (DSS-01) When adding the force move party steps we should check as well if the party is
                // maintaining facing direction.
                if (actor.MaintainFacing) 
                {
                    direction = actor.Facing;
                }
            }
        }
        mutablePositionProvider.Facing = direction;
    } 
}