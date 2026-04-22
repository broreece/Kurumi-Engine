using System.Text.Json;

namespace Game.Scripts.Serialization;

public sealed class ScriptStepData 
{
    public required string Type { get; set; }

    public string? Next { get; set; }

    public Dictionary<string, JsonElement> Parameters { get; set; } = [];
}