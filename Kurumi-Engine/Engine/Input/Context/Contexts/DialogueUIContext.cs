using Engine.Input.Base;
using Engine.Input.Context.Base;
using Game.UI.Overlays.Core;

namespace Engine.Input.Context.Contexts;

public class DialogueUIContext : IInputContext 
{
    private readonly DialogueOverlay _overlay;

    public DialogueUIContext(DialogueOverlay state) 
    {
        _overlay = state;
    }

    public void Handle(InputState input) 
    {
        if (input.IsPressed(InputAction.Confirm)) 
        {
            _overlay.Advance();
        }
    }
}