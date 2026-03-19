namespace Scripts.BattleScripts.Base;

using Engine.Runtime;
using Scripts.Base;

/// <summary>
/// The Battle scene script class, a type of script that can activate on the battle scene.
/// </summary>
public class BattleScript : SceneScript {
    /// <summary>
    /// Constructor for the Battle scene script.
    /// </summary>
    /// <param name="name">The name of the script.</param>
    /// <param name="head">The head of the script.</param>
    public BattleScript(string name, ScriptStep head) : base(name, head) {}
    
    /// <summary>
    /// Activates the map scene script.
    /// </summary>
    /// <param name="gameContext">The context of the game used by script steps.</param>
    public void Activate(GameContext gameContext) {
        gameContext.SetCurrentScriptName(name);
        BattleScriptContext scriptContext = new(gameContext, this);
        while (scriptStep != null) {
            scriptStep.Activate(scriptContext);
            scriptStep = scriptStep.GetNextStep();
        }
        scriptStep = head;
        gameContext.FinishScriptExecution();
    }
}