namespace Scripts.Base;

using Utils.Interfaces;

/// <summary>
/// Abstract script step class, scripts are lists of scripts steps.
/// </summary>
public abstract class ScriptStep : INextScriptStepAccessor {
    /// <summary>
    /// Constructor for the script step class.
    /// </summary>
    protected ScriptStep() {
        next = null;
        paused = false;
    }

    /// <summary>
    /// The activator function for the script step, requiring the specific context for the type of step.
    /// </summary>
    /// <param name="scriptContext">The context which is required by the script step to activate.</param>
    public abstract void Activate(ScriptContext scriptContext);

    /// <summary>
    /// Function that pauses the script execution.
    /// </summary>
    public void Pause() {
        paused = true;
    }

    /// <summary>
    /// Getter for if the script step is paused.
    /// </summary>
    /// <returns>If the script step is paused.</returns>
    public bool IsPaused() {
        return paused;
    }

    /// <summary>
    /// Getter for the next script step.
    /// </summary>
    /// <returns>next script step in the script.</returns>
    /// <exception cref="ConditionalException">Error thrown if a scriptConditional value was not created.</exception>
    public virtual ScriptStep ? GetNextStep() {
        return next;
    }

    /// <summary>
    /// Setter for the next script step in the script.
    /// </summary>
    /// <param name="newNext">The new next script step.</param>
    public void SetNext(ScriptStep ? newNext) {
        next = newNext;
    }

    protected ScriptStep ? next;
    private bool paused;
}