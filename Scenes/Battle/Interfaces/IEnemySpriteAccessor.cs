namespace Scenes.Battle.Interfaces;

/// <summary>
/// The enemy sprite accessor interface. Used to access an enemy sprite at a given index.
/// </summary>
public interface IEnemySpriteAccessor {
    /// <summary>
    /// Getter for the sprite of a specified enemy.
    /// </summary>
    /// <param name="enemyIndex">The index of the enemy in the enemies array.</param>
    /// <returns>The sprite ID of the specified enemy.</returns>
    public int GetEnemySprite(int enemyIndex);
}