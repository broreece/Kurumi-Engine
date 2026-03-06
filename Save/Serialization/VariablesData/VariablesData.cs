namespace Save.Serialization.VariablesData;

/// <summary>
/// Game variable data structure used to deseralize the in game variables.
/// </summary>
public class VariablesData {
    public Dictionary<string, int> Flags { get; set; } = new();
    public Dictionary<string, int> Variables { get; set; } = new();
}