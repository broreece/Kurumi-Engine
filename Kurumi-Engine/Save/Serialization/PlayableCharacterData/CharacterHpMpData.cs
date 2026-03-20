namespace Save.Serialization.PlayableCharacterRegistry;

/// <summary>
/// Character hp and mp values used to load and save changing values.
/// </summary>
public class CharacterHpMpData {
    public List<int> Characters { get; set; } = [];
    public List<List<int>> HP { get; set; } = [];
    public List<List<int>> MP { get; set; } = [];
}