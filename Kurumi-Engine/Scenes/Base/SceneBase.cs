namespace Scenes.Base;

using Engine.Assets;
using Engine.Rendering;
using SFML.Graphics;

/// <summary>
/// Abstract scene class, contains information shared between all scenes.
/// </summary>
public abstract class SceneBase {
    /// <summary>
    /// Constructor for the scene base class.
    /// </summary>
    /// <param name="window">The game window object.</param>
    /// <param name="assetManager">The game asset manager object, used to load textures, fonts etc.</param>
    protected SceneBase(GameWindow window, AssetManager assetManager) {
        this.window = window;
        this.assetManager = assetManager;
        sprites = [];
    }

    /// <summary>
    /// Function that passes all sprites to the game window to be displayed.
    /// </summary>
    public void Draw() {
        foreach (StoredDrawable sprite in sprites) {
            RenderStates? renderState = sprite.GetRenderState();
            if (renderState != null) {
                window.Draw(sprite.GetSprite(), (RenderStates) renderState);
            }
            else {
                window.Draw(sprite.GetSprite());
            }
        }
    }

    /// <summary>
    /// Function used to reset the clocks of a scene (Used primarily in map scenes).
    /// </summary>
    public virtual void ResetClocks() {}

    /// <summary>
    /// Function that updates sprites on a scene.
    /// </summary>
    public abstract void Update();

    /// <summary>
    /// Adds a sprite to the list of sprites.
    /// </summary>
    /// <param name="newSprite">The new sprite to be added to the list of sprites.</param>
    protected void AddSprite(Drawable newSprite) {
        sprites.Add(new StoredDrawable(newSprite));
    }

    /// <summary>
    /// Adds a sprite to the list of sprites alongside a render state parameter.
    /// </summary>
    /// <param name="newSprite">The new sprite to be added to the list of sprites.</param>
    /// <param name="renderStates">The render states parameter to be added to the sprite.</param>
    protected void AddSpriteWithState(Drawable newSprite, RenderStates renderStates) {
        sprites.Add(new StoredDrawable(newSprite, renderStates));
    }

    /// <summary>
    /// Function that clears the list of sprites in the scene.
    /// </summary>
    protected void ClearSprites() {
        sprites = [];
    }

    protected readonly GameWindow window;
    protected readonly AssetManager assetManager;
    private List<StoredDrawable> sprites;
}