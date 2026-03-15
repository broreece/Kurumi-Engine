namespace Engine.Input.Scenes;

using Engine.Input.Core;
using UI.Input;
using SFML.Window;

/// <summary>
/// The inventory input map.
/// </summary>
public sealed class InventoryInputMap : InputMap {
    /// <summary>
    /// Constructor for the inventory input map object.
    /// </summary>
    /// <param name="inventoryInputController">The inventory input controller.</param>
    public InventoryInputMap(IInventoryInputController inventoryInputController) {
        this.inventoryInputController = inventoryInputController;
    }

    /// <summary>
    /// The inventory input map's key press functionality.
    /// </summary>
    /// <param name="e"></param>
    public override void OnKeyPressed(KeyEventArgs e) {
        switch (e.Code) {
            case Keyboard.Key.W:
                inventoryInputController.MoveUp();
                break;

            case Keyboard.Key.D:
                inventoryInputController.MoveRight();
                break;

            case Keyboard.Key.S:
                inventoryInputController.MoveDown();
                break;

            case Keyboard.Key.A:
                inventoryInputController.MoveLeft();
                break;

            case Keyboard.Key.Z:
                inventoryInputController.Select();
                break;

            case Keyboard.Key.X:
                inventoryInputController.Cancel();
                break;

            case Keyboard.Key.Escape:
                inventoryInputController.Escape();
                break;

            default:
                break;
        }
    }

    private readonly IInventoryInputController inventoryInputController;
}
