namespace Engine.ScriptManager.Core;

using Engine.ScriptManager.Base;
using Engine.ScriptManager.Exceptions;
using Engine.ScriptManager.Serialization;
using Scripts.Base;
using Scripts.MapScripts.Base;
using Scripts.MapScripts.MapScriptSteps;
using Scripts.UniversalScriptSteps;
using Utils.Exceptions;
using System.Text.Json;

/// <summary>
/// The map script manager class, a type of script manager for map scripts.
/// </summary>
public sealed class MapScriptManager : ScriptManager {
    public MapScriptManager(string registryPath) : base(registryPath) {}

    /// <summary>
    /// Deseralizes a specified script id and then returns that script object.
    /// </summary>
    /// <param name="index">The index of the specific script.</param>
    /// <exception cref="MissingJsonFileException">Error thrown if a .json data file is missing.</exception>
    /// <exception cref="ScriptStepException">Error thrown if a null script step is trying to be accessed.</exception>
    public MapScript LoadMapScript(int index) {
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
        return new MapScript(scriptName, head);
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
            case "BasicTextWindow":
                return new BasicTextWindow(parameters["Text"].GetString() 
                    ?? throw new ScriptStepException("Basic text window 'text' parameter not found."));

            case "ChangeMap":
                return new ChangeMap(parameters["MapID"].GetInt32(), parameters["XLocation"].GetInt32(), 
                    parameters["YLocation"].GetInt32());

            case "ForceMoveActor":
                bool forceMoveActorKeepDirection = parameters["KeepDirection"].GetInt32() == 1;
                bool forceMoveActorLockMovement = parameters["LockMovement"].GetInt32() == 1;
                bool forceMoveActorInstant = parameters["Instant"].GetInt32() == 1;
                int forceMoveActorX = parameters["ActorX"].GetInt32();
                int forceMoveActorY = parameters["ActorY"].GetInt32();
                JsonElement forceMoveActorStepsElement = parameters["Steps"];
                List<int> forceMoveActorSteps = [];
                foreach (var item in forceMoveActorStepsElement.EnumerateArray()) {
                    forceMoveActorSteps.Add(item.GetInt32());
                }
                return new ForceMoveActor(forceMoveActorKeepDirection, forceMoveActorLockMovement, 
                    forceMoveActorInstant, forceMoveActorX, forceMoveActorY, forceMoveActorSteps);

            case "StartBattle":
                return new StartBattle(parameters["BackgroundMusicID"].GetInt32(), 
                    parameters["BackgroundArtID"].GetInt32(), parameters["EnemyFormationID"].GetInt32());

            default:
                throw new ScriptStepException($"Script step: {scriptStepData.Type} does not exist");
        }
    }
}