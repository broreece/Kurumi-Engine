// Engine.
using Engine.Input.Base;
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
        _renderSystem.Clear();

        // Draw game state.
        _currentState.Update(deltaTime);

        // Use to check if we should execute the current input on the game state or the UI.
        bool stateInputValid = true;

        var inputState = _inputMapper.BuildState(); 

        if (_stateContext.UIOverlays.Count > 0)
        {
            UpdateAndDrawUI(deltaTime);
            stateInputValid = !HandleTopUI(inputState);
        }

        UpdateScripts();

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

    private void UpdateAndDrawUI(float deltaTime)
    {
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
    }

    private void UpdateScripts()
    {
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
    }

    private bool HandleTopUI(InputState inputState)
    {
        IUIOverlay top = _stateContext.UIOverlays.Peek();

        bool inputConsumed = HandleUIInput(inputState, top);
        
        if (top.IsFinished())
        {
            _stateContext.UIOverlays.Pop();
        }

        return inputConsumed;
    }

    /// <summary>
    /// Function that handles the UI input and returns true if the input was handled or false if not.
    /// </summary>
    /// <param name="inputState">The current input state.</param>
    /// <param name="uiOverlay">The UI overlay.</param>
    /// <returns>True if the input was handeled or false otherwise.</returns>
    private bool HandleUIInput(InputState inputState, IUIOverlay uiOverlay)
    {
        if (uiOverlay.TakesControl)
        {
            uiOverlay.HandleInput(inputState);
            return true;
        }

        return false;
    }
}