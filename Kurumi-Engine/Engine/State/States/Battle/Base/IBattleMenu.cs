namespace Engine.State.States.Battle.Base;

/// <summary>
/// Interface used for the menu actions in the battle state.
/// </summary>
public interface IBattleMenu {
    public void MoveUp();

    public void MoveDown();

    public void MoveRight();

    public void MoveLeft();

    public void Confirm();

    public void Cancel();
}