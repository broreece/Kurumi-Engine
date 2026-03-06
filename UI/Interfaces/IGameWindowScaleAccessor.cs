namespace UI.Interfaces;

/// <summary>
/// Scale accessor interface, used to access a window's width and height scale.
/// </summary>
public interface IGameWindowScaleAccessor : IGameWindowDimensionsAccessor {
    /// <summary>
    /// Gets the width scale, used to scale all scenes sprites.
    /// </summary>
    /// <returns>The scale scenes will use.</returns>
    public float GetWidthScale();

    /// <summary>
    /// Gets the height scale, used to scale all scenes sprites.
    /// </summary>
    /// <returns>The scale scenes will use.</returns>
    public float GetHeightScale();
}