// Data.
using Data.Runtime.Spatials;

namespace Data.Runtime.Maps.Base.Controllers.Base;

/// <summary>
/// Used when provided a hard location for tracking actors to go to, such as a return point.
/// </summary>
public sealed class Position : IPositionProvider 
{
    public required int XLocation { get; init; }
    public required int YLocation { get; init; }
}