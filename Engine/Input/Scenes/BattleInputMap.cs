namespace Engine.Input.Scenes;

using Engine.Input.Core;
using States.Battle.Input;
using SFML.Window;

/// <summary>
/// The battle scene input map.
/// </summary>
public sealed class BattleInputMap : InputMap {
    /// <summary>
    /// Constructor for the battle scene input map object.
    /// </summary>
    /// <param name="battleInputController">The battle input controller.</param>
    public BattleInputMap(IBattleInputController battleInputController) {
        this.battleInputController = battleInputController;
    }

    /// <summary>
    /// The battle scene input battle's key press functionality.
    /// </summary>
    /// <param name="e"></param>
    public override void OnKeyPressed(KeyEventArgs e) {
        switch (e.Code) {
            case Keyboard.Key.W:
                battleInputController.MoveUp();
                break;

            case Keyboard.Key.A:
                battleInputController.MoveLeft();
                break;

            case Keyboard.Key.S:
                battleInputController.MoveDown();
                break;

            case Keyboard.Key.D:
                battleInputController.MoveRight();
                break;

            case Keyboard.Key.Z:
                battleInputController.Select();
                break;

            case Keyboard.Key.X:
                battleInputController.Cancel();
                break;

            case Keyboard.Key.Escape:
                battleInputController.Cancel();
                break;

            default:
                break;
        }
    }

    private readonly IBattleInputController battleInputController;
}
