using Engine.Input.Base;
using Engine.Input.Context.Base;
using Game.UI.States;

namespace Engine.Input.Context.Contexts;

public class DialogueUIContext : IInputContext 
{
    private readonly DialogueState _state;

    public DialogueUIContext(DialogueState state) 
    {
        _state = state;
    }

    public void Handle(InputState input) 
    {
        if (input.IsPressed(InputAction.Confirm)) 
        {
            _state.Advance();
        }
    }
}