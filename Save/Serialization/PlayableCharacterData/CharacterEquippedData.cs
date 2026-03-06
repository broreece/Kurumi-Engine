namespace Save.Serialization.PlayableCharacterRegistry;

/// <summary>
/// Character equipped data, the saved data that shows what each character has equipped.
/// </summary>
public class CharacterEquippedData {
    public List<int> Characters { get; set; } = new();
    public List<List<int>> Equipped { get; set; } = new();
}