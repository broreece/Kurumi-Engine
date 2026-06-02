// Data.
using Data.Runtime.Entities.Statuses.Core;

namespace Engine.Systems.Statuses.Interfaces;

public interface IHasStatuses 
{
    public IList<Status> Statuses { get; }

    public void ClearStatuses();

    public void AddStatus(Status newStatus);
}