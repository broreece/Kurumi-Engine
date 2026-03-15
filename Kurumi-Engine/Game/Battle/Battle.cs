namespace Game.Battle;

using Game.Entities.Enemy;
using Registry.Enemies;
using Save.Serialization.EnemyFormationData;

/// <summary>
/// The battle game class, contains information relating to battle objects.
/// </summary>
public class Battle {
    /// <summary>
    /// Constructor for a games battle object.
    /// </summary>
    /// <param name="enemyFormationRegistry">Enemy formation data object containing all enemy formations.</param>
    /// <param name="enemyRegistry">Enemy data object containing all enemies.</param>
    /// <param name="enemyFormationId">The enemy formation id of the battle scene.</param>
    public Battle(EnemyFormationRegistry enemyFormationRegistry, EnemyRegistry enemyRegistry, int enemyFormationId) {
        // Load enemies and the enemy formations.
        enemyFormationData = enemyFormationRegistry.GetEnemyFormation(enemyFormationId);
        enemies = new Enemy[enemyFormationData.Enemies.Count];

        // Load the enemy formations into an array.
        int enemyIndex = 0;
        foreach (EnemyFormationEnemyData currentEnemyData in enemyFormationData.Enemies) {
            Enemy currentEnemy = enemyRegistry.GetEnemy(currentEnemyData.Id);
            enemies[enemyIndex] = currentEnemy;
            enemyIndex ++;
        }
    }

    /// <summary>
    /// Function used to set a HP value of a specified enemy.
    /// </summary>
    /// <param name="index">The index of the specified enemy.</param>
    /// <param name="newHp">The new HP value of the enemy.</param>
    public void SetEnemyHp(int index, int newHp) {
        enemies[index].SetCurrentHP(newHp);
    }

    /// <summary>
    /// Function that returns an array of each enemies HP value.
    /// </summary>
    /// <returns>An array of each enemies HP value.</returns>
    public int[] GetEnemiesHp() {
        int[] hpValues = new int[enemies.Length];
        int index = 0;
        foreach (Enemy enemy in enemies) {
            hpValues[index] = enemy.GetCurrentHp();
            index ++;
        }
        return hpValues;
    }

    /// <summary>
    /// Function that returns a single enemies HP value.
    /// </summary>
    /// <param name="index">The index of the specified enemy.</param>
    /// <returns>The enemies HP.</returns>
    public int GetEnemyHp(int index) {
        return enemies[index].GetCurrentHp();
    }

    /// <summary>
    /// Function that returns a single enemies stat value.
    /// </summary>
    /// <param name="enemyIndex">The index of the specified enemy.</param>
    /// <param name="statIndex">The index of the specified stat.</param>
    /// <returns>The enemies stat at a specified location.</returns>
    public int GetEnemyStat(int enemyIndex, int statIndex) {
        return enemies[enemyIndex].GetStat(statIndex);
    }

    /// <summary>
    /// Function that returns the amount of enemies.
    /// </summary>
    /// <returns>The amount of enemies in the battle.</returns>
    public int GetEnemiesLength() {
        return enemies.Length;
    }

    /// <summary>
    /// Checks if the battle has been won.
    /// </summary>
    /// <returns>If all enemies are dead.</returns>
    public bool WonBattle() {
        foreach (Enemy enemy in enemies) {
            if (enemy.GetCurrentHp() > 0) {
                return false;
            }
        }
        return true;
    }

    /// <summary>
    /// Getter for the list of enemy formation enemy data from the battles enemy formation data.
    /// </summary>
    /// <returns>A list of enemy formation enemy data.</returns>
    public List<EnemyFormationEnemyData> GetEnemyFormationEnemyData() {
        return enemyFormationData.Enemies;
    }

    /// <summary>
    /// Getter for a specific enemy formation enemy data from the battles enemy formation data.
    /// </summary>
    /// <param name="index">The index of the desired enemy.</param>
    /// <returns>The specific enemy formation enemy data requested.</returns>
    public EnemyFormationEnemyData GetEnemyFormationEnemyData(int index) {
        return enemyFormationData.Enemies[index];
    }

    /// <summary>
    /// Getter for a specific enemyin the enemies array.
    /// </summary>
    /// <param name="index">The specific index of the enemy.</param>
    /// <returns>The specific enemy.</returns>
    public Enemy GetEnemy(int index) {
        return enemies[index];
    }

    // Stored enemy information.
    private readonly EnemyFormationData enemyFormationData;
    private readonly Enemy[] enemies;
}
