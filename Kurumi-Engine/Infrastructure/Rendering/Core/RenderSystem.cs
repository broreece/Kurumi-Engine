using Infrastructure.Rendering.Base;

namespace Infrastructure.Rendering.Core;

public sealed class RenderSystem 
{
    private readonly GameWindow _window;
    private readonly List<RenderCommand> _commands = [];

    public RenderSystem(GameWindow window) 
    {
        _window = window;
    }

    public void Submit(RenderCommand command) => _commands.Add(command);

    /// <summary>
    /// Loops through all render commands and draws them.
    /// </summary>
    public void Render() 
    {
        // Sort queued commands.
        _commands.Sort((a, b) => a.Layer.CompareTo(b.Layer));

        _window.Clear();
        foreach (var command in _commands) 
        {
            _window.SetView(command.View);
            _window.Draw(command.Drawable, command.States);
        }
        _window.Display();
    }

    public void Clear() => _commands.Clear();
}