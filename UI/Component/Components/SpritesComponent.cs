namespace UI.Component.Components;

using UI.Component.Core;
using UI.Core;
using SFML.Graphics;

/// <summary>
/// The sprites component class, stores information needed to draw multiple sprites.
/// </summary>
public sealed class SpritesComponent : IUIComponent {
    /// <summary>
    /// Constructor for the sprites component.
    /// </summary>
    public SpritesComponent() {
        spriteGroup = new SpriteGroup();
    }

    /// <summary>
    /// Function used to add a new sprite to the list of sprites in the sprites component.
    /// </summary>
    /// <param name="sprite">The new sprite to be added.</param>
    public void AddSprite(Sprite sprite) {
        spriteGroup.Add(sprite);
    }

    /// <summary>
    /// Function used to clear the list of the sprites.
    /// </summary>
    public void ClearList() {
        spriteGroup.Clear();
    }

    /// <summary>
    /// Function that returns all the component sprites.
    /// </summary>
    /// <returns>Returns the sprites of the component.</returns>
    public Drawable CreateSprite() {
        return spriteGroup;
    }

    private readonly SpriteGroup spriteGroup;
}