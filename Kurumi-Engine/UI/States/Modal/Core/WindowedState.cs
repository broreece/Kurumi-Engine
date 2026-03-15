namespace UI.States.Modal.Core;

using UI.Component.Components;
using UI.Core;
using UI.Interfaces;

/// <summary>
/// The abstract windowed state class, inherited by states that utilize a window component with varying controls.
/// </summary>
public abstract class WindowedState : UIState {
    /// <summary>
    /// Constructor for the windowed state object.
    /// </summary>
    /// <param name="windowXPosition">The x position of the window.</param>
    /// <param name="windowYPosition">The y position of the window.</param>
    /// <param name="width">The width of the window.</param>
    /// <param name="height">The height of the window.</param>
    /// <param name="windowFileName">The window art file name.</param>
    /// <param name="windowFileAccessor">The window file accessor object.</param>
    /// <param name="gameWindowDimensionsAccessor">The game window dimensions accessor object.</param>
    public WindowedState(int windowXPosition, int windowYPosition, int width, int height, string windowFileName, IWindowFileAccessor windowFileAccessor,
        IGameWindowDimensionsAccessor gameWindowDimensionsAccessor, int textXPosition, int textYPosition, float fontSize, 
        string fontFileName, string[,] text) {
        components.Push(new WindowComponent(windowXPosition, windowYPosition, width, height, windowFileName, windowFileAccessor, 
            gameWindowDimensionsAccessor));
        textComponent = new PageTextComponent(textXPosition, textYPosition, fontSize, fontFileName, text);
        components.Push(textComponent);
    }

    // Store text component here so we can adjust it's current page and change it's displayed text.
    protected readonly PageTextComponent textComponent;
}
