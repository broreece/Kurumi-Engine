namespace Engine.Input.Scenes;

using Engine.Input.Core;
using UI.Input;
using SFML.Window;

/// <summary>
/// The menu UI input map.
/// </summary>
public sealed class MenuInputMap : InputMap {
    /// <summary>
    /// Constructor for the menu input map.
    /// </summary>
    /// <param name="choiceInputController">The menu state.</param>
    public MenuInputMap(IMenuInputController menuInputController) {
        this.menuInputController = menuInputController;
    }

    /// <summary>
    /// The menu UI button key pressed function.
    /// </summary>
    /// <param name="e"></param>
    public override void OnKeyPressed(KeyEventArgs e) {
        switch (e.Code) {
            case Keyboard.Key.Z:
                menuInputController.Select();
                break;

            case Keyboard.Key.X:
                menuInputController.Cancel();
                break;

            case Keyboard.Key.Escape:
                menuInputController.Escape();
                break;

            case Keyboard.Key.W:
                menuInputController.MoveUp();
                break;

            case Keyboard.Key.S:
                menuInputController.MoveDown();
                break;

            default:
                break;
        }
    }

    private readonly IMenuInputController menuInputController;
}