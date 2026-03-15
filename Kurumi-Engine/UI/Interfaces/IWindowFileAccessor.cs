namespace UI.Interfaces;

/// <summary>
/// Window file accessor interface, used to access a window files width and height.
/// </summary>
public interface IWindowFileAccessor {
    /// <summary>
    /// Getter for the window file width stored in the window config.
    /// </summary>
    /// <returns>The window file width in config.</returns>
    public int GetWindowFileWidth();

    /// <summary>
    /// Getter for the window file height stored in the window config.
    /// </summary>
    /// <returns>The window file height in config.</returns>
    public int GetWindowFileHeight();
}
