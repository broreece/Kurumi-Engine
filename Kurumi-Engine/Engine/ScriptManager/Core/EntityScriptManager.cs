namespace Engine.ScriptManager.Core;

using Engine.ScriptManager.Base;
using Engine.ScriptManager.Exceptions;
using Engine.ScriptManager.Serialization;
using Scripts.Base;
using Scripts.EntityScripts.Base;
using Scripts.EntityScripts.EntityScriptSteps;
using Utils.Exceptions;
using System.Text.Json;

/// <summary>
/// The entity script manager class, a type of script manager for entity scripts.
/// </summary>
public sealed class EntityScriptManager : ScriptManager {
    public EntityScriptManager(string registryPath) : base(registryPath) {}

    /// <summary>
    /// Deseralizes a specified script id and then returns that script object.
    /// </summary>
    /// <param name="index">The index of the specific script.</param>
    /// <exception cref="MissingJsonFileException">Error thrown if a .json data file is missing.</exception>
    /// <exception cref="ScriptStepException">Error thrown if a null script step is trying to be accessed.</exception>
    public EntityScript LoadEntityScript(int index) {
        // Load from passed file.
        ScriptData scriptData;
        try {
            string fullRegistryPath = Path.Combine(
                AppContext.BaseDirectory,
                scriptFileNames[index]
            );
            var json = File.ReadAllText(fullRegistryPath);
            scriptData = JsonSerializer.Deserialize<ScriptData>(json) ?? 
                throw new Exception();
        }
        catch (Exception) {
            throw new MissingJsonFileException($"Script file: {scriptFileNames[index]} could not be found or contains "
                + "an invalid format");
        }

        // Load base values.
        string scriptName = scriptData.Name;
        ScriptStep head = LoadScriptStep(scriptData.Steps[0]);
        ScriptStep? currentStep = head;

        // Loop over each step.
        int size = scriptData.Steps.Count;
        for (int stepIndex = 1; stepIndex < size; stepIndex ++) {
            if (currentStep != null) {
                currentStep.SetNext(LoadScriptStep(scriptData.Steps[stepIndex]));
                currentStep = currentStep.GetNextStep();
            }
            else {
                throw new ScriptStepException($"Null script step found at index: {stepIndex} for script: {scriptName}");
            }
        }
        return new EntityScript(scriptName, head);
    }

    /// <summary>
    /// Function used to extract a script step from script step data.
    /// </summary>
    /// <param name="scriptStepData">The script step data being checked.</param>
    /// <returns>A valid script step object.</returns>
    /// <exception cref="ScriptStepException">Exception thrown when making a script step if an error occurs.</exception>
    private ScriptStep LoadScriptStep(ScriptStepData scriptStepData) {
        Dictionary<string, JsonElement> parameters = scriptStepData.Parameters;
        switch (scriptStepData.Type) {
            case "ChangeHP":
                return new ChangeHp(parameters["ReduceHP"].GetInt32() == 1, parameters["CanKO"].GetInt32() == 1, 
                    parameters["Formula"].GetString() ?? throw new ScriptStepException("Change HP 'formula' parameter " 
                    + "not found."));

            default:
                throw new ScriptStepException($"Script step: {scriptStepData.Type} does not exist");
        }
    }
}