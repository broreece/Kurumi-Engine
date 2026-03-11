namespace Scripts.EntityScripts.Base;

using Engine.Runtime.Core;
using Scripts.Base;
using Scripts.EntityScripts.EntityScriptSteps;
using Game.Entities.Base;

/// <summary>
/// The entity script class, a type of script that can activate containing two entites.
/// </summary>
public class EntityScript : Script {
    /// <summary>
    /// Constructor for the Entity script.
    /// </summary>
    /// <param name="scriptInformation">The string representing each script step in the script.</param>
    public EntityScript(string scriptText) {
        while (scriptText.Contains(',')) {
            string scriptName = scriptText[..scriptText.IndexOf(',')];
            scriptText = scriptText[(scriptText.IndexOf(',') + 1)..];
            switch (scriptName) {
                case "ChangeHp":
                    bool reduceHp = int.Parse(scriptText[..scriptText.IndexOf(',')]) == 1;
                    scriptText = scriptText[(scriptText.IndexOf(',') + 1)..];
                    bool canKnockOut = int.Parse(scriptText[..scriptText.IndexOf(',')]) == 1;
                    scriptText = scriptText[(scriptText.IndexOf(',') + 1)..];
                    string formula;
                    // Check if end of script.
                    if (scriptText.Contains(',')) {
                        formula = scriptText[..scriptText.IndexOf(',')];
                        scriptText = scriptText[(scriptText.IndexOf(',') + 1)..];
                    }
                    else {
                        formula = scriptText;
                    }
                    AddStep(new ChangeHp(reduceHp, canKnockOut, formula));
                    break;

                default:
                    break;
            }
        }
    }

    /// <summary>
    /// Activates the scene script.
    /// </summary>
    /// <param name="gameContext">The context of the game used by script steps.</param>
    /// <param name="user">The user entity.</param>
    /// <param name="user">The target entity.</param>
    public void Activate(GameContext gameContext, Entity user, Entity target) {
        ScriptStep ? scriptStep = head;
        EntityScriptContext entityScriptContext = new(gameContext, user, target);
        while (scriptStep != null) {
            scriptStep.Activate(entityScriptContext);
            scriptStep = scriptStep.GetNextStep();
        }
        scriptStep = head;
    }
}