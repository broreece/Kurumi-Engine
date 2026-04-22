namespace Engine.State.Base;

/// <summary>
/// Game states are the fundamental "state" of the engine where inputs are handled based on the state.
/// </summary>
public interface IGameState 
{
    public void OnEnter();

    public void OnExit();

    public void Update(float deltaTime);
}