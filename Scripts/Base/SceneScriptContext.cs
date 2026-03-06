namespace Scripts.Base;

using Engine.Runtime;
using Scenes.Base;

/// <summary>
/// Scene script context class, instance of script context that also loads and stores the scene controller..
/// </summary>
public abstract class SceneScriptContext : ScriptContext {
    /// <summary>
    /// Constructor for the scene script context class.
    /// </summary>
    /// <param name="gameContext">The game's context object.</param>
    protected SceneScriptContext(GameContext gameContext) : base(gameContext) {
        scene = gameContext.GetCurrentScene() ?? throw new Exception();
    }

    protected readonly SceneBase scene;
}