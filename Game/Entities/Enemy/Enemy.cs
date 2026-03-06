namespace Game.Entities.Enemy;

using Game.Entities.Base;
using Game.Entities.Skills;

/// <summary>
/// The enemy class. Contains information relating to enemies combat stats. Inherits from entity.
/// </summary>
public sealed class Enemy : Entity {
    /// <summary>
    /// Constructor for the enemy.
    /// </summary>
    /// <param name="name">Name of the enemy.</param>
    /// <param name="description">Description of the enemy.</param>
    /// <param name="maxHp">The max hp value of the enemy.</param>
    /// <param name="spriteId">The battle sprite ID of the enemy.</param>
    /// <param name="stats">The enemies stats.</param>
    /// <param name="elementalResistances">The enemies elemental resistances.</param>
    /// <param name="statusResistances">The entities status resistances.</param>
    /// <param name="baseAbilities">The enemies base abilities.</param>
    public Enemy(string name, string description, int maxHp, int spriteId, int[] stats, int[] elementalResistances, 
        int[] statusResistances, List<Ability> baseAbilities) 
        : base(name, description, maxHp, spriteId, stats, elementalResistances, statusResistances, baseAbilities) {
    }

    /// <summary>
    /// Overriden function to return the base abilities of an enemy.
    /// </summary>
    /// <returns>The enemies base abilities.</returns>
    public override List<Ability> GetBaseAbilities() {
        return baseAbilities;
    }

    /// <summary>
    /// Getter for a specific ability from the base abilities list.
    /// </summary>
    /// <param name="index">The index of the desired ability.</param>
    /// <returns>The specified ability.</returns>
    public override Ability GetBaseAbility(int index) {
        return baseAbilities[index];
    }
}