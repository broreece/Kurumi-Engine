using Data.Definitions.Entities.Statuses.Core;
using Data.Runtime.Entities.Statuses.Core;

namespace Data.Runtime.Entities.Statuses.Factories;

public sealed class StatusFactory 
{
    public Status Create(StatusDefinition definition) 
    {
        return new Status(definition);
    }
}