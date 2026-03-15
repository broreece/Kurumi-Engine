namespace UI.States.Modal.Modals;

using UI.Component.Components;
using UI.Interfaces;
using Utils.Interfaces;

/// <summary>
/// The dialogue state with name class, locks the players movements.
/// </summary>
public class DialogueWithNameState : DialogueState {
    /// <summary>
    /// Constructor for the dialogue with name state object.
    /// </summary>
    /// <param name="windowXPosition">The x position of the window.</param>
    /// <param name="windowYPosition">The y position of the window.</param>
    /// <param name="nameXPosition">The x position of the name window.</param>
    /// <param name="nameYPosition">The y position of the name window.</param>
    /// <param name="width">The width of the window.</param>
    /// <param name="height">The height of the window.</param>
    /// <param name="nameWidth">The width of the name window.</param>
    /// <param name="nameHeight">The height of the name window.</param>
    /// <param name="windowFileName">The window art file name.</param>
    /// <param name="windowFileAccessor">The window file accessor object.</param>
    /// <param name="gameWindowDimensionsAccessor">The game window dimensions accessor object.</param>
    /// <param name="textXPosition">The x position of the text.</param>
    /// <param name="textYPosition">The y position of the text.</param>
    /// <param name="nameTextXPosition">The x position of the name text.</param>
    /// <param name="nameTextYPosition">The y position of the name text.</param>
    /// <param name="fontSize">The font size.</param>
    /// <param name="fontFileName">The font file name.</param>
    /// <param name="text">The 2D array containing each page of text.</param>
    /// <param name="name">The name displayed alongside the text.</param>
    /// <param name="nextScriptStepAccessor">The next script step accessor object.</param>
    /// <param name="continuableScript">The continuable script obkject.</param>
    public DialogueWithNameState(int windowXPosition, int windowYPosition, int nameXPosition, int nameYPosition, int width, int height, 
        int nameWidth, int nameHeight, string windowFileName, IWindowFileAccessor windowFileAccessor, 
        IGameWindowDimensionsAccessor gameWindowDimensionsAccessor, int textXPosition, int textYPosition, int nameTextXPosition, 
        int nameTextYPosition, float fontSize, string fontFileName, string[,] text, string name, INextScriptStepAccessor nextScriptStepAccessor, 
        IContinuableScript continuableScript) : base(windowXPosition, windowYPosition, width, height, windowFileName, 
        windowFileAccessor, gameWindowDimensionsAccessor, textXPosition, textYPosition, fontSize, fontFileName, text,
        nextScriptStepAccessor, continuableScript) {
        // Add both name window and name text to UI components.
        components.Push(new WindowComponent(nameXPosition, nameYPosition, nameWidth, nameHeight, windowFileName, 
            windowFileAccessor, gameWindowDimensionsAccessor));
        components.Push(new ListTextComponent(nameTextXPosition, nameTextYPosition, fontSize, fontFileName, name));
        }
}