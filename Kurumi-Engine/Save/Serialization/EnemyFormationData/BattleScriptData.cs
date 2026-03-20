namespace Save.Serialization.EnemyFormationData;

/// <summary>
/// The battle script data, contains sub data for the enemies in the formations.
/// </summary>
public class BattleScriptData {
    public int Script { get; set; }
    public int Target { get; set; }
    public int StartTurn { get; set; }
    public int Frequency { get; set; }
}