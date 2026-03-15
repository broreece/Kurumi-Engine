namespace Utils.Interfaces;

using Scripts.Base;

/// <summary>
/// The next script step accessor.
/// </summary>
public interface INextScriptStepAccessor {
    /// <summary>
    /// Getter for the next script step.
    /// </summary>
    /// <returns>next script step in the script.</returns>
    /// <exception cref="ConditionalException">Error thrown if a scriptConditional value was not created.</exception>
    public ScriptStep ? GetNextStep();
}