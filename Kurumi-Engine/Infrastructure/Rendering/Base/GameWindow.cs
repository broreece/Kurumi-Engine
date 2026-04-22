using Config.Runtime.Game;

using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Infrastructure.Rendering.Base;

/// <summary>
/// Exposes the render window object via functions.
/// </summary>
public sealed class GameWindow 
{
    private readonly RenderWindow _window;

    public GameWindow(GameWindowConfig gameWindowConfig) 
    {
        var mode = new VideoMode((uint) gameWindowConfig.Width, (uint) gameWindowConfig.Height);
        _window = new RenderWindow(mode, gameWindowConfig.Title, Styles.Titlebar | Styles.Close);
        _window.SetVerticalSyncEnabled(true);

        _window.Closed += (_, _) => _window.Close();

        SetIcon();
    }

    private void SetIcon() 
    {
        var iconPath = Path.Combine(
            AppContext.BaseDirectory,
            "Assets",
            "Icons",
            "window_icon.png"
        );
        var icon = new Image(iconPath);
        _window.SetIcon(icon.Size.X, icon.Size.Y, icon.Pixels);
    }

    public void Clear() => _window.Clear();

    public void Draw(Drawable drawable, RenderStates states) => _window.Draw(drawable, states);

    public void Display() => _window.Display();

    public void DispatchEvents() => _window.DispatchEvents();

    public void SetView(View? view) => _window.SetView(view);

    public bool IsOpen() => _window.IsOpen;

    public Vector2u GetSize() => _window.Size;
}