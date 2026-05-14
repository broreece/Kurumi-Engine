using Data.Definitions.Entities.Statuses.Core;

namespace Data.Runtime.Entities.Statuses.Core;

public sealed class Status 
{
    private readonly StatusDefinition _definition;

    internal Status(StatusDefinition definition)
    {
        _definition = definition;
    }

    public int Priority => _definition.Priority;
}