namespace Engine.Input.ControllerInterfaces;

/// <summary>
/// The selectable input interface class, used to determine which input controllers can have a "Select" button press.
/// </summary>
public interface ISelectable {
    /// <summary>
    /// The select function.
    /// </summary>
    public void Select();
}