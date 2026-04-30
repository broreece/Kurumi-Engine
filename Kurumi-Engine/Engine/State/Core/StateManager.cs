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

    public StateManager(IGameState currentState, StateContext stateContext, RenderSystem renderSystem) 
    {
        _currentState = currentState;
        _currentState.OnEnter();
        _stateContext = stateContext;
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

        // Draw UI.
        foreach (IUIOverlay uIOverlay in _stateContext.UIOverlays)
        {
            uIOverlay.Update(deltaTime);
            _uiRenderSystem.Render(uIOverlay.Build(), _renderSystem, _stateContext.GameWindow.Size);
        }
        _renderSystem.Render();
    }
}