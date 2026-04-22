using Engine.Input.Base;

namespace Engine.Input.Context.Base;

/// <summary>
/// Input contexts contain varying handle methods for the games current input state.
/// </summary>
public interface IInputContext 
{
    public void Handle(InputState inputState);
}