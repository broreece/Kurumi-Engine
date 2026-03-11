namespace Scripts.BattleScripts.Base;

using Engine.Runtime;
using Scripts.Base;
using Scripts.BattleScripts.BattleScriptSteps;
using Scripts.UniversalScriptSteps;

/// <summary>
/// The Battle scene script class, a type of script that can activate on the battle scene.
/// </summary>
public class BattleScript : Script {
    /// <summary>
    /// Constructor for the Battle scene script.
    /// </summary>
    /// <param name="scriptInformation">The string representing each script step in the script.</param>
    public BattleScript(string scriptText) {
        while (scriptText.Contains(',')) {
            string scriptName = scriptText[..scriptText.IndexOf(',')];
            scriptText = scriptText[(scriptText.IndexOf(',') + 1)..];
            switch (scriptName) {
                case "ChangeFlag":
                    int flagId = int.Parse(scriptText[..scriptText.IndexOf(',')]);
                    scriptText = scriptText[(scriptText.IndexOf(',') + 1)..];
                    bool newValue = int.Parse(scriptText[..scriptText.IndexOf(',')]) == 1;
                    scriptText = scriptText[(scriptText.IndexOf(',') + 1)..];
                    AddStep(new ChangeFlag(flagId, newValue));
                    break;

                case "CheckFlag":
                    int checkedFlagId = int.Parse(scriptText[..scriptText.IndexOf(',')]);
                    scriptText = scriptText[(scriptText.IndexOf(',') + 1)..];
                    bool value = int.Parse(scriptText[..scriptText.IndexOf(',')]) == 1;
                    scriptText = scriptText[(scriptText.IndexOf(',') + 1)..];
                    int nextIfFalse = int.Parse(scriptText[..scriptText.IndexOf(',')]);
                    scriptText = scriptText[(scriptText.IndexOf(',') + 1)..];
                    AddStep(new CheckFlag(checkedFlagId, value, nextIfFalse));
                    break;

                case "ChoiceBox":
                    string fullChoices = scriptText[..scriptText.IndexOf(',')];
                    string[] choices = fullChoices.Split('-');
                    scriptText = scriptText[(scriptText.IndexOf(',') + 1)..];
                    int choiceBoxNextIfFalse = int.Parse(scriptText[..scriptText.IndexOf(',')]);
                    scriptText = scriptText[(scriptText.IndexOf(',') + 1)..];
                    AddStep(new ChoiceBox(choices, choiceBoxNextIfFalse));
                    break;

                case "DisplayGlobalMessage":
                    int globalMessageTimeLimit = int.Parse(scriptText[..scriptText.IndexOf(',')]);
                    scriptText = scriptText[(scriptText.IndexOf(',') + 1)..];
                    string globalMessageText = scriptText[..scriptText.IndexOf(',')];
                    scriptText = scriptText[(scriptText.IndexOf(',') + 1)..];
                    AddStep(new DisplayGlobalMessage(globalMessageTimeLimit, globalMessageText));
                    break;

                case "KillEnemy":
                    int enemyId = int.Parse(scriptText[..scriptText.IndexOf(',')]);
                    scriptText = scriptText[(scriptText.IndexOf(',') + 1)..];
                    AddStep(new KillEnemy(enemyId));
                    break;

                case "UseAbility":
                    int abilityId = int.Parse(scriptText[..scriptText.IndexOf(',')]);
                    scriptText = scriptText[(scriptText.IndexOf(',') + 1)..];
                    AddStep(new UseAbility(abilityId));
                    break;

                default:
                    break;
            }
        }
        scriptStep = head;
    }
    
    /// <summary>
    /// Activates the map scene script.
    /// </summary>
    /// <param name="gameContext">The context of the game used by script steps.</param>
    public void Activate(GameContext gameContext) {
        BattleScriptContext scriptContext = new(gameContext, this);
        while (scriptStep != null) {
            scriptStep.Activate(scriptContext);
            scriptStep = scriptStep.GetNextStep();
        }
        scriptStep = head;
    }

    /// <summary>
    /// Getter for the battle scene scripts current script step.
    /// </summary>
    /// <returns>The current script step.</returns>
    public ScriptStep ? GetSceneScriptStep() {
        return scriptStep;
    }

    private ScriptStep ? scriptStep;
}