namespace UI.States.Modal.Modals;

using Engine.Input.Scenes;
using UI.Input;
using UI.Interfaces;
using UI.States.Modal.Core;
using Utils.Interfaces;

/// <summary>
/// The dialogue state class, dialogue is when only text is displayed, locking the players movements.
/// </summary>
public class DialogueState : WindowedState, IDialogueInputController {
    /// <summary>
    /// Constructor for the dialogue state object.
    /// </summary>
    /// <param name="windowXPosition">The x position of the window.</param>
    /// <param name="windowYPosition">The y position of the window.</param>
    /// <param name="width">The width of the window.</param>
    /// <param name="height">The height of the window.</param>
    /// <param name="windowFileName">The window art file name.</param>
    /// <param name="windowFileAccessor">The window file accessor object.</param>
    /// <param name="gameWindowDimensionsAccessor">The game window dimensions accessor object.</param>
    /// <param name="textXPosition">The x position of the text.</param>
    /// <param name="textYPosition">The y position of the text.</param>
    /// <param name="fontSize">The font size.</param>
    /// <param name="fontFileName">The font file name.</param>
    /// <param name="text">The 2D array containing each page of text.</param>
    /// <param name="nextScriptStepAccessor">The next script step accessor object.</param>
    /// <param name="continuableScript">The continuable script obkject.</param>
    public DialogueState(int windowXPosition, int windowYPosition, int width, int height, string windowFileName, IWindowFileAccessor windowFileAccessor,
        IGameWindowDimensionsAccessor gameWindowDimensionsAccessor, int textXPosition, int textYPosition, float fontSize, 
        string fontFileName, string[,] text, INextScriptStepAccessor nextScriptStepAccessor, IContinuableScript continuableScript) : base(windowXPosition, windowYPosition, width, height, windowFileName, 
        windowFileAccessor, gameWindowDimensionsAccessor, textXPosition, textYPosition, fontSize, fontFileName, text) {
        // Assign script context.
        this.nextScriptStepAccessor = nextScriptStepAccessor;
        this.continuableScript = continuableScript;

        // Assign input map.
        inputMap = new DialogueInputMap(this);
    }

    /// <summary>
    /// /// Function used to select and proceed in the dialogue state.
    /// </summary>
    public void Select() {
        // Checks the number of pages in the window.
        if (textComponent.GetCurrentPage() == textComponent.GetMaxPage() - 1) {
            Close();
        }
        else {
            textComponent.IncrementCurrentPage();
        }
    }

    /// <summary>
    /// Function used to close the dialogue state.
    /// </summary>
    protected override void Close() {
        // TODO: (UICA-01) Implement closing animation.
        continuableScript.ContinueScript(nextScriptStepAccessor);
        closed = true;
    }

    private readonly INextScriptStepAccessor nextScriptStepAccessor;
    private readonly IContinuableScript continuableScript;
}