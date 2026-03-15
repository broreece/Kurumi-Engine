namespace Scripts.BattleScripts.BattleScriptSteps;

using Scripts.Base;
using Scripts.BattleScripts.Base;

/// <summary>
/// The use ability, battle scene script step.
/// </summary>
public sealed class UseAbility : ScriptStep {
    /// <summary>
    /// Constructor for the use ability battle scene script step.
    /// </summary>
    /// <param name="abilityId">The ability id that will be used.</param>
    public UseAbility(int abilityId) {
        this.abilityId = abilityId;
    }

    /// <summary>
    /// The activation function for the use ability script. Used in battles to force enemies to activate abilities.
    /// </summary>
    /// <param name="scriptContext">The context of the script.</param>
    public override void Activate(ScriptContext scriptContext) {
        BattleScriptContext battleScriptContext = (BattleScriptContext) scriptContext;
        battleScriptContext.ActivateAbility(abilityId);
    }

    /// <summary>
    /// Getter for the abilities ID.
    /// </summary>
    /// <returns>The ability id that is activated.</returns>
    public int GetAbilityId() {
        return abilityId;
    }

    private readonly int abilityId;
}
