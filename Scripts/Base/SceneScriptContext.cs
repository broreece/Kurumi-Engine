namespace Scripts.Base;

using Config.Runtime.Defaults;
using Config.Runtime.Windows;
using Engine.Rendering;
using Engine.Runtime;
using Scenes.Base;
using Scripts.Exceptions;
using UI.Core;
using Utils.Interfaces;

/// <summary>
/// Scene script context class, instance of script context that also loads and stores the scene controller..
/// </summary>
public abstract class SceneScriptContext : ScriptContext, IContinuableScript {
    /// <summary>
    /// Constructor for the scene script context class.
    /// </summary>
    /// <param name="gameContext">The game's context object.</param>
    /// <exception cref="SceneNotSetException">Error thrown if no current scene is set in the game context.</exception>
    protected SceneScriptContext(GameContext gameContext, Script script) : base(gameContext) {
        scene = gameContext.GetCurrentScene() ?? throw new SceneNotSetException("The game contexts current scene is not found");
        this.script = script;
    }

    /// <summary>
    /// Function used to add a new UI state to the game UI list.
    /// </summary>
    /// <param name="uIState">The new UIState to be added to the list.</param>
    public void AddUIState(UIState uIState) {
        gameContext.AddUIState(uIState);
    }

    /// <summary>
    /// Function used to pause the game.
    /// </summary>
    public void Pause() {
        gameContext.Pause();
    }

    /// <summary>
    /// Function used to continue a script following from a previous script step.
    /// </summary>
    /// <param name="previousStep">The last executed scene step.</param>
    public abstract void ContinueScript(INextScriptStepAccessor previousStep);

    /// <summary>
    /// Function used to load the max number of lines in one page.
    /// </summary>
    /// <returns>The max number of lines in one page.</returns>
    public int GetMaxLinesPerPage() {
        return gameContext.GetMaxLinesPerPage();
    }

    /// <summary>
    /// Function used to load a specific font file name.
    /// </summary>
    /// <param name="fontId">The font ID.</param>
    /// <returns>The font file name of a specific ID.</returns>
    public string GetFontFileName(int fontId) {
        return gameContext.GetFontFileName(fontId);
    }

    /// <summary>
    /// Function used to load a specific selection box file name.
    /// </summary>
    /// <param name="selectionId">The selection box ID.</param>
    /// <returns>The selection box file name of a specific ID.</returns>
    public string GetSelectionBoxFileName(int selectionId) {
        return gameContext.GetSelectionBoxFileName(selectionId);
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
    /// Function used to load the choice box defaults object.
    /// </summary>
    /// <returns>The choice box defaults object.</returns>
    public ChoiceBoxDefaults GetChoiceBoxDefaults() {
        return gameContext.GetChoiceBoxDefaults();
    }

    /// <summary>
    /// Function used to load the global message defaults object.
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

    protected readonly SceneBase scene;
    protected readonly Script script;
}