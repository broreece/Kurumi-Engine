using SFML.Window;

namespace Engine.Input.System;

/// <summary>
/// Handles the raw key press by storing them in hash sets representing when they were pressed.
/// </summary>
public sealed class InputSystem 
{
    private readonly HashSet<Keyboard.Key> _currentKeys = [];
    private readonly HashSet<Keyboard.Key> _previousKeys = [];   

    public void Update() 
    {
        // Set previous keys.
        _previousKeys.Clear();
        foreach (var key in _currentKeys)
        {
            _previousKeys.Add(key);
        }

        // Set current keys.
        _currentKeys.Clear();
        foreach (Keyboard.Key key in Enum.GetValues(typeof(Keyboard.Key))) 
        {
            if (Keyboard.IsKeyPressed(key)) 
            {
                _currentKeys.Add(key);
            }
        }
    }

    public bool IsHeld(Keyboard.Key key) => _currentKeys.Contains(key);

    public bool IsPressed(Keyboard.Key key) => _currentKeys.Contains(key) && !_previousKeys.Contains(key);

    public bool IsReleased(Keyboard.Key key) => !_currentKeys.Contains(key) && _previousKeys.Contains(key);
}