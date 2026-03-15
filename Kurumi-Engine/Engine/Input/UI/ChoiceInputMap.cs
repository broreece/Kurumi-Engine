namespace Engine.Input.Scenes;

using Engine.Input.Core;
using UI.Input;
using SFML.Window;

/// <summary>
/// The choice UI input map.
/// </summary>
public sealed class ChoiceInputMap : InputMap {
    /// <summary>
    /// Constructor for the choice input map.
    /// </summary>
    /// <param name="choiceInputController">The choice state.</param>
    public ChoiceInputMap(IChoiceInputController choiceInputController) {
        this.choiceInputController = choiceInputController;
    }

    /// <summary>
    /// The choice UI button key pressed function.
    /// </summary>
    /// <param name="e"></param>
    public override void OnKeyPressed(KeyEventArgs e) {
        switch (e.Code) {
            case Keyboard.Key.Z:
                choiceInputController.Select();
                break;

            case Keyboard.Key.W:
                choiceInputController.MoveUp();
                break;

            case Keyboard.Key.S:
                choiceInputController.MoveDown();
                break;

            default:
                break;
        }
    }

    private readonly IChoiceInputController choiceInputController;
}