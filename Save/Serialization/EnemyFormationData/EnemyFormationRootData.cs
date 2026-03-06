namespace Save.Serialization.EnemyFormationData;

/// <summary>
/// The enemy formation root class.
/// </summary>
public class EnemyFormationRootData {
    public List<EnemyFormationData> Formations { get; set; } = new();
}