namespace Scripts.UniversalScriptSteps;

using Scripts.Base;
using Scripts.Conditional;

/// <summary>
/// The choice box script step.
/// </summary>
public sealed class ChoiceBox : ConditionalScript {
    /// <summary>
    /// Constructor for the choice box script step.
    /// </summary>
    /// <param name="artId">The art ID of the window.</param>
    /// <param name="fontId">The font ID of the window's text.</param>
    /// <param name="xPosition">The x position of the window.</param>
    /// <param name="yPosition">The y position of the window.</param> 
    /// <param name="choices">The choices that are displayed.</param>
    /// <param name="nextIfFalse">The index of the next script step if negative choice is made.</param>
    public ChoiceBox(int windowArtId, int fontId, int xPosition, int yPosition, string[] choices, int nextIfFalse) : base(nextIfFalse) {
        this.windowArtId = windowArtId;
        this.fontId = fontId;
        this.xPosition = xPosition;
        this.yPosition = yPosition;
        this.choices = choices;
    }
    
    /// <summary>
    /// Activates the choice box scene script by creating and initiating a choice box.
    /// </summary>
    /// <param name="scriptContext">The context of the script.</param>
    public override void Activate(ScriptContext scriptContext) {
        //SetConditionMet(false);
        //ChoiceState choiceState = new(xPosition, yPosition, windowWidth, windowHeight, fontSize, fontFileName, windowFileName,
        //    windowConfig, gameWindow, xPosition, yPosition, windowWidth, choiceBoxHeight, spacing, choiceBoxFileName, choices);
        // TODO: Push this state onto stack.
        // TODO: We need to reconsider the way this will work... The old system froze the game untill a choice was made.
        // Maybe we can just loop draw and update only this choice state.
        //if (choiceState.GetCurrentChoice() == 0) {
        //    SetConditionMet(true);
        //}
    }

    /// <summary>
    /// Getter for the window art id.
    /// </summary>
    /// <returns>Returns the window art ID of the choice box.</returns>
    public int GetWindowArtId() {
        return windowArtId;
    }

    /// <summary>
    /// Getter for the font id.
    /// </summary>
    /// <returns>Returns the font ID of the choice box.</returns>
    public int GetFontId() {
        return fontId;
    }

    /// <summary>
    /// Getter for the windows x position.
    /// </summary>
    /// <returns>Returns the windows x position.</returns>
    public int GetWindowXPosition() {
        return xPosition;
    }

    /// <summary>
    /// Getter for the windows y position.
    /// </summary>
    /// <returns>Returns the windows y position.</returns>
    public int GetWindowYPosition() {
        return yPosition;
    }

    /// <summary>
    /// Getter for the choices of the choice box.
    /// </summary>
    /// <returns>The array of different choices.</returns>
    public string[] GetChoices() {
        return choices;
    }

    private readonly int windowArtId, fontId, xPosition, yPosition;
    private readonly string[] choices;
}