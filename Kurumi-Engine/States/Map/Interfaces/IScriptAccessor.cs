namespace States.Map.Interfaces;

using Scripts.MapScripts.Base;

/// <summary>
/// The public script acessor interface, used for objects where you can access a script.
/// </summary>
public interface IScriptAccessor {
    /// <summary>
    /// Getter for the object's linked script.
    /// </summary>
    /// <returns>A script (if any) that is linked to the object.</returns>
    public MapScript GetScript();
}