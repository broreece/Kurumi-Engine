namespace Scripts.MapScripts.Base;

using Engine.Runtime;
using Scripts.Base;
using Scripts.MapScripts.MapScriptSteps;
using Scripts.UniversalScriptSteps;

/// <summary>
/// The map scene script class, a type of script that can activate on the map scene.
/// </summary>
public class MapScript : Script {
    /// <summary>
    /// Constructor for the map scene script.
    /// </summary>
    /// <param name="scriptText">The string representing each script step in the script.</param>
    public MapScript(string scriptText) {
        while (scriptText.Contains(',')) {
            string scriptName = scriptText[..scriptText.IndexOf(',')];
            scriptText = scriptText[(scriptText.IndexOf(',') + 1)..];
            switch (scriptName) {
                case "AddStatusToParty":
                    int statusId = int.Parse(scriptText[..scriptText.IndexOf(',')]);
                    scriptText = scriptText[(scriptText.IndexOf(',') + 1)..];
                    AddStep(new AddStatusToParty(statusId));
                    break;

                case "BasicTextWindow":
                    int windowArtId = int.Parse(scriptText[..scriptText.IndexOf(',')]);
                    scriptText = scriptText[(scriptText.IndexOf(',') + 1)..];
                    int fontId = int.Parse(scriptText[..scriptText.IndexOf(',')]);
                    scriptText = scriptText[(scriptText.IndexOf(',') + 1)..];
                    int xPosition = int.Parse(scriptText[..scriptText.IndexOf(',')]);
                    scriptText = scriptText[(scriptText.IndexOf(',') + 1)..];
                    int yPosition = int.Parse(scriptText[..scriptText.IndexOf(',')]);
                    scriptText = scriptText[(scriptText.IndexOf(',') + 1)..];
                    string text = scriptText[..scriptText.IndexOf(',')];
                    scriptText = scriptText[(scriptText.IndexOf(',') + 1)..];
                    AddStep(new BasicTextWindow(windowArtId, fontId, xPosition, yPosition, text));
                    break;

                case "ChangeFlag":
                    int flagId = int.Parse(scriptText[..scriptText.IndexOf(',')]);
                    scriptText = scriptText[(scriptText.IndexOf(',') + 1)..];
                    bool newValue = int.Parse(scriptText[..scriptText.IndexOf(',')]) == 1;
                    scriptText = scriptText[(scriptText.IndexOf(',') + 1)..];
                    AddStep(new ChangeFlag(flagId, newValue));
                    break;

                case "ChangeMap":
                    int mapId = int.Parse(scriptText[..scriptText.IndexOf(',')]);
                    scriptText = scriptText[(scriptText.IndexOf(',') + 1)..];
                    int xLocation = int.Parse(scriptText[..scriptText.IndexOf(',')]);
                    scriptText = scriptText[(scriptText.IndexOf(',') + 1)..];
                    int yLocation = int.Parse(scriptText[..scriptText.IndexOf(',')]);
                    scriptText = scriptText[(scriptText.IndexOf(',') + 1)..];
                    AddStep(new ChangeMap(mapId, xLocation, yLocation));
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
                    int choiceBoxArtId = int.Parse(scriptText[..scriptText.IndexOf(',')]);
                    scriptText = scriptText[(scriptText.IndexOf(',') + 1)..];
                    int choiceBoxfontId = int.Parse(scriptText[..scriptText.IndexOf(',')]);
                    scriptText = scriptText[(scriptText.IndexOf(',') + 1)..];
                    int choixeBoxXPosition = int.Parse(scriptText[..scriptText.IndexOf(',')]);
                    scriptText = scriptText[(scriptText.IndexOf(',') + 1)..];
                    int choiceBoxYPosition = int.Parse(scriptText[..scriptText.IndexOf(',')]);
                    scriptText = scriptText[(scriptText.IndexOf(',') + 1)..];
                    string fullChoices = scriptText[..scriptText.IndexOf(',')];
                    string[] choices = fullChoices.Split('-');
                    scriptText = scriptText[(scriptText.IndexOf(',') + 1)..];
                    int choiceBoxNextIfFalse = int.Parse(scriptText[..scriptText.IndexOf(',')]);
                    scriptText = scriptText[(scriptText.IndexOf(',') + 1)..];
                    AddStep(new ChoiceBox(choiceBoxArtId, choiceBoxfontId, choixeBoxXPosition,
                        choiceBoxYPosition, choices, choiceBoxNextIfFalse));
                    break;

                case "DisplayGlobalMessage":
                    int globalMessageTimeLimit = int.Parse(scriptText[..scriptText.IndexOf(',')]);
                    scriptText = scriptText[(scriptText.IndexOf(',') + 1)..];
                    string globalMessageText = scriptText[..scriptText.IndexOf(',')];
                    scriptText = scriptText[(scriptText.IndexOf(',') + 1)..];                 
                    AddStep(new DisplayGlobalMessage(globalMessageTimeLimit, globalMessageText));
                    break;

                case "ForceMoveScript":
                    bool keepDirection = int.Parse(scriptText[..scriptText.IndexOf(',')]) == 1;
                    scriptText = scriptText[(scriptText.IndexOf(',') + 1)..];
                    bool lockMovement = int.Parse(scriptText[..scriptText.IndexOf(',')]) == 1;
                    scriptText = scriptText[(scriptText.IndexOf(',') + 1)..];
                    bool instant = int.Parse(scriptText[..scriptText.IndexOf(',')]) == 1;
                    scriptText = scriptText[(scriptText.IndexOf(',') + 1)..];
                    int scriptX = int.Parse(scriptText[..scriptText.IndexOf(',')]);
                    scriptText = scriptText[(scriptText.IndexOf(',') + 1)..];
                    int scriptY = int.Parse(scriptText[..scriptText.IndexOf(',')]);
                    scriptText = scriptText[(scriptText.IndexOf(',') + 1)..];
                    List<int> path = [];
                    // Check if value is int, if so keep looping.
                    while (scriptText.Contains(',') && int.TryParse(scriptText[..scriptText.IndexOf(',')], out int nextStep)) {
                        path.Add(nextStep);
                        scriptText = scriptText[(scriptText.IndexOf(',') + 1)..];
                    }
                    AddStep(new ForceMoveScript(keepDirection, lockMovement, instant, scriptX, scriptY, path));
                    break;

                case "ForceMoveParty":
                    bool partyKeepDirection = int.Parse(scriptText[..scriptText.IndexOf(',')]) == 1;
                    scriptText = scriptText[(scriptText.IndexOf(',') + 1)..];
                    List<int> partyPath = new();
                    // Check if value is int, if so keep looping.
                    while (scriptText.Contains(',') && int.TryParse(scriptText[..scriptText.IndexOf(',')], out int nextStep)) {
                        partyPath.Add(nextStep);
                        scriptText = scriptText[(scriptText.IndexOf(',') + 1)..];
                    }
                    AddStep(new ForceMoveParty(partyKeepDirection, partyPath));
                    break;

                case "StartBattle":
                    int backgroundMusicId = int.Parse(scriptText[..scriptText.IndexOf(',')]);
                    scriptText = scriptText[(scriptText.IndexOf(',') + 1)..];
                    int battleBackgroundArtId = int.Parse(scriptText[..scriptText.IndexOf(',')]);
                    scriptText = scriptText[(scriptText.IndexOf(',') + 1)..];
                    int enemyFormationId = int.Parse(scriptText[..scriptText.IndexOf(',')]);
                    scriptText = scriptText[(scriptText.IndexOf(',') + 1)..];
                    AddStep(new StartBattle(backgroundMusicId, battleBackgroundArtId, enemyFormationId));
                    break;

                case "WindowWithTextAndNamebox":
                    int windowAndNameArtId = int.Parse(scriptText[..scriptText.IndexOf(',')]);
                    scriptText = scriptText[(scriptText.IndexOf(',') + 1)..];
                    int windowAndNameFontId = int.Parse(scriptText[..scriptText.IndexOf(',')]);
                    scriptText = scriptText[(scriptText.IndexOf(',') + 1)..];
                    string mainText = scriptText[..scriptText.IndexOf(',')];
                    scriptText = scriptText[(scriptText.IndexOf(',') + 1)..];
                    string nameText = scriptText[..scriptText.IndexOf(',')];
                    scriptText = scriptText[(scriptText.IndexOf(',') + 1)..];
                    AddStep(new ShowWindowWithTextAndNamebox(windowAndNameArtId, windowAndNameFontId, mainText, nameText));
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
        MapScriptContext scriptContext = new(gameContext);
        while (scriptStep != null) {
            scriptStep.Activate(scriptContext);
            scriptStep = scriptStep.GetNextStep();
        }
        scriptStep = head;
    }

    /// <summary>
    /// Getter for the map scene scripts current script step.
    /// </summary>
    /// <returns>The current script step.</returns>
    public ScriptStep ? GetSceneScriptStep() {
        return scriptStep;
    }

    private ScriptStep ? scriptStep;
}