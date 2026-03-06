namespace Scripts.UniversalScriptSteps;

using Scripts.Base;

/// <summary>
/// The basic text window script step.
/// </summary>
public sealed class BasicTextWindow : ScriptStep {
    /// <summary>
    /// Constructor for the basic text window script step.
    /// </summary>
    /// <param name="artId">The art ID of the window.</param>
    /// <param name="fontId">The font ID of the window's text.</param>
    /// <param name="xPosition">The x position of the window.</param>
    /// <param name="yPosition">The y position of the window.</param> 
    /// <param name="text">The text that is displayed.</param>
    public BasicTextWindow(int windowArtId, int fontId, int xPosition, int yPosition, string text) {
        this.windowArtId = windowArtId;
        this.fontId = fontId;
        this.xPosition = xPosition;
        this.yPosition = yPosition;
        this.text = text;
    }

    /// <summary>
    /// Activates the basic text window scene script by creating and initiating a text box.
    /// </summary>
    /// <param name="scriptContext">The context of the script.</param>
    public override void Activate(ScriptContext scriptContext) {
        //DialogueState dialogueState = new(xPosition, yPosition, width, height, windowFileName, windowConfig,
        //    gameWindow, xPosition, yPosition, fontSize, fontFileName, text);
        // TODO: Push this state onto stack.
    }

    /// <summary>
    /// Getter for the windows art id.
    /// </summary>
    /// <returns>Returns the window art ID of the basic text window script.</returns>
    public int GetWindowArtId() {
        return windowArtId;
    }
    
    /// <summary>
    /// Getter for the font id.
    /// </summary>
    /// <returns>Returns the font ID of the basic text window script.</returns>
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
    /// Getter for the pages of text that the basic text window displays.
    /// </summary>
    /// <returns>The array of different text pages.</returns>
    public string GetText() {
        return text;
    }

    private readonly int windowArtId, fontId, xPosition, yPosition;
    private readonly string text;
}
