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
    private readonly ScriptData _scriptData;
    private readonly ScriptDataConverter _scriptDataConverter;

    private string? _currentKey;

    internal Script(ScriptData scriptData, ScriptDataConverter scriptDataConverter) 
    {
        _scriptData = scriptData;
        _scriptDataConverter = scriptDataConverter;
        _currentKey = scriptData.FirstStep;
    }

    /// <summary>
    /// Used to navigate the script steps activating in logical order.
    /// </summary>
    /// <param name="scriptContext">The script context required when activating scripts.</param>
    public void Activate(ScriptContext scriptContext) 
    {
        while (_currentKey != null) 
        {
            ScriptStep currentStep = _scriptDataConverter.Convert(_scriptData.Steps[_currentKey]);
            currentStep.Activate(scriptContext);
            _currentKey = currentStep.GetNextStep();
        }
    }
}