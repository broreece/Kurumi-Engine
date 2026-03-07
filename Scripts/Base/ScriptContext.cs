namespace Scripts.Base;

using Config.Runtime.Defaults;
using Config.Runtime.Windows;
using Engine.Rendering;
using Engine.Runtime;
using UI.Core;

/// <summary>
/// Script context class, contains parameters to be used in the activator function for each script sub class.
/// </summary>
public class ScriptContext {
    /// <summary>
    /// Constructor for the script context class.
    /// </summary>
    /// <param name="gameContext">The game's context object.</param>
    public ScriptContext(GameContext gameContext) {
        this.gameContext = gameContext;
    }

    /// <summary>
    /// Function that sets a game flag within the game variables object.
    /// </summary>
    /// <param name="flagIndex">The flag index.</param>
    /// <param name="newValue">The new value of the flag.</param>
    public void SetGameFlag(int flagIndex, bool newValue) {
        // TODO: Yets use one function in game context here.
        gameContext.GetGameVariables().SetGameFlag(flagIndex, newValue);
    }

    /// <summary>
    /// Function used to add a new UI state to the game UI stack.
    /// </summary>
    /// <param name="uIState">The new UIState to be added to the stack.</param>
    public void AddUIState(UIState uIState) {
        gameContext.AddUIState(uIState);
    }

    /// <summary>
    /// Function that returns a flag value within the game variables object.
    /// </summary>
    /// <param name="flagIndex">The flag index.</param>
    /// <returns>The specified flags state.</returns>
    public bool GetGameFlag(int flagIndex) {
        // TODO: Yets use one function in game context here.
        return gameContext.GetGameVariables().GetGameFlag(flagIndex);
    }

    /// <summary>
    /// Function used to load the max number of lines in one page.
    /// </summary>
    /// <returns>The max number of lines in one page.</returns>
    public int GetMaxLinesPerPage() {
        return gameContext.GetMaxLinesPerPage();
    }

    /// <summary>
    /// Function used to load a specific window file art name.
    /// </summary>
    /// <param name="windowArtId">The window art ID.</param>
    /// <returns>The window art file name of a specific ID.</returns>
    public string GetWindowArtFileName(int windowArtId) {
        return gameContext.GetWindowArtFileName(windowArtId);
    }

    /// <summary>
    ///Function used to load a specific font file name.
    /// </summary>
    /// <param name="fontId">The font ID.</param>
    /// <returns>The font file name of a specific ID.</returns>
    public string GetFontFileName(int fontId) {
        return gameContext.GetFontFileName(fontId);
    }

    /// <summary>
    /// Function used to load window config from the game context.
    /// </summary>
    /// <returns>The window config object.</returns>
    public WindowConfig GetWindowConfig() {
        return gameContext.GetWindowConfig();
    }

    /// <summary>
    /// Function used to load the game window object from the game context.
    /// </summary>
    /// <returns>The game window object.</returns>
    public GameWindow GetGameWindow() {
        return gameContext.GetGameWindow();
    }

    /// <summary>
    /// Function used to load the global message defaults from the game context.
    /// </summary>
    /// <returns>The global message defaults object.</returns>
    public GlobalMessageDefaults GetGlobalMessageDefaults() {
        return gameContext.GetGlobalMessageDefaults();
    }

    /// <summary>
    /// Getter for the text window defaults object.
    /// </summary>
    /// <returns>The text window defaults.</returns>
    public TextWindowDefaults GetTextWindowDefaults() {
        return gameContext.GetTextWindowDefaults();
    }

    // TODO: When we removed instances of this yets remove the function as it exposes too much.
    /// <summary>
    /// Getter for the game context object.
    /// </summary>
    /// <returns>The game context.</returns>
    public GameContext GetGameContext() {
        return gameContext;
    }
    
    protected readonly GameContext gameContext;
}