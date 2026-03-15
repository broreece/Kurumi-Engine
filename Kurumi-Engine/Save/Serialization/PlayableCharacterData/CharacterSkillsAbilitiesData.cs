namespace Save.Serialization.PlayableCharacterRegistry;

/// <summary>
/// Character skills abilities data used to load skills and skill abilities to characters.
/// </summary>
public class CharacterSkillsAbilitiesData {
    public List<int> Characters { get; set; } = new();
    public List<List<int>> Skills { get; set; } = new();
    public List<List<List<int>>> Abilities { get; set; } = new();
}