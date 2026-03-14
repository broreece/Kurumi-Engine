namespace States.Map.Interfaces;

using Scripts.Base;

/// <summary>
/// The public continuable script interface, used to continue scripts during the map state.
/// </summary>
public interface IContinuableScript {
    /// <summary>
    /// Function used to continue a script following from a previous script step.
    /// </summary>
    /// <param name="previousStep">The last executed scene step.</param>
    public void ContinueScript(ScriptStep previousStep);
}