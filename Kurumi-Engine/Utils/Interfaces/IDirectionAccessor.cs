namespace Utils.Interfaces;

/// <summary>
/// The public direction accessor interface, used to check direction object is facing.
/// </summary>
public interface IDirectionAccessor {
    /// <summary>
    /// Getter for the map elements direction.
    /// </summary>
    /// <returns>The direction the map element is facing.</returns>
    public int GetDirection();
}