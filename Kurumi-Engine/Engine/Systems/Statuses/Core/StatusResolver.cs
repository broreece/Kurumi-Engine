using Data.Definitions.Entities.Statuses.Base;
using Data.Runtime.Entities.Statuses.Core;

using Engine.Systems.Statuses.Interfaces;

namespace Engine.Systems.Statuses.Core;

/// <summary>
/// Allows application of statuses and logic based around priorities.
/// </summary>
public sealed class StatusResolver 
{
    public void TryApplyStatus(IHasStatuses statusableObject, Status newStatus) 
    {
        var statuses = statusableObject.GetStatuses();

        // Check if we can apply the statuses based on priorites.
        if (CanApply(statuses, newStatus)) 
        {
            // Resolve any erasure rules of statuses.
            if ((StatusPriority) newStatus.Priority != StatusPriority.CanStack) 
            {
                statusableObject.ClearStatuses();
                statusableObject.AddStatus(newStatus);
            }
            else 
            {
                statusableObject.AddStatus(newStatus);
            }
        }
    }

    private bool CanApply(IList<Status> statuses, Status newStatus) 
    {
        foreach (var status in statuses) 
        {
            if (newStatus.Priority < status.Priority) 
            {
                return false;
            }
        }
        return true;
    }
}