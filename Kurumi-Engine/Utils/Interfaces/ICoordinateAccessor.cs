namespace Utils.Interfaces;

/// <summary>
/// The public coordinate accessor interface, used to check coordinates.
/// </summary>
public interface ICoordinateAccessor {
    /// <summary>
    /// Gets the position provider's X location.
    /// </summary>
    /// <returns>The position provider's X coordinate.</returns>
    public int GetXLocation();

    /// <summary>
    /// Gets the position provider's Y location.
    /// </summary>
    /// <returns>The position provider's Y coordinate.</returns>
    public int GetYLocation();
}