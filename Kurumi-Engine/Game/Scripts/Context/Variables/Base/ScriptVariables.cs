using Data.Runtime.Entities.Base;

namespace Game.Scripts.Context.Variables.Base;

/// <summary>
/// Contains a fixed pairing of variable keys with fixed types and the key name.
/// Acts similarly to an enum but unlike enums, we allow creation of new variable keys.
/// </summary>
public static class ScriptVariables 
{
    public static readonly ScriptVariableKey<EntityIndex> User = new() { Name = "user"};
    public static readonly ScriptVariableKey<EntityIndex> Target = new() { Name = "target"};
}
