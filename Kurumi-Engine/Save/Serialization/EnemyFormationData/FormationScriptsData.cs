namespace Save.Serialization.EnemyFormationData;

/// <summary>
/// The formation scripts data container.
/// </summary>
public class FormationScriptsData {
    public int OnFoundActor { get; set; }
    public int MovementPatternActor { get; set; }
    public int OnLoseScript { get; set; }
    public int OnWinScript { get; set; }
}