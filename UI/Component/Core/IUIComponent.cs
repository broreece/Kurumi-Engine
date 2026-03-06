namespace UI.Component.Core;

using SFML.Graphics;

/// <summary>
/// The public UI component interface
/// </summary>
public interface IUIComponent {
    /// <summary>
    /// Function that returns the component sprite.
    /// </summary>
    /// <returns>Returns the sprite of the component.</returns>
    public abstract Drawable CreateSprite();
}