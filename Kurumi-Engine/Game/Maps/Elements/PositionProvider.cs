namespace Game.Maps.Elements;

using Utils.Interfaces;

/// <summary>
/// The position provider class, contains and X and Y location, used to abstract data.
/// </summary>
public abstract class PositionProvider : ICoordinateAccessor {
    /// <summary>
    /// The constructor for the position provider class.
    /// </summary>
    /// <param name="xLocation">The x location for the position provider.</param>
    /// <param name="yLocation">The y location for the position provider.</param>
    public PositionProvider(int xLocation, int yLocation) {
        this.xLocation = xLocation;
        this.yLocation = yLocation;
    }

    /// <summary>
    /// Gets the position provider's X location.
    /// </summary>
    /// <returns>The position provider's X coordinate.</returns>
    public int GetXLocation() {
        return xLocation;
    }

    /// <summary>
    /// Sets the position provider's X location.
    /// </summary>
    /// <param name="newX">The new X coordinate of the position provider.</param>
    public void SetXLocation(int newX) {
        xLocation = newX;
    }

    /// <summary>
    /// Gets the position provider's Y location.
    /// </summary>
    /// <returns>The position provider's Y coordinate.</returns>
    public int GetYLocation() {
        return yLocation;
    }

    /// <summary>
    /// Sets the position provider's Y location.
    /// </summary>
    /// <param name="newX">The new Y coordinate of the position provider.</param>
    public void SetYLocation(int newY) {
        yLocation = newY;
    }
    
    private int xLocation, yLocation;
}