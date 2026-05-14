using Data.Runtime.Entities.Core;

namespace Data.Runtime.Formations.Base;

/// <summary>
/// Contains an entity alongside it's draw coordinates.
/// </summary>
public readonly struct StoredEntityData 
{
    public required Entity Entity { get; init; }

    public required int XLocation { get; init; }
    public required int YLocation { get; init; }
}