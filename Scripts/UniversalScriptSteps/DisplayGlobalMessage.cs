namespace Scripts.UniversalScriptSteps;

using Config.Runtime.Defaults;
using Scripts.Base;
using UI.States.Modal.Modals;
using Utils.Strings;

/// <summary>
/// The add status to party script step.
/// </summary>
public sealed class DisplayGlobalMessage : ScriptStep {
    /// <summary>
    /// Constructor for the display global message script step.
    /// </summary>
    /// <param name="timeLimit">How long the message will be displayed for.</param>
    /// <param name="message">What text will be displayed.</param>
    public DisplayGlobalMessage(int timeLimit, string message) {
        this.timeLimit = timeLimit;
        this.message = message;
    }

    /// <summary>
    /// Activator function, opens the global message window with the passed text for the specified time frame.
    /// </summary>
    /// <param name="scriptContext">The context of the script.</param>
    public override void Activate(ScriptContext scriptContext) {
        // Load default global message values.
        GlobalMessageDefaults globalMessageDefaults = scriptContext.GetGlobalMessageDefaults();
        int xLocation = globalMessageDefaults.GetGlobalMessageX();
        int yLocation = globalMessageDefaults.GetGlobalMessageY();
        int windowId = globalMessageDefaults.GetGlobalMessageWindowId();
        int fontId = globalMessageDefaults.GetGlobalMessageFontId();
        string windowFileName = scriptContext.GetWindowArtFileName(windowId);
        string fontFileName = scriptContext.GetFontFileName(fontId);

        // Create the new global message UI state.
        scriptContext.AddUIState(new GlobalMessageState(xLocation, yLocation, globalMessageDefaults.GetGlobalMessageWidth(), 
            globalMessageDefaults.GetGlobalMessageHeight(), timeLimit, windowFileName, scriptContext.GetWindowConfig(), 
            scriptContext.GetGameWindow(), xLocation, yLocation, globalMessageDefaults.GetGlobalMessageFontSize(), fontFileName, 
            PageGenerator.TurnTextIntoPages(message, scriptContext.GetMaxLinesPerPage())));
    }

    /// <summary>
    /// Getter for the global message time limit.
    /// </summary>
    /// <returns>The global message time limit.</returns>
    public int GetTimeLimit() {
        return timeLimit;
    }

    /// <summary>
    /// Getter for the global message text.
    /// </summary>
    /// <returns>The global message text.</returns>
    public string GetMessage() {
        return message;
    }

    private readonly int timeLimit;
    private readonly string message;
}
