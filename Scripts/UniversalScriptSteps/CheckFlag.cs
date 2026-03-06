namespace Scripts.UniversalScriptSteps;

using Scripts.Base;
using Scripts.Conditional;

/// <summary>
/// The basic check flag script step.
/// </summary>
public sealed class CheckFlag : ConditionalScript {
    /// <summary>
    /// The constructor for the check flag script step.
    /// </summary>
    /// <param name="flagIndex">The index of the flag in the flags array.</param>
    /// <param name="value">The value that the index's flag will be compared against.</param>
    /// <param name="nextIfFalse">The index of the next script step if flag's values do not match.</param>
    public CheckFlag(int flagIndex, bool value, int nextIfFalse) : base(nextIfFalse) {
        this.flagIndex = flagIndex;
        this.value = value;
    }
    
    /// <summary>
    /// The activator function for the check flag script step.
    /// </summary>
    /// <param name="scriptContext">The context of the script.</param>
    /// <exception cref="ConditionalException">Error thrown if a scriptConditional value was not created.</exception>
    public override void Activate(ScriptContext scriptContext) {
        SetConditionMet(scriptContext.GetGameFlag(flagIndex) == value);
    }
    
    /// <summary>
    /// Getter for the index of the flag in the flags array.
    /// </summary>
    /// <returns>The index of the flag array.</returns>
    public int GetFlagIndex() {
        return flagIndex;
    }
    
    /// <summary>
    /// Getter for the value of the check flag script step.
    /// </summary>
    /// <returns>True: Flag will be checked if it is true.</returns>
    public bool GetValue() {
        return value;
    }
    
    private readonly int flagIndex;
    private readonly bool value;
}