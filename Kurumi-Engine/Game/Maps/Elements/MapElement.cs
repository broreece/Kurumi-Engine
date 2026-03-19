namespace Game.Maps.Elements;

/// <summary>
/// The abstract map element class, used by party and all NPCs.
/// </summary>
public abstract class MapElement : PositionProvider {
    /// <summary>
    /// Constructor for the map element class.
    /// </summary>
    /// <param name="xLocation">X location of the party.</param>
    /// <param name="yLocation">Y location of the party.</param>
    /// <param name="direction">The direction the party is facing.</param>
    /// <param name="visible">If the party is visible.</param>
    public MapElement(int xLocation, int yLocation, int direction, bool visible) : base(xLocation, yLocation) {
        oldXLocation = xLocation;
        oldYLocation = yLocation;
        this.direction = (Direction) direction;
        this.visible = visible;
        currentAnimationFrame = 0;
    }

    /// <summary>
    /// Gets the map element's current animation frame.
    /// </summary>
    /// <returns>The map element's current animation frame.</returns>
    public int GetCurrentAnimationFrame() {
        return currentAnimationFrame;
    }

    /// <summary>
    /// Sets the map element's current animation frame.
    /// </summary>
    /// <param name="newAnimationFrame">The new current animation frame of the map element.</param>
    public void SetCurrentAnimationFrame(int newAnimationFrame) {
        currentAnimationFrame = newAnimationFrame;
    }

    /// <summary>
    /// Gets the map element's previous X location.
    /// </summary>
    /// <returns>The map elements previous X coordinate.</returns>
    public int GetOldXLocation() {
        return oldXLocation;
    }

    /// <summary>
    /// Sets the map element's previous X location.
    /// </summary>
    /// <param name="oldX">The previous X coordinate of the map element.</param>
    public void SetOldXLocation(int oldX) {
        oldXLocation = oldX;
    }

    /// <summary>
    /// Gets the map element's previous Y location.
    /// </summary>
    /// <returns>The map element's previous Y coordinate.</returns>
    public int GetOldYLocation() {
        return oldYLocation;
    }

    /// <summary>
    /// Sets the map element's previous Y location.
    /// </summary>
    /// <param name="newX">The previous Y coordinate of the map element.</param>
    public void SetOldYLocation(int oldY) {
        oldYLocation = oldY;
    }

    /// <summary>
    /// Getter for the map elements direction.
    /// </summary>
    /// <returns>The direction the map element is facing.</returns>
    public int GetDirection() {
        return (int) direction;
    }

    /// <summary>
    /// Sets the map elements direction.
    /// </summary>
    /// <param name="newDirection">The new direction of the map element.</param>
    public void SetDirection(Direction newDirection) {
        direction = newDirection;
    }

    /// <summary>
    /// Getter for the map elements visible state.
    /// </summary>
    /// <returns>True: The map element is visible.
    /// False: The map element is invisible.</returns>
    public bool GetVisible() {
        return visible;
    }

    /// <summary>
    /// Sets the map elements visiblity.
    /// </summary>
    /// <param name="newX">The new visiblity state of the map element.</param>
    public void SetVisible(bool newVisible) {
        visible = newVisible;
    }

    private int currentAnimationFrame, oldXLocation, oldYLocation;
    protected Direction direction;
    protected bool visible;
}