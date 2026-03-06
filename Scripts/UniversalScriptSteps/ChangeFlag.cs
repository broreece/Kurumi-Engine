namespace Scripts.UniversalScriptSteps;

using Scripts.Base;

/// <summary>
/// The basic change flag script step.
/// </summary>
public sealed class ChangeFlag : ScriptStep {
    /// <summary>
    /// The constructor for the change flag script step.
    /// </summary>
    /// <param name="flagIndex">The index of the flag in the flags array.</param>
    /// <param name="newValue">The new value that the index's flag will be set to.</param>
    public ChangeFlag(int flagIndex, bool newValue) {
        this.flagIndex = flagIndex;
        this.newValue = newValue;
    }

    /// <summary>
    /// The activator function for the change flag script step.
    /// </summary>
    /// <param name="scriptContext">The context of the script.</param>
    public override void Activate(ScriptContext scriptContext) {
        scriptContext.SetGameFlag(flagIndex, newValue);
    }
       
    /// <summary>
    /// Getter for the index of the flag in the flags array.
    /// </summary>
    /// <returns>The index of the flag array.</returns>
    public int GetFlagIndex() {
        return flagIndex;
    }
    
    /// <summary>
    /// Getter for the new value of the change flag script step.
    /// </summary>
    /// <returns>True: Flag will be set to true.</returns>
    public bool GetNewValue() {
        return newValue;
    }

    private readonly int flagIndex;
    private readonly bool newValue;
}