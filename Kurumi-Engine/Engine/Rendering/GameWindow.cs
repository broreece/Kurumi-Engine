namespace Engine.Rendering;

using Config.Runtime.Game;
using Config.Runtime.Map;
using Engine.Input.Core;
using UI.Interfaces;
using SFML.Graphics;
using SFML.Window;

/// <summary>
/// The game window class. The game window stores the render window and window variables.
/// </summary>
public sealed class GameWindow : IGameWindowScaleAccessor {
    /// <summary>
    /// Constructor that builds the games render window.
    /// </summary>
    /// <param name="gameWindowConfig">The game window config object.</param>
    /// <param name="mapConfig">The map config object.</param>
    /// <param name="tileSheetConfig">The tile sheet config object.</param>
    public GameWindow(GameWindowConfig gameWindowConfig, MapConfig mapConfig, TileSheetConfig tileSheetConfig) {
        // Load re-used config.
        windowWidth = gameWindowConfig.GetWindowWidth();
        windowHeight = gameWindowConfig.GetWindowHeight();

        // Create global render window.
        VideoMode mode = new((uint) windowWidth, (uint) windowHeight);
        window = new(mode, gameWindowConfig.GetWindowTitle(), Styles.Titlebar | Styles.Close );
        window.SetVerticalSyncEnabled(true);

        // Create and set game window icon.
        string iconPath = Path.Combine(
            AppContext.BaseDirectory,
            "Assets",
            "Icons",
            "window_icon.png"
        );
        Image icon = new(iconPath);
        window.SetIcon(icon.Size.X, icon.Size.Y, icon.Pixels);

        // Set controls.
        window.Closed += (sender, args) => window.Close();
        window.KeyPressed += OnKeyPressed;
        
        // Set width and height scale.
        widthScale = (float) windowWidth / (mapConfig.GetMaxTilesWide() * tileSheetConfig.GetTileWidth());
        heightScale = (float) windowHeight / (mapConfig.GetMaxTilesHigh() * tileSheetConfig.GetTileHeight());

        // Set input to not be frozen and null the input map.
        inputMap = null;
        inputFrozen = false;
    }

    /// <summary>
    /// Setter for the input map.
    /// </summary>
    /// <param name="inputMap">The input map that determines the controls.</param>
    public void SetInputMap(InputMap inputMap) {
        this.inputMap = inputMap;
    }

    /// <summary>
    /// Function used to draw a sprite onto the render window.
    /// </summary>
    /// <param name="sprite">The sprite parameter to be drawn.</param>
    public void Draw(Drawable sprite) {
        window.Draw(sprite);
    }

    /// <summary>
    /// Function used to draw a sprite onto the render window. Containing additional render states.
    /// </summary>
    /// <param name="sprite">The sprite parameter to be drawn.</param>
    /// <param name="renderStates">The render states parameter to be added to the sprite.</param>
    public void Draw(Drawable sprite, RenderStates renderStates) {
        window.Draw(sprite, renderStates);
    }

    /// <summary>
    /// Function used to display the render window with updated sprites.
    /// </summary>
    public void Display() {
        window.Display();
    }

    /// <summary>
    /// Function used to clear the render windows display.
    /// </summary>
    public void Clear() {
        window.Clear();
    }

    /// <summary>
    /// Freezes the input.
    /// </summary>
    public void FreezeInput() {
        inputFrozen = true;
    }

    /// <summary>
    /// Resumes the input.
    /// </summary>
    public void ResumeInput() {
        inputFrozen = false;
    }

    /// <summary>
    /// Function used to dispatch any events (Key presses).
    /// </summary>
    public void DispatchEvents() {
        window.DispatchEvents();
    }

    /// <summary>
    /// Getter for the width of the game window.
    /// </summary>
    /// <returns>The width of the game window.</returns>
    public int GetWidth() {
        return (int) window.Size.X;
    }

    /// <summary>
    /// Gets the width scale, used to scale all scenes sprites.
    /// </summary>
    /// <returns>The scale scenes will use.</returns>
    public float GetWidthScale() {
        return widthScale;
    }

    /// <summary>
    /// Getter for the height of the game window.
    /// </summary>
    /// <returns>The height of the game window.</returns>
    public int GetHeight() {
        return (int) window.Size.Y;
    }

    /// <summary>
    /// Gets the height scale, used to scale all scenes sprites.
    /// </summary>
    /// <returns>The scale scenes will use.</returns>
    public float GetHeightScale() {
        return heightScale;
    }

    /// <summary>
    /// Getter for if the input should be frozen.
    /// </summary>
    /// <returns>If the input is frozen.</returns>
    public bool InputFrozen() {
        return inputFrozen;
    }

    /// <summary>
    /// Function that checks if the game window is currently open or not.
    /// </summary>
    /// <returns>If the game window is open.</returns>
    public bool IsOpen() {
        return window.IsOpen;
    }

    /// <summary>
    /// The game windows on key press function.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnKeyPressed(object? sender, KeyEventArgs e) {
        if(!inputFrozen && inputMap != null) {
            inputMap.OnKeyPressed(e);
        }
    }

    // Re-used config elements:
    private readonly int windowWidth, windowHeight;

    // Window information.
    private readonly float widthScale, heightScale;
    private readonly RenderWindow window;

    // Input variables.
    private InputMap ? inputMap;
    private bool inputFrozen;
}
