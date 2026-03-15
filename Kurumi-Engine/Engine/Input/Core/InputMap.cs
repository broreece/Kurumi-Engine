namespace Engine.Input.Core;

using SFML.Window;

/// <summary>
/// The abstract input map class. Each scene will have a seperate input map.
/// </summary>
public abstract class InputMap {
    /// <summary>
    /// Function used to react to any key press for each scene.
    /// </summary>
    /// <param name="e">The key pressed.</param>
    public abstract void OnKeyPressed(KeyEventArgs e);
}