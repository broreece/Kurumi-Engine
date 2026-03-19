namespace Engine.ScriptManager.Serialization;

/// <summary>
/// Script data used to deseralize scripts from the script's json file.
/// </summary>
public sealed class ScriptData {
    public string Name { get; set; } = string.Empty;
    public List<ScriptStepData> Steps { get; set; } = [];
}