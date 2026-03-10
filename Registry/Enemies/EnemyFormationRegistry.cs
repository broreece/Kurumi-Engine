namespace Registry.Enemies;

using Save.Serialization.EnemyFormationData;

/// <summary>
/// The enemy formation data registry, contains data about the enemy formations.
/// </summary>
public sealed class EnemyFormationRegistry {
    /// <summary>
    /// Constructor for the enemy formation data registry.
    /// </summary>
    /// <param name="enemyFormations">The enemy formations array.</param>
    public EnemyFormationRegistry(EnemyFormationData[] enemyFormations) {
        this.enemyFormations = enemyFormations;
    }

    /// <summary>
    /// Returns a specified enemy formation from the enemy formations array.
    /// </summary>
    /// <param name="index">The index at of the enemy formation to access.</param>
    /// <returns>The specified enemy formation.</returns>
    public EnemyFormationData GetEnemyFormation(int index) {
        return enemyFormations[index];
    }

    // TODO: (AS-01) Implement a save function here.

    private EnemyFormationData[] enemyFormations;
}