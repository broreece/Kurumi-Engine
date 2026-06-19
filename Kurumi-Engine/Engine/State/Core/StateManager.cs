// Engine.
using Engine.Input.Mapper;

using Engine.State.Base;

using Engine.UI.Render;

// Game/
using Game.UI.Overlays.Base;

// Infrastructure.
using Infrastructure.Rendering.Core;

// External libraries.
using SFML.System;

namespace Engine.State.Core;

/// <summary>
/// Holds the current state and allows changing states or updating the current state.
/// </summary>
public sealed class StateManager 
{
    // Current state and context.
    private IGameState _currentState;
    private readonly StateContext _stateContext;

    // Render systems.
    private readonly RenderSystem _renderSystem;
    private readonly UIRenderSystem _uiRenderSystem;

    // Input mapper.
    private readonly InputMapper _inputMapper;

    // Virtual window size.
    private readonly Vector2u _displaySize;

    public StateManager(
        IGameState currentState, 
        StateContext stateContext, 
        InputMapper inputMapper, 
        RenderSystem renderSystem,
        UIRenderSystem uIRenderSystem,
        Vector2u displaySize
    ) 
    {
        _currentState = currentState;
        _currentState.OnEnter();
        _stateContext = stateContext;
        _inputMapper = inputMapper;
        _renderSystem = renderSystem;
        _uiRenderSystem = uIRenderSystem;
        _displaySize = displaySize;
    }

    public void ChangeState(IGameState newState) 
    {
        _currentState.OnExit();
        _renderSystem.Clear();
        _currentState = newState;
        _currentState.OnEnter();
    }

    public void Update(float deltaTime)
    {
        // Clear display.
        _renderSystem.Clear();

        // Draw game state.
        _currentState.Update(deltaTime);

        // Use to check if we should execute the current input on the game state.
        bool stateInputValid = true;

        // The current input state.
        var inputState = _inputMapper.BuildState(); 

        if (_stateContext.UIOverlays.Count > 0)
        {
            // Draw UI.
            foreach (IUIOverlay uIOverlay in _stateContext.UIOverlays)
            {
                uIOverlay.Update(deltaTime);
                _uiRenderSystem.Render(
                    uIOverlay.UIElement, 
                    _renderSystem, 
                    _displaySize, 
                    _stateContext.GameWindow.Size
                );
            }

            // Load top UI element, check if finished, if so remove it, else check if it should handle the input state.
            IUIOverlay top = _stateContext.UIOverlays.Peek();
            if (top.TakesControl)
            {
                stateInputValid = false;
                top.HandleInput(inputState);
            }
            if (top.IsFinished())
            {
                _stateContext.UIOverlays.Pop();
            }
        }

        // Loop through executing scripts, remove any that have finished.
        var executingScripts = _stateContext.Scripts;
        for (int scriptIndex = executingScripts.Count - 1; scriptIndex >= 0; scriptIndex --)
        {
            var script = executingScripts[scriptIndex];
            if (script.Finished)
            {
                _stateContext.RemoveExecutingScript(script);
            }
            else
            {
                script.Update(_currentState.ScriptContext);
            }
        }

        if (stateInputValid) 
        {
            _stateContext.InputContextManager.Update(inputState);
        }

        _renderSystem.Render();
    }

    /// <summary>
    /// Function used to determine if the state is ready to be changed.
    /// </summary>
    /// <returns>If the state is ready to be changed.</returns>
    public bool ReadyToChangeState()
    {
        return !_stateContext.IsPaused();
    }
}