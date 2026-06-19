namespace Game.Scripts.Base;

/// <summary>
/// Cntains a condition and a key of the script to step branch to if condition isn't met.
/// </summary>
public abstract class ConditionalScriptStep : ScriptStep 
{
    public string? NextIfFalse { get; protected init; }

    protected abstract bool IsConditionMet();

    /// <summary>
    /// Overrides the default script step get next step for conditionals, check if condition was met to return
    /// the correct path to follow.
    /// </summary>
    /// <returns>The next script step to execute.</returns>
    public override string? GetNextStep() 
    {
        if (IsConditionMet()) 
        {
            return base.GetNextStep();
        }
        return NextIfFalse;
    }
}