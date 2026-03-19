namespace Scripts.EntityScripts.Base;

using Engine.Runtime;
using Scripts.Base;
using Game.Entities.Base;

/// <summary>
/// The entity script class, a type of script that can activate containing two entites.
/// </summary>
public class EntityScript : Script {
    /// <summary>
    /// Constructor for the Entity script.
    /// </summary>
    /// <param name="name">The name of the script.</param>
    /// <param name="head">The head of the script.</param>
    public EntityScript(string name, ScriptStep head) : base(name, head) {}

    /// <summary>
    /// Activates the entity script.
    /// </summary>
    /// <param name="gameContext">The context of the game used by script steps.</param>
    /// <param name="user">The user entity.</param>
    /// <param name="target">The target entity.</param>
    public void Activate(GameContext gameContext, Entity user, Entity target) {
        ScriptStep ? scriptStep = head;
        EntityScriptContext entityScriptContext = new(gameContext, user, target);
        while (scriptStep != null) {
            scriptStep.Activate(entityScriptContext);
            scriptStep = scriptStep.GetNextStep();
        }
    }
}