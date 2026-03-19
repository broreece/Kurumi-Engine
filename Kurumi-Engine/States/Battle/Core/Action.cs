namespace States.Battle.Core;

using Scripts.BattleScripts.Base;

/// <summary>
/// The action class used to determine the list of actions executed in speed order.
/// </summary>
public class Action {
    /// <summary>
    /// Constructor for the action object used in the battle state.
    /// </summary>
    /// <param name="characterId">The character id that is executing the action.</param>
    /// <param name="targetId">The target id that is being targeted.</param>
    /// <param name="speed">The speed of the action.</param>
    /// <param name="battleScript">The battle script that is being executed.</param>
    /// <param name="enemy">If the action is being done by the enemy formation.</param>
    public Action(int characterId, int targetId, int speed, BattleScript battleScript, bool enemy) {
        this.characterId = characterId;
        this.targetId = targetId;
        this.speed = speed;
        this.battleScript = battleScript;
        this.enemy = enemy;
    }

    /// <summary>
    /// Getter for the character ID.
    /// </summary>
    /// <returns>The id of the character.</returns>
    public int GetCharacterId() {
        return characterId;
    }

    /// <summary>
    /// Getter for the target ID.
    /// Target -1 is random party member, -2 is all party, target -3 is a random enemy, -4 is all enemies.
    /// </summary>
    /// <returns>The id of the target.</returns>
    public int GetTargetId() {
        return targetId;
    }

    /// <summary>
    /// Getter for the speed value.
    /// </summary>
    /// <returns>The speed associated with this action.</returns>
    public int GetSpeed() {
        return speed;
    }

    /// <summary>
    /// Getter for the battle script identifier.
    /// </summary>
    /// <returns>The battle script string identifier.</returns>
    public BattleScript GetBattleScript() {
        return battleScript;
    }

    /// <summary>
    /// Getter indicating whether the user is an enemy.
    /// </summary>
    /// <returns>True if the user is an enemy; otherwise, false.</returns>
    public bool IsEnemy() {
        return enemy;
    }

    private readonly int characterId, targetId, speed;
    private readonly bool enemy;
    private readonly BattleScript battleScript;
}