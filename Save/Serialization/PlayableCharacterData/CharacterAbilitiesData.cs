namespace Save.Serialization.PlayableCharacterRegistry;

/// <summary>
/// Character ability data used to load base abilities.
/// </summary>
public class CharacterAbilitiesData {
    public List<int> Characters { get; set; } = new();
    public List<List<int>> Abilities { get; set; } = new();
}