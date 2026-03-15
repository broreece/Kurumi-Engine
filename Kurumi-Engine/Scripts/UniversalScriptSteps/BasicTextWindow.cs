namespace Scripts.UniversalScriptSteps;

using Config.Runtime.Defaults;
using Scripts.Base;
using UI.States.Modal.Modals;
using Utils.Strings;

/// <summary>
/// The basic text window script step.
/// </summary>
public sealed class BasicTextWindow : ScriptStep {
    /// <summary>
    /// Constructor for the basic text window script step.
    /// </summary>
    /// <param name="text">The text that is displayed.</param>
    public BasicTextWindow(string text) {
        this.text = text;
    }

    /// <summary>
    /// Activates the basic text window scene script by creating and initiating a text box.
    /// </summary>
    /// <param name="scriptContext">The context of the script.</param>
    public override void Activate(ScriptContext scriptContext) {
        // Load default text window values.
        SceneScriptContext sceneScriptContext = (SceneScriptContext) scriptContext;
        TextWindowDefaults textWindowDefaults = sceneScriptContext.GetTextWindowDefaults();
        int xPosition = textWindowDefaults.GetWindowX();
        int yPosition = textWindowDefaults.GetWindowY();
        int windowId = textWindowDefaults.GetWindowId();
        int fontId = textWindowDefaults.GetFontId();
        string windowFileName = sceneScriptContext.GetWindowArtFileName(windowId);
        string fontFileName = sceneScriptContext.GetFontFileName(fontId);

        // Pause the script step and the game to be continued after the dialogue state closes.
        Pause();
        sceneScriptContext.Pause();

        // Create the new dialogue UI state.
        sceneScriptContext.AddUIState(new DialogueState(xPosition, yPosition, textWindowDefaults.GetWindowWidth(), 
            textWindowDefaults.GetWindowHeight(), windowFileName, sceneScriptContext.GetWindowConfig(),
            sceneScriptContext.GetGameWindow(), xPosition, yPosition,  textWindowDefaults.GetFontSize(), fontFileName, 
            PageGenerator.TurnTextIntoPages(text, sceneScriptContext.GetMaxLinesPerPage()), this, sceneScriptContext));
    }

    /// <summary>
    /// Getter for the pages of text that the basic text window displays.
    /// </summary>
    /// <returns>The array of different text pages.</returns>
    public string GetText() {
        return text;
    }

    private readonly string text;
}
