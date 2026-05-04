using Game.Scripts.Context.Core;

namespace Engine.State.Base;

/// <summary>
/// Game states are the fundamental "state" of the engine contains functionality for creation/finishing and updates.
/// </summary>
public interface IGameState 
{
    public void OnEnter();

    public void OnExit();

    public void Update(float deltaTime);

    public ScriptContext GetScriptContext();
}