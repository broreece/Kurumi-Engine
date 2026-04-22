namespace Game.Scripts.Serialization;

public sealed class ScriptData 
{
    public required string Name { get; set; }
    public required string FirstStep { get; set; }
    
    public Dictionary<string, ScriptStepData> Steps { get; set; } = [];
}