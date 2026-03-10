namespace Scripts.UniversalScriptSteps;

using Scripts.Base;

/// <summary>
/// The window with text and namebox scene script step.
/// </summary>
public sealed class ShowWindowWithTextAndNamebox : ScriptStep {
    /// <summary>
    /// Constructor for the window with text and name box script step.
    /// </summary>
    /// <param name="windowArtId">The window art id used by text and namebox.</param>
    /// <param name="fontId">The font id used by text and namebox.</param>
    /// <param name="text">The text displayed on the text window.</param>
    /// <param name="name">The name displayed on the name window.</param>
    public ShowWindowWithTextAndNamebox(int windowArtId, int fontId, string text, string name) {
        this.windowArtId = windowArtId;
        this.fontId = fontId;
        this.text = text;
        this.name = name;
    }

    /// <summary>
    /// Activator function for the window with text and namebox script step.
    /// </summary>
    /// <param name="scriptContext">The context of the script.</param>
    public override void Activate(ScriptContext scriptContext) {
        // TODO: (TWNUI-01) We have to implement a new UI modal for dialogue with a name box.
    }
    
    /// <summary>
    /// Getter for the windows art id.
    /// </summary>
    /// <returns>Returns the window art ID of the text window script.</returns>
    public int GetWindowArtId() {
        return windowArtId;
    }
    
    /// <summary>
    /// Getter for the font id.
    /// </summary>
    /// <returns>Returns the font ID of the text window script.</returns>
    public int GetFontId() {
        return fontId;
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
    
    private readonly int windowArtId, fontId;
    private readonly string text, name;
}