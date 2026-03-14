namespace Scripts.UniversalScriptSteps;

using Config.Runtime.Defaults;
using Scripts.Base;
using UI.States.Modal.Modals;
using Utils.Strings;

/// <summary>
/// The window with text and namebox scene script step.
/// </summary>
public sealed class ShowWindowWithTextAndNamebox : ScriptStep {
    /// <summary>
    /// Constructor for the window with text and name box script step.
    /// </summary>
    /// <param name="text">The text displayed on the text window.</param>
    /// <param name="name">The name displayed on the name window.</param>
    public ShowWindowWithTextAndNamebox(string text, string name) {
        this.text = text;
        this.name = name;
    }

    /// <summary>
    /// Activator function for the window with text and namebox script step.
    /// </summary>
    /// <param name="scriptContext">The context of the script.</param>
    public override void Activate(ScriptContext scriptContext) {
        SceneScriptContext sceneScriptContext = (SceneScriptContext) scriptContext;
        // Load default text window values.
        TextWindowDefaults textWindowDefaults = sceneScriptContext.GetTextWindowDefaults();
        int xPosition = textWindowDefaults.GetWindowX();
        int yPosition = textWindowDefaults.GetWindowY();
        int width = textWindowDefaults.GetWindowWidth();
        int height = textWindowDefaults.GetWindowHeight();
        int windowId = textWindowDefaults.GetWindowId();
        int fontId = textWindowDefaults.GetFontId();
        string windowFileName = sceneScriptContext.GetWindowArtFileName(windowId);
        string fontFileName = sceneScriptContext.GetFontFileName(fontId);

        // Load default name box values.
        NameBoxDefaults nameBoxDefaults = sceneScriptContext.GetNameBoxDefaults();
        int nameBoxXPosition = nameBoxDefaults.GetNameBoxX();
        int nameBoxYPosition = nameBoxDefaults.GetNameBoxY();
        int nameBoxWidth = nameBoxDefaults.GetNameBoxWidth();
        int nameBoxHeight = nameBoxDefaults.GetNameBoxHeight();

        // Pause the script step and the game to be continued after the dialogue state closes.
        Pause();
        sceneScriptContext.Pause();

        // Create the new dialogue UI state.
        sceneScriptContext.AddUIState(new DialogueWithNameState(xPosition, yPosition, nameBoxXPosition, nameBoxYPosition, 
            width, height, nameBoxWidth, nameBoxHeight, windowFileName, sceneScriptContext.GetWindowConfig(), sceneScriptContext.GetGameWindow(), 
            xPosition, yPosition, nameBoxXPosition, nameBoxYPosition, textWindowDefaults.GetFontSize(), fontFileName, 
            PageGenerator.TurnTextIntoPages(text, sceneScriptContext.GetMaxLinesPerPage()), name, this, sceneScriptContext));
    }
    
    /// <summary>
    /// Getter for the pages of text that the text window displays.
    /// </summary>
    /// <returns>The array of different text pages.</returns>
    public string GetText() {
        return text;
    }
    
    /// <summary>
    /// Getter for the name of the name box (Only will use 1 page with 1 value).
    /// </summary>
    /// <returns>The name to be displayed.</returns>
    public string GetName() {
        return name;
    }
    
    private readonly string text, name;
}