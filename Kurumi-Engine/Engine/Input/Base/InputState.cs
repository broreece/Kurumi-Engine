namespace Engine.Input.Base;

/// <summary>
/// Contains hash sets for button states and functions to set/get a buttons state.
/// </summary>
public sealed class InputState 
{
    private readonly HashSet<InputAction> _pressed = [];
    private readonly HashSet<InputAction> _held = [];
    private readonly HashSet<InputAction> _released = [];

    public void Clear() 
    {
        _pressed.Clear();
        _held.Clear();
        _released.Clear();
    }

    public void SetPressed(InputAction action) => _pressed.Add(action);

    public void SetHeld(InputAction action) => _held.Add(action);

    public void SetReleased(InputAction action) => _released.Add(action);

    public bool IsPressed(InputAction action) => _pressed.Contains(action);

    public bool IsHeld(InputAction action) => _held.Contains(action);

    public bool IsReleased(InputAction action) => _released.Contains(action);
}