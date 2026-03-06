namespace Engine.Input.ControllerInterfaces;

/// <summary>
/// The cancelable input interface class, used to determine which input controllers can have a "Cancel" button press.
/// </summary>
public interface ICancelable {
    /// <summary>
    /// The cancel function.
    /// </summary>
    public void Cancel();
}