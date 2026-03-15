namespace UI.Interfaces;

/// <summary>
/// Dimension accessor interface, used to access a window's width and height.
/// </summary>
public interface IGameWindowDimensionsAccessor {
    /// <summary>
    /// Getter for the window width stored in the window config.
    /// </summary>
    /// <returns>The window width in config.</returns>
    public int GetWidth();

    /// <summary>
    /// Getter for the window height stored in the window config.
    /// </summary>
    /// <returns>The window height in config.</returns>
    public int GetHeight();
}
