namespace Scripts.MapScripts.Base;

using Engine.Runtime;
using Scripts.Base;

/// <summary>
/// The map scene script class, a type of script that can activate on the map scene.
/// </summary>
public class MapScript : SceneScript {
    /// <summary>
    /// Constructor for the map scene script.
    /// </summary>
    /// <param name="name">The name of the script.</param>
    /// <param name="head">The head of the script.</param>
    public MapScript(string name, ScriptStep head) : base(name, head) {}
    
    /// <summary>
    /// Activates the map scene script.
    /// </summary>
    /// <param name="gameContext">The context of the game used by script steps.</param>
    public void Activate(GameContext gameContext) {
        gameContext.SetCurrentScriptName(name);
        MapScriptContext scriptContext = new(gameContext, this);
        while (scriptStep != null && !scriptStep.IsPaused()) {
            scriptStep.Activate(scriptContext);
            if (!scriptStep.IsPaused()) {
                scriptStep = scriptStep.GetNextStep();
            }
        }
        scriptStep = head;
        gameContext.FinishScriptExecution();
    }
}