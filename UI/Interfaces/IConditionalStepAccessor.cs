namespace UI.Interfaces;

using Utils.Interfaces;

/// <summary>
/// The conditional script step accessor.
/// </summary>
public interface IConditionalStepAccessor : INextScriptStepAccessor {
    /// <summary>
    /// Setter the condition to be met.
    /// </summary>
    /// <param name="conditionMet">If the condition was met or not.</param>
    public void SetConditionMet(bool conditionMet);
}