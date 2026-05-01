using Engine.Input.Base;
using Engine.Input.Context.Base;

namespace Engine.Input.Context.Contexts;

/// <summary>
/// Updates a provided battle window based on input's pressed
/// </summary>
public class BattleInputContext : IGameplayInputContext 
{
    public bool InteractRequested { get; set; } = false;

    public void Handle(InputState input)
    {
        // TODO: (BS-01) Implement key presses affecting the UI here.
        InteractRequested = input.IsPressed(InputAction.Confirm);
    }
}