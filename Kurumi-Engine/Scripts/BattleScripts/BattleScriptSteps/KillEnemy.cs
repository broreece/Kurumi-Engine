namespace Scripts.BattleScripts.BattleScriptSteps;

using Scripts.Base;
using Scripts.BattleScripts.Base;

/// <summary>
/// The kill enemy, battle scene script step.
/// </summary>
public sealed class KillEnemy : ScriptStep {
    /// <summary>
    /// /// Constructor for the kill enemy battle scene script step.
    /// </summary>
    /// <param name="enemyId">The enemy id (In the formation) that will be killed..</param>
    public KillEnemy(int enemyId) {
        this.enemyId = enemyId;
    }

    /// <summary>
    /// The activator function for the kill enemy battle script step.
    /// </summary>
    /// <param name="scriptContext">The context of the script.</param>
    public override void Activate(ScriptContext scriptContext) {
        BattleScriptContext battleScriptContext = (BattleScriptContext) scriptContext;
        battleScriptContext.KillEnemy(enemyId);
    }

    private readonly int enemyId;
}