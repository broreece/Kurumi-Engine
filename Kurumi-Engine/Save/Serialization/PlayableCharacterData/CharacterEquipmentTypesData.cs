namespace Save.Serialization.PlayableCharacterRegistry;

/// <summary>
/// Character equipment type data used to load characters equipable types.
/// </summary>
public class CharacterEquipmentTypesData {
    public List<int> Characters { get; set; } = new();
    public List<List<int>> EquipmentTypes { get; set; } = new();
}