namespace States.Map.Interfaces;

/// <summary>
/// The public finishable interface, used for objects that can be finished.
/// </summary>
public interface IFinishable {
    /// <summary>
    /// Function that returns if the finishable object has finished.
    /// </summary>
    /// <returns>True if the object has finished it's action, false otherwise.</returns>
    public bool IsFinished();
}