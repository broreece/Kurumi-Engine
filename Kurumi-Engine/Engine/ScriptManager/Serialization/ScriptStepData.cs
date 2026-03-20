namespace Engine.ScriptManager.Serialization;

using System.Text.Json;

/// <summary>
/// Script step data used to deseralize scripts steps from the script data.
/// </summary>
public sealed class ScriptStepData {
    public string Type { get; set; } = string.Empty;
    public Dictionary<string, JsonElement> Parameters { get; set; } = [];
}