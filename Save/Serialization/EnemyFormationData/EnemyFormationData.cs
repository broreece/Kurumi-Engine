namespace Save.Serialization.EnemyFormationData;

/// <summary>
/// The enemy formation class. Contains information stored in enemy formation files.
/// </summary>
public class EnemyFormationData {
    public int MapId { get; set; }
    public int FieldArtId { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
    public int ReturnX { get; set; }
    public int ReturnY { get; set; }
    public int SearchTimer { get; set; }
    public int LootPool { get; set; }
    public int Found { get; set; }
    public int Dead { get; set; }
    public List<EnemyFormationEnemyData> Enemies { get; set; } = new();
    public FormationScriptsData Scripts { get; set; } = new();
}