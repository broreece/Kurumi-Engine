namespace UI.Core;

using SFML.Graphics;

/// <summary>
/// The public sprite group class, a type of SFML drawable that contains multiple SFML sprite objects, used in components to draw
/// multiple sprites.
/// </summary>
public class SpriteGroup : Drawable {
    /// <summary>
    /// Constructor for the sprite group class.
    /// </summary>
    public SpriteGroup() {
        sprites = [];
    }

    /// <summary>
    /// Interfaced drawable function that draws the object onto the target.
    /// </summary>
    /// <param name="target">The render target object.</param>
    /// <param name="states">The render states object.</param>
    public void Draw(RenderTarget target, RenderStates states) {
        foreach (Sprite sprite in sprites) {
            target.Draw(sprite);
        }
    }

    /// <summary>
    /// Function used to add a new sprite to the sprite group.
    /// </summary>
    /// <param name="newSprite">The new sprite to be added.</param>
    public void Add(Sprite newSprite) {
        sprites.Add(newSprite);
    }

    /// <summary>
    /// Function used to clear the list of sprites.
    /// </summary>
    public void Clear() {
        sprites = [];
    }

    private List<Sprite> sprites;
}