namespace Engine.Systems;

using Game.Entities.Base;
using Game.Entities.Status;

/// <summary>
/// The status resolver system class. Allows application of statuses and logic based around priorities.
/// </summary>
public sealed class StatusResolver {
    /// <summary>
    /// The apply status function that applies a status to a given entity.
    /// </summary>
    /// <param name="entity">The entity which will have the status added.</param>
    /// <param name="status">The status being added to the entity.</param>
    public void ApplyStatus(Entity entity, Status newStatus) {
        Status[] statuses = [.. entity.GetStatuses()];
        // Check if all existing statuses needs to be removed.
        if (statuses.Length != 0) {
            if (statuses[0].GetPriority() != Priority.Fainted) {
                switch (newStatus.GetPriority()) {
                    case Priority.CanStackWithOthers:
                        bool canAdd = true;
                        foreach (Status status in statuses) {
                            if (status.GetPriority() == Priority.EraseIfOtherIsAdded) {
                                entity.RemoveStatus(status);
                            }
                            else if (status.GetPriority() == Priority.EraseAllOthersWhenAdded
                                || status.GetPriority() == Priority.Fainted) {
                                canAdd = false;
                            }
                        }
                        if (canAdd) {
                            entity.AddStatus(newStatus);
                        }
                        break;

                    case Priority.EraseIfOtherIsAdded:
                        bool canAddErase = true;
                        foreach (Status status in statuses) {
                            if (status.GetPriority() == Priority.EraseIfOtherIsAdded) {
                                entity.RemoveStatus(status);
                            }
                            else {
                                canAdd = false;
                            }
                        }
                        if (canAddErase) {
                            entity.AddStatus(newStatus);
                        }
                        break;

                    default:
                        entity.ClearStatuses();
                        entity.AddStatus(newStatus);
                        break;
                }
            }
        }
        else {
            entity.AddStatus(newStatus);
        }
    }
}