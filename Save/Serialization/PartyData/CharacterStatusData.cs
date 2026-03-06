namespace Save.Serialization.PartyData;

/// <summary>
/// The status data class used to seralize status data into json files.
/// </summary>
public class CharacterStatusData {
    public List<int> Characters { get; set; } = new();
    public List<int[]> Statuses { get; set; } = new();
}