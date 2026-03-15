namespace UI.States.Modal.Modals;

using UI.Component.Components;
using UI.Interfaces;
using Utils.Interfaces;

/// <summary>
/// The dialouge with choice state class.
/// </summary>
public class DialogueWithChoiceState : ChoiceState {
    /// <summary>
    /// Constructor for the dialogue with choice state UI state.
    /// </summary>
    /// <param name="windowXPosition">The x position of the window.</param>
    /// <param name="windowYPosition">The y position of the window.</param>
    /// <param name="textWindowXPosition">The x position of the text window.</param>
    /// <param name="textWindowYPosition">The y position of the text window.</param>
    /// <param name="windowWidth">The width of the window.</param>
    /// <param name="windowHeight">The height of the window.</param>
    /// <param name="textWindowWidth">The width of the text window.</param>
    /// <param name="textWindowHeight">The height of the text window.</param>
    /// <param name="fontSize">The font size of the choice state.</param>
    /// <param name="fontFileName">The font art file name.</param>
    /// <param name="windowFileName">The window art file name.</param>
    /// <param name="fileAccessors">The window file accessors object.</param>
    /// <param name="gameWindowDimensionsAccessor">The game window dimensions accessor object.</param>
    /// <param name="choiceBoxXPosition">The x position of the choice box.</param>
    /// <param name="choiceBoxYPosition">The y position of the choice box.</param>
    /// <param name="textXPosition">The x position of the dialogue.</param>
    /// <param name="textYPosition">The y position of the dialogue.</param>
    /// <param name="choiceBoxWidth">The width of the choice box.</param>
    /// <param name="choiceBoxHeight">The height of the choice box.</param>
    /// <param name="spacing">The spacing of the choice box.</param>
    /// <param name="choiceBoxFileName">The choice box file name.</param>
    /// <param name="choices">The array containing all the possible choices.</param>
    /// <param name="text">The 2D array containing the dialogue displayed with the choices.</param>
    /// <param name="conditionalScriptStepAccessor">The current step being executed.</param>
    /// <param name="continuableScript">The context of a script being executed.</param>
    public DialogueWithChoiceState(int windowXPosition, int windowYPosition, int textWindowXPosition, int textWindowYPosition, 
        int windowWidth, int windowHeight, int textWindowWidth, int textWindowHeight, int fontSize, string fontFileName, string windowFileName, 
        IWindowFileAccessors fileAccessors, IGameWindowDimensionsAccessor gameWindowDimensionsAccessor, int choiceBoxXPosition, int choiceBoxYPosition,
        int textXPosition, int textYPosition, int choiceBoxWidth, int choiceBoxHeight, int spacing, string choiceBoxFileName, string[] choices, 
        string[,] text, IConditionalStepAccessor conditionalScriptStepAccessor, IContinuableScript continuableScript) : base(windowXPosition, 
        windowYPosition, windowWidth, windowHeight, fontSize, fontFileName, windowFileName, fileAccessors, gameWindowDimensionsAccessor, 
        choiceBoxXPosition, choiceBoxYPosition, choiceBoxWidth, choiceBoxHeight, spacing, choiceBoxFileName, choices, conditionalScriptStepAccessor, 
        continuableScript) {
        // Add both name window and name text to UI components.
        components.Push(new WindowComponent(textWindowXPosition, textWindowYPosition, textWindowWidth, textWindowHeight, windowFileName, 
            fileAccessors, gameWindowDimensionsAccessor));
        components.Push(new PageTextComponent(textXPosition, textYPosition, fontSize, fontFileName, text));
    }
}