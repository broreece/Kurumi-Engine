namespace Engine.Input.Context.Base;

/// <summary>
/// Allows access to actions related to universal button inputs.
/// </summary>
public interface IGameplayInputContext : IInputContext 
{
    public bool InteractRequested { get; set; }
}