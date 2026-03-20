namespace Save.Serialization.EnemyFormationData;

/// <summary>
/// The enemy formation enemy data class. Contains information about the individual enemies.
/// </summary>
public class EnemyFormationEnemyData {
    public int Id { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
    public int MainPart { get; set; }
    public List<int> Statuses { get; set; } = [];
    public List<BattleScriptData> BattleScripts { get; set; } = [];
    public int OnKillScript { get; set; }
}