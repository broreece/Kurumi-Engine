using Engine.Input.Base;
using Engine.Input.System;

using SFML.Window;

namespace Engine.Input.Mapper;

/// <summary>
/// Maps keyboard keys to input actions. Utilizes the input system to return an input state.
/// </summary>
public sealed class InputMapper 
{
    private readonly Dictionary<InputAction, List<Keyboard.Key>> _bindings = [];
    private readonly InputSystem _inputSystem;

    public InputMapper(InputSystem inputSystem) 
    {
        _inputSystem = inputSystem;
        InitializeDefaultBindings();
    }

    public InputState BuildState() 
    {
        var inputState = new InputState();
        foreach (var pair in _bindings) 
        {
            var action = pair.Key;
            var keys = pair.Value;
            foreach (var key in keys) 
            {
                if (_inputSystem.IsHeld(key)) 
                {
                    inputState.SetHeld(action);
                }

                if (_inputSystem.IsPressed(key))
                {
                    inputState.SetPressed(action);
                }

                if (_inputSystem.IsReleased(key))
                {
                    inputState.SetReleased(action);
                }
            }
        }
        return inputState;
    }

    private void InitializeDefaultBindings() 
    {
        _bindings[InputAction.Confirm] = [Keyboard.Key.Z];
        _bindings[InputAction.Cancel] = [Keyboard.Key.X];

        _bindings[InputAction.MoveUp] = [Keyboard.Key.W, Keyboard.Key.Up];
        _bindings[InputAction.MoveDown] = [Keyboard.Key.S, Keyboard.Key.Down];
        _bindings[InputAction.MoveLeft] = [Keyboard.Key.A, Keyboard.Key.Left];
        _bindings[InputAction.MoveRight] = [Keyboard.Key.D, Keyboard.Key.Right];

        _bindings[InputAction.Pause] = [Keyboard.Key.Escape];
    }
}