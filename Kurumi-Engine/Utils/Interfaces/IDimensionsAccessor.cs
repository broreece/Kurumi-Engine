namespace Utils.Interfaces;

/// <summary>
/// Dimension accessor interface, used to access a objects width and height.
/// </summary>
public interface IDimensionsAccessor {
    /// <summary>
    /// Getter for the object width.
    /// </summary>
    /// <returns>The object width.</returns>
    public int GetWidth();

    /// <summary>
    /// Getter for the object height.
    /// </summary>
    /// <returns>The object height.</returns>
    public int GetHeight();
}
