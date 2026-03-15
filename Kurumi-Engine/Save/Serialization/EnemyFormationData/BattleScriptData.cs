namespace Save.Serialization.EnemyFormationData;

/// <summary>
/// The battle script data, contains sub data for the enemies in the formations.
/// </summary>
public class BattleScriptData {
    public string Script { get; set; } = "";
    public int Target { get; set; }
    public int StartTurn { get; set; }
    public int Frequency { get; set; }
}