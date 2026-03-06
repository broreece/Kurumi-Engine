namespace Engine.Input.ControllerInterfaces;

/// <summary>
/// The horizontal moveable input interface class, used to determine which input controllers can have a "MoveRight" and "MoveLeft" button press.
/// </summary>
public interface IHorizontalMoveable {
    /// <summary>
    /// Function that attempts to move right.
    /// </summary>
    public void MoveRight();

    /// <summary>
    /// Function that attempts to move left.
    /// </summary>
    public void MoveLeft();
}