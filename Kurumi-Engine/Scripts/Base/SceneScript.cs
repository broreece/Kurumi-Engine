namespace Scripts.Base;

/// <summary>
/// Abstract scene script class, a type of script that can be paused and contains a pointer to the current step.
/// </summary>
public abstract class SceneScript : Script {
    /// <summary>
    /// The constructor for scene scripts.
    /// </summary>
    /// <param name="name">The name of the script.</param>
    /// <param name="head">The head of the script.</param>
    protected SceneScript(string name, ScriptStep head) : base(name, head) {
        scriptStep = head;
    }

    /// <summary>
    /// Getter for the map scene scripts current script step.
    /// </summary>
    /// <returns>The current script step.</returns>
    public ScriptStep ? GetSceneScriptStep() {
        return scriptStep;
    }

    /// <summary>
    /// Setter for the map scene scripts current script step.
    /// </summary>
    /// <param name="scriptStep">The currently executing script step.</param>
    public void SetScriptStep(ScriptStep? scriptStep) {
        this.scriptStep = scriptStep;
    } 

    protected ScriptStep ? scriptStep;
}