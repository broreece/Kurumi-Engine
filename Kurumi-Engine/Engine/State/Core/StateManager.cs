using Engine.State.Base;

namespace Engine.State.Core;

/// <summary>
/// Holds the current state and allows changing states or updating the current state.
/// </summary>
public sealed class StateManager 
{
    private IGameState _currentState;

    public StateManager(IGameState currentState) 
    {
        _currentState = currentState;
        _currentState.OnEnter();
    }

    public void ChangeState(IGameState newState) 
    {
        _currentState.OnExit();
        _currentState = newState;
        _currentState.OnEnter();
    }

    public void Update(float deltaTime) => _currentState.Update(deltaTime);
}