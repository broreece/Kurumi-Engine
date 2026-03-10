namespace Scripts.UniversalScriptSteps;

using Config.Runtime.Defaults;
using Scripts.Base;
using Scripts.Conditional;
using UI.States.Modal.Modals;

/// <summary>
/// The choice box script step.
/// </summary>
public sealed class ChoiceBox : ConditionalScriptStep {
    /// <summary>
    /// Constructor for the choice box script step.
    /// </summary>
    /// <param name="choices">The choices that are displayed.</param>
    /// <param name="nextIfFalse">The index of the next script step if negative choice is made.</param>
    public ChoiceBox(string[] choices, int nextIfFalse) : base(nextIfFalse) {
        this.choices = choices;
    }
    
    /// <summary>
    /// Activates the choice box scene script by creating and initiating a choice box.
    /// </summary>
    /// <param name="scriptContext">The context of the script.</param>
    public override void Activate(ScriptContext scriptContext) {
        // Load default text window values.
        SceneScriptContext sceneScriptContext = (SceneScriptContext) scriptContext;
        ChoiceBoxDefaults choiceBoxDefaults = sceneScriptContext.GetChoiceBoxDefaults();
        int xPosition = choiceBoxDefaults.GetChoiceBoxX();
        int yPosition = choiceBoxDefaults.GetChoiceBoxY();
        int windowWidth = choiceBoxDefaults.GetChoiceBoxWidth();
        int windowHeight = choiceBoxDefaults.GetChoiceBoxHeight();
        int choiceBoxHeight = windowHeight / choices.Length;
        int windowId = choiceBoxDefaults.GetWindowId();
        int selectionId = choiceBoxDefaults.GetSelectionArtId();
        int fontId = choiceBoxDefaults.GetFontId();
        int fontSize = choiceBoxDefaults.GetFontSize();
        // TODO: (CBPI-01) Temporary spacing value.
        int spacing = fontSize;
        string windowFileName = sceneScriptContext.GetWindowArtFileName(windowId);
        string fontFileName = sceneScriptContext.GetFontFileName(fontId);
        string choiceBoxFileName = sceneScriptContext.GetSelectionBoxFileName(selectionId);


        // Pause the script step to be continued after the dialogue state closes.
        Pause();

        // Create the new choice box UI state.
        sceneScriptContext.AddUIState(new ChoiceState(xPosition, yPosition, windowWidth, windowHeight, fontSize, fontFileName, windowFileName,
            sceneScriptContext.GetWindowConfig(), sceneScriptContext.GetGameWindow(), xPosition, yPosition, windowWidth, choiceBoxHeight, spacing, 
            choiceBoxFileName, choices, this, sceneScriptContext));
    }

    /// <summary>
    /// Getter for the choices of the choice box.
    /// </summary>
    /// <returns>The array of different choices.</returns>
    public string[] GetChoices() {
        return choices;
    }

    private readonly string[] choices;
}