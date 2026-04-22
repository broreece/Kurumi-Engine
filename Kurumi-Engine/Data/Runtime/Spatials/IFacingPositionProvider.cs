namespace Data.Runtime.Spatials;

/// <summary>
/// Allows access to the facing direction of the object.
/// </summary>
public interface IFacingPositionProvider : IPositionProvider 
{
    public int Facing { get; }
}