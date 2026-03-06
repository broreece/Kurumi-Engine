namespace UI.Component.Components;

using UI.Component.Core;
using UI.Interfaces;
using SFML.Graphics;
using SFML.System;

/// <summary>
/// The window class, stores information needed to draw a window UI element.
/// </summary>
public sealed class WindowComponent : ImageComponentBase {
    /// <summary>
    /// Constructor for the window object.
    /// </summary>
    /// <param name="xPosition">The x position of the window.</param>
    /// <param name="yPosition">The y position of the window.</param>
    /// <param name="width">The width of the window.</param>
    /// <param name="height">The height of the window.</param>
    /// <param name="windowFileName">The window art file name.</param>
    /// <param name="windowFileAccessor">The window file accessor object.</param>
    /// <param name="gameWindowDimensionAccessor">The game window dimension accessor object.</param>
    public WindowComponent(int xPosition, int yPosition, int width, int height, string windowFileName, IWindowFileAccessor windowFileAccessor, 
        IGameWindowDimensionsAccessor gameWindowDimensionAccessor) 
        : base(xPosition, yPosition, width, height, windowFileAccessor.GetWindowFileWidth(), windowFileAccessor.GetWindowFileHeight(), 
            windowFileName, gameWindowDimensionAccessor) {       
        // Load values from config.
        windowFileWidth = windowFileAccessor.GetWindowFileWidth();
        windowFileHeight = windowFileAccessor.GetWindowFileHeight();
        // Load texture in advanced.
        windowTexture = new(windowFileName);
    }

    /// <summary>
    /// Function that returns the window sprite.
    /// </summary>
    /// <returns>Returns the sprite of the window object.</returns>
    public override Drawable CreateSprite() {
        Sprite windowSprite = new(windowTexture, new IntRect(0, 0, windowFileWidth, windowFileHeight))
        {
            // Apply scale.
            Scale = new Vector2f(width, height)
        };

        // Place and scale the window art.
        Vector2f position = new(xPosition, yPosition);
        windowSprite.Position = position;
        return windowSprite;
    }

    private readonly int windowFileWidth, windowFileHeight;
    private readonly Texture windowTexture;
}
