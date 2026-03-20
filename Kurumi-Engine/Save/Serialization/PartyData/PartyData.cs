namespace Save.Serialization.PartyData;

/// <summary>
/// The party data class used to deseralize party data from json files.
/// </summary>
public class PartyData {
    public int Visible { get; set; }
    public int CurrentMap { get; set; }
    public string ? MapName { get; set; }
    public int XLocation { get; set; }
    public int YLocation { get; set; }
    public int Facing { get; set; }
    public List<int> PartyMembers { get; set; } = [];
}