namespace Scripts.Base;

using Engine.Runtime;

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
    /// Function that returns a flag value within the game variables object.
    /// </summary>
    /// <param name="flagIndex">The flag index.</param>
    /// <returns>The specified flags state.</returns>
    public bool GetGameFlag(int flagIndex) {
        // TODO: Yets use one function in game context here.
        return gameContext.GetGameVariables().GetGameFlag(flagIndex);
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