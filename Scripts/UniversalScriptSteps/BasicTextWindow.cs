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
        TextWindowDefaults textWindowDefaults = scriptContext.GetTextWindowDefaults();
        int xPosition = textWindowDefaults.GetTextWindowX();
        int yPosition = textWindowDefaults.GetTextWindowY();
        int windowId = textWindowDefaults.GetTextWindowArtId();
        int fontId = textWindowDefaults.GetTextWindowFontId();
        string windowFileName = scriptContext.GetWindowArtFileName(windowId);
        string fontFileName = scriptContext.GetFontFileName(fontId);

        // Create the new dialogue UI state.
        scriptContext.AddUIState(new DialogueState(xPosition, yPosition, textWindowDefaults.GetTextWindowWidth(), 
            textWindowDefaults.GetTextWindowHeight(), windowFileName, scriptContext.GetWindowConfig(),
            scriptContext.GetGameWindow(), xPosition, yPosition,  textWindowDefaults.GetTextWindowFontSize(), fontFileName, 
            PageGenerator.TurnTextIntoPages(text, scriptContext.GetMaxLinesPerPage())));
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
