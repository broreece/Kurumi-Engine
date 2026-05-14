using Engine.Input.Mapper;
using Engine.State.Base;
using Engine.UI.Layout.Core;
using Engine.UI.Render;

using Game.UI.Overlays.Base;

using Infrastructure.Rendering.Core;

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

    public StateManager(
        IGameState currentState, 
        StateContext stateContext, 
        InputMapper inputMapper, 
        RenderSystem renderSystem) 
    {
        _currentState = currentState;
        _currentState.OnEnter();
        _stateContext = stateContext;
        _inputMapper = inputMapper;
        _renderSystem = renderSystem;
        _uiRenderSystem = new UIRenderSystem(new UILayoutSystem());
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
                _uiRenderSystem.Render(uIOverlay.GetUIElement(), _renderSystem, _stateContext.GameWindow.Size);
            }

            // Load top UI element, check if finished, if so remove it, else check if it should handle the input state.
            IUIOverlay top = _stateContext.UIOverlays.Peek();
            if (top.TakesControl())
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
                script.Update(_currentState.GetScriptContext());
            }
        }

        if (stateInputValid) {
            _stateContext.InputContextManager.Update(inputState);
        }

        _renderSystem.Render();
    }
}