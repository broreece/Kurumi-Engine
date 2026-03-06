namespace Scripts.BattleScripts.Base;

using Engine.Runtime;
using Scripts.Base;
using States.Battle.Core;

/// <summary>
/// Battle script context class, contains additional functions that utilize battle scene and state.
/// </summary>
public sealed class BattleScriptContext : SceneScriptContext {
    /// <summary>
    /// Constructor for the battle script context class.
    /// </summary>
    /// <param name="gameContext">The game's context object.</param>
    public BattleScriptContext(GameContext gameContext) : base(gameContext) {}

    /// <summary>
    /// Function used to kill an enemy within the battle.
    /// </summary>
    /// <param name="enemyId">The enemy id to be killed.</param>
    public void KillEnemy(int enemyId) {
        GetBattleState()?.KillEnemy(enemyId);
    }

    /// <summary>
    /// Function used to activate an ability within battle.
    /// </summary>
    /// <param name="abilityId">The ability id to be executed.</param>
    public void ActivateAbility(int abilityId) {
        GetBattleState()?.ActivateAbility(abilityId);
    }

    /// <summary>
    /// Private function used to load the battle state from the game context.
    /// </summary>
    /// <returns>The state currently set as active.</returns>
    private BattleState? GetBattleState() {
        return (BattleState?) gameContext.GetCurrentState();
    }
}