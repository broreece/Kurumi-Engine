namespace Registry.Enemies;

using Game.Entities.Enemy;
using Scenes.Battle.Interfaces;

/// <summary>
/// The enemy data registry, contains data about the enemies.
/// </summary>
public sealed class EnemyRegistry : IEnemySpriteAccessor {
    /// <summary>
    /// Constructor for the enemy data registry.
    /// </summary>
    /// <param name="enemies">The enemies array.</param>
    public EnemyRegistry(Enemy[] enemies) {
        this.enemies = enemies;
    }

    /// <summary>
    /// Getter for a specific enemy in the enemies array.
    /// </summary>
    /// <param name="index">The index of the specific enemy.</param>
    /// <returns>The specific enemy.</returns>
    public Enemy GetEnemy(int index) {
        return enemies[index];
    }

    /// <summary>
    /// Getter for the sprite of a specified enemy.
    /// </summary>
    /// <param name="enemyIndex">The index of the enemy in the enemies array.</param>
    /// <returns>The sprite ID of the specified enemy.</returns>
    public int GetEnemySprite(int enemyIndex) {
        return enemies[enemyIndex].GetSpriteId();
    }

    private readonly Enemy[] enemies;
}