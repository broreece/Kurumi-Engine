namespace States.Map.Core;

using States.Map.Interfaces;
using Utils.Interfaces;

/// <summary>
/// The scripted actor controller class, a storing data structure that contains an actor controller alongside a 
/// script being executed and a script step. Used for when scripts change actor controllers.
/// </summary>
public sealed class ScriptedActorController {
    /// <summary>
    /// Constructor for the scripted actor controller object.
    /// </summary>
    /// <param name="actorController">The actor controller stored as a finishable object.</param>
    /// <param name="continuableScript">The continuable script to be executed when finished.</param>
    /// <param name="nextScriptStepAccessor">The next script step accessor.</param>
    public ScriptedActorController(IFinishable actorController, IContinuableScript continuableScript, 
        INextScriptStepAccessor nextScriptStepAccessor) {
        this.actorController = actorController;
        this.continuableScript = continuableScript;
        this.nextScriptStepAccessor = nextScriptStepAccessor;
    }

    /// <summary>
    /// Function used to continue the script.
    /// </summary>
    public void Continue() {
        continuableScript.ContinueScript(nextScriptStepAccessor);
    }

    /// <summary>
    /// Function used to check if the actor controller has finished it's movements.
    /// </summary>
    /// <returns>If the actor controller has finished.</returns>
    public bool IsFinished() {
        return actorController.IsFinished();
    }

    private readonly IFinishable actorController;
    private readonly IContinuableScript continuableScript;
    private readonly INextScriptStepAccessor nextScriptStepAccessor;
}