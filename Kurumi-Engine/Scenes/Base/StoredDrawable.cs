namespace Scenes.Base;

using SFML.Graphics;

/// <summary>
/// The stored drawable class used in the scene base to store sprites alongside a potential render state.
/// </summary>
public class StoredDrawable {
    /// <summary>
    /// Constructor for the stored drawable that takes a sprite and a render state.
    /// </summary>
    /// <param name="sprite">The sprite to be drawn.</param>
    /// <param name="renderState">The render state of the sprite.</param>
    public StoredDrawable(Drawable sprite, RenderStates? renderState = null) {
        this.sprite = sprite;
        this.renderState = renderState;
    }

    /// <summary>
    /// Getter for the stored drawable associated sprite.
    /// </summary>
    /// <returns>The sprite stored within the drawable.</returns>
    public Drawable GetSprite() {
        return sprite;
    }

    /// <summary>
    /// Getter for the render state of the stored drawable (If a render state exists).
    /// </summary>
    /// <returns>Null if stored drawable has no render state or the render state if it does contain one.</returns>
    public RenderStates? GetRenderState() {
        return renderState;
    }

    private readonly Drawable sprite;
    private readonly RenderStates ? renderState;
}