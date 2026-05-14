using Data.Definitions.Maps.Base;
using Data.Runtime.Spatials;
using Engine.Systems.Navigation.Core;

namespace Engine.Systems.Perception.Core;

/// <summary>
/// Used to check if a location provider is in vision of another position provider.
/// </summary>
public sealed class VisionResolver 
{
    private const int PeripheralRange = 2;

    private readonly NavigationGrid _navigationGrid;

    internal VisionResolver(NavigationGrid navigationGrid) 
    {
        _navigationGrid = navigationGrid;
    }

    public bool CanSee(IFacingPositionProvider viewer, IPositionProvider target, int range) 
    {
        var xDifference = target.XLocation - viewer.XLocation;
        var yDifference = target.YLocation - viewer.YLocation;

        switch (viewer.Facing) 
        {
            case (int) Direction.North:
                if (WithinRange(yDifference, range) 
                    && InPeripheralVision(xDifference) 
                    && !OutOfSight(viewer.YLocation, target.YLocation)) 
                {
                    return ClearSight(yDifference, xDifference, viewer.YLocation, viewer.XLocation, target.YLocation);
                }
                return false;
                
            case (int) Direction.East:
                if (WithinRange(xDifference, range) 
                    && InPeripheralVision(yDifference) 
                    && !OutOfSight(viewer.XLocation, target.XLocation)) 
                {
                    return ClearSight(xDifference, yDifference, viewer.XLocation, viewer.YLocation, target.XLocation);
                }
                return false;


            case (int) Direction.South:
                if (WithinRange(yDifference, range) 
                    && InPeripheralVision(xDifference) 
                    && !OutOfSight(viewer.YLocation, target.YLocation)) 
                {
                    return ClearSight(yDifference, xDifference, viewer.YLocation, viewer.XLocation, target.YLocation);
                }
                return false;


            default:
                if (WithinRange(xDifference, range) 
                    && InPeripheralVision(yDifference) 
                    && !OutOfSight(viewer.XLocation, target.XLocation)) 
                {
                    return ClearSight(xDifference, yDifference, viewer.XLocation, viewer.YLocation, target.XLocation);
                }
                return false;
                
        }
    }

    private bool WithinRange(int distance, int range) => range * -1 <= distance && distance <= range;

    /// <summary>
    /// Enables checking if a target is in the peripheral vision of an actor instead of just in front of them.
    /// </summary>
    /// <param name="peripheralDifference">The peripheral difference (The non-main axis difference) of the 
    /// target.</param>
    /// <returns>If the target is within the peripheral difference.</returns>
    private bool InPeripheralVision(int peripheralDifference) 
    {
        return PeripheralRange * -1 <= peripheralDifference && peripheralDifference <= PeripheralRange;
    }

    /// <summary>
    /// Function used to check if the target is out of the sight of the actor regardless of other checks.
    /// Used to check if the target is on the same main axis coordinate.
    /// </summary>
    /// <param name="viewerLocation">The viewer's relevant location coordinate.</param>
    /// <param name="targetLocation">The target's relevant location coordinate.</param>
    /// <returns>If the target is just out of sight of the viewer.</returns>
    private bool OutOfSight(int viewerLocation, int targetLocation) => viewerLocation == targetLocation;

    /// <summary>
    /// Function used to check if the target is in clear view of the viewer in terms of passability of tiles and
    /// actors in between viewer and target.
    /// </summary>
    /// <param name="distance">The distance between target and user.</param>
    /// <param name="peripheralDistance">The peripheral difference (Not the main facing direction) between target
    /// and user.</param>
    /// <param name="viewerLocation">The viewer's relevant location coordinate.</param>
    /// <param name="viewerPeriphery">The viewer's periphery coordinate.</param>
    /// <param name="targetLocation">The target's relevant location coordinate.</param>
    /// <returns>If there is a clear line of sight between viewer and target.</returns>
    private bool ClearSight(int distance, int peripheralDistance, int viewerLocation, int viewerPeriphery, 
        int targetLocation) 
        {
        // Use a for loop that goes either in positive or negative range based on distance.
        var step = distance > 0 ? 1 : -1;
        for (var coordinate = step > 0 ? 1 : -1; step > 0 ? coordinate <= distance : coordinate >= distance; 
            coordinate += step) 
            {
            if (viewerLocation + coordinate == targetLocation) 
            {
                return true;
            }
            if (!_navigationGrid.IsLocationSeeThrough(
                viewerLocation + coordinate, 
                viewerPeriphery + peripheralDistance
            )) 
            {
                return false;
            }
        }
        // TODO: (DKE-01) Custom exception here, because we check for in range this line should never be reached.
        return false;
    }
}