using Engine.Input.Base;
using Engine.Input.Context.Base;
using Engine.State.States.Battle.Base;

namespace Engine.Input.Context.Contexts;

/// <summary>
/// Updates a provided battle window based on input's pressed
/// </summary>
public class BattleInputContext : IGameplayInputContext 
{
    private readonly IBattleMenu _battleMenu;

    public bool InteractRequested { get; set; } = false;

    public BattleInputContext(IBattleMenu battleMenu) 
    {
        _battleMenu = battleMenu;
    }

    public void Handle(InputState input)
    {
        if (input.IsPressed(InputAction.MoveUp)) 
        {
            _battleMenu.MoveUp();
        }
        if (input.IsPressed(InputAction.MoveRight)) 
        {
            _battleMenu.MoveRight();
        }
        if (input.IsPressed(InputAction.MoveDown)) 
        {
            _battleMenu.MoveDown();
        }
        if (input.IsPressed(InputAction.MoveLeft)) 
        {
            _battleMenu.MoveLeft();
        }
        if (input.IsPressed(InputAction.Confirm))
        {
            _battleMenu.Confirm();
        }
        if (input.IsPressed(InputAction.Cancel))
        {
            _battleMenu.Cancel();
        }
    }
}