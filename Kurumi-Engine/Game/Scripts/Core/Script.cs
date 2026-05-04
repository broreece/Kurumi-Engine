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

    public bool Waiting => _scriptSteps[CurrentKey].Waiting();

    public string? NextKey => _scriptSteps[CurrentKey].GetNextStep();

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
        ScriptStep currentStep = _scriptSteps[CurrentKey];
        currentStep.Activate(scriptContext);
    }
}