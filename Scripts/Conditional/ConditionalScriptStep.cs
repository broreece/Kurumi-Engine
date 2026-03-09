namespace Scripts.Conditional;

using Scripts.Base;

/// <summary>
/// Class for the script conditional, contains a condition and branches script steps.
/// If nextIsFalse is set to 0 it equates to the script ending if the condition is not met.
/// Otherwise next is false skips indexes aka if set to 2 it'd skip the first step in front of it.
/// </summary>
public abstract class ConditionalScriptStep : ScriptStep {
    /// <summary>
    /// Constructor for the script conditional.
    /// </summary>
    /// <param name="nextIfFalse">The next index of the script step to be executed if condition isn't met.</param>
    protected ConditionalScriptStep(int nextIfFalse) {
        conditionMet = false;
        storedNextIfFalse = null;
        this.nextIfFalse = nextIfFalse;
    }

    /// <summary>
    /// Getter for the next script step.
    /// </summary>
    /// <returns>Next script step in the script.</returns>
    /// <exception cref="ConditionalException">Error thrown if a scriptConditional value was not created.</exception>
    public override ScriptStep ? GetNextStep() {
        int nextStep = nextIfFalse;
        ScriptStep ? currentStep = next;
        // If condition is met set the final point to the next if false index.
        if (conditionMet) {
            if (nextStep == 0) {
                if (currentStep != null) {
                    return currentStep;
                }
                else {
                    throw new ConditionalException("ScriptStep, condition has been met but the current index has been corrupted.");
                }
            }
            for (int currentStepIndex = 0; currentStepIndex < nextStep - 2; currentStepIndex++) {
                if (currentStep != null) {
                    currentStep = currentStep.GetNextStep();
                } 
                else {
                    throw new ConditionalException("ScriptStep, condition has been met but the index for false is out of bounds.");
                }
            }
            if (currentStep == null) {
                throw new ConditionalException("ScriptStep, condition has been met but the index for false is out of bounds.");
            }
            // We store the next is false only once.
            storedNextIfFalse ??= currentStep.GetNextStep();
            currentStep.SetNext(null);
        }
        // If condition isn't met jump to the next if false index.
        else {
            if (nextStep == 0) {
                return null;
            }
            // If condition has been set as true before we use the stored false index.
            else if (storedNextIfFalse != null) {
                return storedNextIfFalse;
            }
            for (int currentStepIndex = 0; currentStepIndex < nextStep - 1; currentStepIndex ++) {
                if (currentStep == null) {
                    throw new ConditionalException("ScriptStep, Index is out of range if condition isn't met.");
                }
                currentStep = currentStep.GetNextStep();
            }
            return currentStep;
        }
        return next;
    }

    /// <summary>
    /// Setter the condition to be met.
    /// </summary>
    /// <param name="conditionMet">If the condition was met or not.</param>
    public void SetConditionMet(bool conditionMet) {
        this.conditionMet = conditionMet;
    }

    /// <summary>
    /// Setter for the next script step index if condition is not met.
    /// </summary>
    /// <param name="newNext">The new next script step index if condition is not met.</param>
    public void SetNextIfFalse(int newNext) {
        nextIfFalse = newNext;
    }

    protected bool conditionMet;
    protected int nextIfFalse;
    protected ScriptStep ? storedNextIfFalse;
}