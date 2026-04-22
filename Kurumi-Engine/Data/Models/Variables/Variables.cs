namespace Data.Models.Variables;

public sealed class Variables {
    public required Dictionary<string, bool> Flags { get; set; } = [];
    public required Dictionary<string, int> Counters { get; set; } = [];
}