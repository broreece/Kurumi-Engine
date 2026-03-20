namespace Save.Serialization.PartyData;

/// <summary>
/// The inventory data class used to deseralize inventory data from json files.
/// </summary>
public class InventoryData {
    public List<int> Items { get; set; } = [];
    public List<int> Amount { get; set; } = [];
}