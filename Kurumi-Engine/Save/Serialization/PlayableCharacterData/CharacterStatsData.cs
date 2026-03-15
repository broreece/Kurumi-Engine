namespace Save.Serialization.PlayableCharacterRegistry;

/// <summary>
/// Character stat data used to load stats.
/// </summary>
public class CharacterStatsData {
    public List<int> Characters { get; set; } = new();
    public List<List<int>> Stats { get; set; } = new();
}