namespace Engine.Input.ControllerInterfaces;

/// <summary>
/// The vertical moveable input interface class, used to determine which input controllers can have a "MoveUp" and "MoveDown" button press.
/// </summary>
public interface IVerticalMoveable {
    /// <summary>
    /// Function that moves up.
    /// </summary>
    public void MoveUp();

    /// <summary>
    /// Function that moves down.
    /// </summary>
    public void MoveDown();
}