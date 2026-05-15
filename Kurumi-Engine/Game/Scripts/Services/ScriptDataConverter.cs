using System.Text.Json;

using Game.Scripts.Exceptions;
using Game.Scripts.Base;
using Game.Scripts.Serialization;
using Game.Scripts.Steps.Battle;
using Game.Scripts.Steps.Entity;
using Game.Scripts.Steps.Map;
using Game.Scripts.Steps.Universal;

namespace Game.Scripts.Services;

/// <summary>
/// A service that converts the script data objects into script runtime objects.
/// </summary>
public sealed class ScriptDataConverter 
{
    /// <summary>
    /// Function used to convert the raw script step data into a runtime script step object.
    /// </summary>
    /// <param name="scriptStepData">The raw script step data to be converted.</param>
    /// <returns>A new script step using the script data.</returns>
    /// <exception cref="ScriptStepException">Error thrown if a string in the script step's parameters is not 
    /// set.</exception>
    public ScriptStep Convert(ScriptStepData scriptStepData) 
    {
        string? nextStep = scriptStepData.Next;
        Dictionary<string, JsonElement> parameters = scriptStepData.Parameters;
        switch (scriptStepData.Type) 
        {
            // Battle script steps:
            case "KillEnemy":
                return new KillEnemy(parameters["EnemyIndex"].GetInt32()) { NextStep = nextStep};

            // Entity script steps:
            case "ChangeHP":
                return new ChangeHp(parameters["ReduceHP"].GetInt32() == 1, 
                    parameters["CanKO"].GetInt32() == 1, 
                    parameters["Formula"].GetString() ?? throw new ScriptStepException("Change HP 'formula' parameter " 
                        + "not found.")) { NextStep = nextStep};

            // Map script steps:
            case "ChangeMap":
                string mapName = parameters["MapName"].GetString()
                    ?? throw new ScriptStepException("Change map 'MapName' parameter not found.");
                return new ChangeMap(mapName, parameters["XLocation"].GetInt32(), parameters["YLocation"].GetInt32())
                    { NextStep = nextStep};

            case "ForceMoveActor":
                bool forceMoveActorKeepDirection = parameters["KeepDirection"].GetInt32() == 1;
                bool forceMoveActorLockMovement = parameters["LockMovement"].GetInt32() == 1;
                bool forceMoveActorInstant = parameters["Instant"].GetInt32() == 1;
                string forceMoveActorKey = parameters["ActorKey"].GetString()
                    ?? throw new ScriptStepException("Force move actor 'ActorKey' parameter not found.");
                JsonElement forceMoveActorStepsElement = parameters["Steps"];
                var forceMoveActorSteps = new List<int>();
                foreach (var item in forceMoveActorStepsElement.EnumerateArray()) {
                    forceMoveActorSteps.Add(item.GetInt32());
                }
                return new ForceMoveActor(
                    forceMoveActorKeepDirection, 
                    forceMoveActorLockMovement, 
                    forceMoveActorInstant, 
                    forceMoveActorKey, 
                    forceMoveActorSteps
                ) { NextStep = nextStep };

            case "ForceMoveParty":
                bool forceMovePartyKeepDirection = parameters["KeepDirection"].GetInt32() == 1;
                bool forceMovePartyInstant = parameters["Instant"].GetInt32() == 1;
                JsonElement forceMovePartyStepsElement = parameters["Steps"];
                var forceMovePartySteps = new List<int>();
                foreach (var item in forceMovePartyStepsElement.EnumerateArray()) {
                    forceMovePartySteps.Add(item.GetInt32());
                }
                return new ForceMoveParty(
                    forceMovePartyKeepDirection, 
                    forceMovePartyInstant, 
                    forceMovePartySteps
                ) { NextStep = nextStep };

            case "StartBattle":
                string backgroundMusicName = parameters["BackgroundMusicName"].GetString()
                    ?? throw new ScriptStepException("Start battle 'BackgroundMusicName' parameter not found.");
                string backgroundArtName = parameters["BackgroundArtName"].GetString()
                    ?? throw new ScriptStepException("Start battle 'BackgroundArtName' parameter not found.");
                return new StartBattle(
                    backgroundMusicName, 
                    backgroundArtName, 
                    parameters["EnemyFormationID"].GetInt32()
                ) { NextStep = nextStep};

            // Universal steps:
            case "BasicTextWindow":
                JsonElement basicTextWindowPagesElement = parameters["Pages"];
                var basicTextWindowPages = new List<string>();
                foreach (var item in basicTextWindowPagesElement.EnumerateArray()) 
                {
                    basicTextWindowPages.Add(item.GetString() 
                    ?? throw new ScriptStepException("Basic text window 'Pages' parameter not found."));
                }
                return new BasicTextWindow(basicTextWindowPages) { NextStep = nextStep };

            case "ChoiceBoxWithText":
                string choiceBoxWithTextNextIfFalse = parameters["NextIfFalse"].GetString()
                    ?? throw new ScriptStepException("Choice box with text 'NextIfFalse' parameter not found.");

                string choiceBoxWithTextText = parameters["Text"].GetString()
                    ?? throw new ScriptStepException("Choice box with text 'Text' parameter not found.");

                JsonElement choiceBoxWithTextChoicesElement = parameters["Choices"];
                var choiceBoxWithTextChoices = new List<string>();
                foreach (var item in choiceBoxWithTextChoicesElement.EnumerateArray())
                {
                    choiceBoxWithTextChoices.Add(item.GetString()
                    ?? throw new ScriptStepException("choice box with text 'Choices' parameter not found."));
                }
                return new ChoiceBoxWithText(
                    choiceBoxWithTextChoices, 
                    choiceBoxWithTextText, 
                    choiceBoxWithTextNextIfFalse
                ) { NextStep = nextStep };

            case "DisplayGlobalMessage":
                return new DisplayGlobalMessage(parameters["TimeLimit"].GetInt32(), parameters["Text"].GetString() 
                    ?? throw new ScriptStepException("Display Global Message 'Text' parameter not found."))
                    { NextStep = nextStep};

            default:
                throw new ScriptStepException($"Script step: {scriptStepData.Type} does not exist");
        }
    }
}