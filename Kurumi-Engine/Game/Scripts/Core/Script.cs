// Game.
using Game.Scripts.Base;
using Game.Scripts.Context.Core;
using Game.Scripts.Serialization;
using Game.Scripts.Services;

namespace Game.Scripts.Core;

/// <summary>
/// Contains a list of script steps that can be conditionals or nodes.
/// </summary>
public sealed class Script 
{
    private readonly Dictionary<string, ScriptStep> _scriptSteps = [];

    public string StartingKey { get; }
    public string CurrentKey { get; private set; }

    internal Script(ScriptData scriptData, ScriptDataConverter scriptDataConverter) 
    {
        foreach (var keyPair in scriptData.Steps)
        {
            _scriptSteps.Add(keyPair.Key, scriptDataConverter.Convert(keyPair.Value));
        }
        StartingKey = scriptData.FirstStep;
        CurrentKey = StartingKey;
    }

    /// <summary>
    /// Used to activate a specific step and activate it.
    /// </summary>
    /// <param name="scriptContext">The script context required when activating scripts.</param>
    /// <param name="stepKey">The key of the desired script step.</param>
    public void Activate(ScriptContext scriptContext, string stepKey) 
    {
        CurrentKey = stepKey;
        if (stepKey != null) {
            if (_scriptSteps.TryGetValue(CurrentKey, out ScriptStep? currentStep))
            {
                currentStep.Activate(scriptContext);
            }
        }
    }

    public string? GetNextKey() { 
        if (CurrentKey != null && _scriptSteps.TryGetValue(CurrentKey, out ScriptStep? currentStep))
        {
            return currentStep.GetNextStep();
        }
        return null;
    }

    public bool IsWaiting() { 
        if (CurrentKey != null && _scriptSteps.TryGetValue(CurrentKey, out ScriptStep? currentStep))
        {
            return currentStep.Waiting();
        }
        return false; 
    }

    /// <summary>
    /// Function used to check if a potential next key exists in cases where a conditional script ends one branch.
    /// </summary>
    /// <returns>If a potential next key exists.</returns>
    public bool PotentialNextKeyExists()
    {
        if (CurrentKey != null && 
            _scriptSteps.TryGetValue(CurrentKey, out ScriptStep? currentStep) &&
            currentStep is ConditionalScriptStep conditionalScriptStep)
        {
            if (conditionalScriptStep.NextIfFalse != null)
            {
                return true;
            }
        }
        return GetNextKey() != null;
    }
}