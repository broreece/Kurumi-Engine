namespace Engine.Input.Scenes;

using Engine.Input.Core;
using States.Map.Input;
using SFML.Window;

/// <summary>
/// The map scene input map.
/// </summary>
public sealed class MapInputMap : InputMap {
    /// <summary>
    /// Constructor for the map scene input map object.
    /// </summary>
    /// <param name="mapInputController">The map input controller.</param>
    public MapInputMap(IMapInputController mapInputController) {
        this.mapInputController = mapInputController;
    }

    /// <summary>
    /// The map scene input map's key press functionality.
    /// </summary>
    /// <param name="e"></param>
    public override void OnKeyPressed(KeyEventArgs e) {
        switch (e.Code) {
            case Keyboard.Key.W:
                mapInputController.MoveUp();
                break;

            case Keyboard.Key.D:
                mapInputController.MoveRight();
                break;

            case Keyboard.Key.S:
                mapInputController.MoveDown();
                break;

            case Keyboard.Key.A:
                mapInputController.MoveLeft();
                break;

            case Keyboard.Key.Z:
                mapInputController.Select();
                break;

            case Keyboard.Key.Escape:
                mapInputController.Escape();
                break;

            default:
                break;
        }
    }

    private readonly IMapInputController mapInputController;
}
