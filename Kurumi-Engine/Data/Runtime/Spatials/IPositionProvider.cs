namespace Data.Runtime.Spatials;

/// <summary>
/// Allows access to an objects X and Y location.
/// </summary>
public interface IPositionProvider 
{
    public int XLocation { get; }

    public int YLocation { get; }
}