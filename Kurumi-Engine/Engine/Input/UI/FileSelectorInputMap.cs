namespace Engine.Input.Scenes;

using Engine.Input.Core;
using UI.Input;
using SFML.Window;

/// <summary>
/// The file selector UI input map.
/// </summary>
public sealed class FileSelectorInputMap : InputMap {
    /// <summary>
    /// Constructor for the file selector input map.
    /// </summary>
    /// <param name="choiceInputController">The file selector state.</param>
    public FileSelectorInputMap(IFileSelectorInputController fileSelectorInputController) {
        this.fileSelectorInputController = fileSelectorInputController;
    }

    /// <summary>
    /// The file selector UI button key pressed function.
    /// </summary>
    /// <param name="e"></param>
    public override void OnKeyPressed(KeyEventArgs e) {
        switch (e.Code) {
            case Keyboard.Key.Z:
                fileSelectorInputController.Select();
                break;

            case Keyboard.Key.X:
                fileSelectorInputController.Cancel();
                break;

            case Keyboard.Key.W:
                fileSelectorInputController.MoveUp();
                break;

            case Keyboard.Key.S:
                fileSelectorInputController.MoveDown();
                break;

            default:
                break;
        }
    }

    private readonly IFileSelectorInputController fileSelectorInputController;
}