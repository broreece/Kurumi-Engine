using Data.Definitions.Entities.Status.Base;
using Data.Definitions.Entities.Status.Core;
using Engine.Systems.Statuses.Interfaces;
using Infrastructure.Database.Base;

namespace Engine.Systems.Statuses.Core;

/// <summary>
/// Allows application of statuses and logic based around priorities.
/// </summary>
public sealed class StatusResolver 
{
    private readonly Registry<Status> _statusRegistry;

    public StatusResolver(Registry<Status> statusRegistry) {
        _statusRegistry = statusRegistry;
    }

    public void ApplyStatus(IHasStatuses statusableObject, int newStatusId) 
    {
        List<int> statuses = statusableObject.GetStatuses();
        var newStatus = _statusRegistry.Get(newStatusId);

        // Check if we can apply the statuses based on priorites.
        if (CanApply(statuses, newStatus)) 
        {
            // Resolve any erasure rules of statuses.
            if ((StatusPriority) newStatus.Priority != StatusPriority.CanStack) 
            {
                statuses.Clear();
                statuses.Add(newStatusId);
            }
            else 
            {
                statuses.Add(newStatusId);
            }
        }
    }

    private bool CanApply(List<int> statuses, Status newStatus) 
    {
        foreach (var statusId in statuses) 
        {
            var status = _statusRegistry.Get(statusId);
            if (newStatus.Priority < status.Priority) 
            {
                return false;
            }
        }
        return true;
    }
}