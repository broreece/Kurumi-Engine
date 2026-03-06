namespace Engine.Input.Scenes;

using Engine.Input.Core;
using UI.Input;
using SFML.Window;

/// <summary>
/// The dialogue UI input map.
/// </summary>
public sealed class DialogueInputMap : InputMap {
    /// <summary>
    /// Constructor for the dialogue input map.
    /// </summary>
    /// <param name="dialogueInputController">The dialogue state.</param>
    public DialogueInputMap(IDialogueInputController dialogueInputController) {
        this.dialogueInputController = dialogueInputController;
    }

    /// <summary>
    /// The dialogue UI button key pressed function.
    /// </summary>
    /// <param name="e"></param>
    public override void OnKeyPressed(KeyEventArgs e) {
        switch (e.Code) {
            case Keyboard.Key.Z:
                dialogueInputController.Select();
                break;

            default:
                break;
        }
    }

    private readonly IDialogueInputController dialogueInputController;
}