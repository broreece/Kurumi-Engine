namespace Engine.Input.ControllerInterfaces;

/// <summary>
/// The escapeable input interface class, used to determine which input controllers can have a "Escape" button press.
/// </summary>
public interface IEscapeable {
    /// <summary>
    /// The escape function.
    /// </summary>
    public void Escape();
}