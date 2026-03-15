namespace UI.Component.Core;

using SFML.Graphics;

/// <summary>
/// The public positionable UI component abstract class.
/// </summary>
public abstract class PositionableComponent : IUIComponent {
    /// <summary>
    /// Constructor for the UI component abstract object.
    /// </summary>
    /// <param name="xPosition">The x position of the component.</param>
    /// <param name="yPosition">The y position of the component.</param>
    protected PositionableComponent(int xPosition, int yPosition) {
        this.xPosition = xPosition;
        this.yPosition = yPosition;
    }

    /// <summary>
    /// Getter for the component's x position.
    /// </summary>
    /// <returns>Returns the component's x position.</returns>
    public int GetXPosition() {
        return xPosition;
    }

    /// <summary>
    /// Getter for the component's y position.
    /// </summary>
    /// <returns>Returns the component's y position.</returns>
    public int GetYPosition() {
        return yPosition;
    }

    /// <summary>
    /// Function that returns the component sprite.
    /// </summary>
    /// <returns>Returns the sprite of the component.</returns>
    public abstract Drawable CreateSprite();

    protected readonly int xPosition, yPosition;
}