namespace Engine.Input.Context.Base;

/// <summary>
/// Allow access to if the confirm action is queued.
/// </summary>
public interface IGameplayInputContext : IInputContext 
{
    public bool InteractRequested { get; }
}