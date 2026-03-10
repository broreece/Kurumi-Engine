namespace Save.Serialization.EnemyFormationData;

/// <summary>
/// The formation scripts data container.
/// </summary>
public class FormationScriptsData {
    public string OnFoundActor { get; set; } = "";
    public string MovementPatternActor { get; set; } = "";
    public string OnLoseScript { get; set; } = "";
    public string OnWinScript { get; set; } = "";
}