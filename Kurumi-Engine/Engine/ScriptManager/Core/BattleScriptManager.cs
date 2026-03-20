namespace Engine.ScriptManager.Core;

using Engine.ScriptManager.Base;
using Engine.ScriptManager.Exceptions;
using Engine.ScriptManager.Serialization;
using Scripts.Base;
using Scripts.BattleScripts.Base;
using Scripts.BattleScripts.BattleScriptSteps;
using Scripts.UniversalScriptSteps;
using Utils.Exceptions;
using System.Text.Json;

/// <summary>
/// The battle script manager class, a type of script manager for battle scripts.
/// </summary>
public sealed class BattleScriptManager : ScriptManager {
    public BattleScriptManager(string registryPath) : base(registryPath) {}

    /// <summary>
    /// Deseralizes a specified script id and then returns that script object.
    /// </summary>
    /// <param name="index">The index of the specific script.</param>
    /// <exception cref="MissingJsonFileException">Error thrown if a .json data file is missing.</exception>
    /// <exception cref="ScriptStepException">Error thrown if a null script step is trying to be accessed.</exception>
    public BattleScript LoadBattleScript(int index) {
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
            throw new MissingJsonFileException($"Script file: {scriptFileNames[index]} could not be found or contains an invalid format");
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
        return new BattleScript(scriptName, head);
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
            case "KillEnemy":
                return new KillEnemy(parameters["EnemyID"].GetInt32());

            case "UseAbility":
                return new UseAbility(parameters["AbilityID"].GetInt32());

            case "DisplayGlobalMessage":
                return new DisplayGlobalMessage(parameters["TimeLimit"].GetInt32(), parameters["Message"].GetString() 
                    ?? throw new ScriptStepException("Display Global Message 'TimeLimit' parameter not found."));

            default:
                throw new ScriptStepException($"Script step: {scriptStepData.Type} does not exist");
        }
    }
}