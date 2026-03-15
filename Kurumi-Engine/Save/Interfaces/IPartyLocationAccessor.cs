namespace Save.Interfaces;

/// <summary>
/// Interface used to be able to load party location data.
/// </summary>
public interface IPartyLocationAccessor {
    /// <summary>
    /// Getter for the map elements visible state.
    /// </summary>
    /// <returns>True: The map element is visible.
    /// False: The map element is invisible.</returns>
    public bool GetVisible();

    /// <summary>
    /// Getter for the current maps id.
    /// </summary>
    /// <returns>The current map id, used when saving party information.</returns>
    public int GetCurrentMapId();

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

    /// <summary>
    /// Getter for the map elements direction.
    /// </summary>
    /// <returns>The direction the map element is facing.</returns>
    public int GetDirection();
}