namespace Save.Serialization.PlayableCharacterRegistry;

/// <summary>
/// Character resistance data used to load elemental and status resistances.
/// </summary>
public class CharacterResistanceData {
    public List<int> Characters { get; set; } = [];
    public List<List<int>> Resistances { get; set; } = [];
}