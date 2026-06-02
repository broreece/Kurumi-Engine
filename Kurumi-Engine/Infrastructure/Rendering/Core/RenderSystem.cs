// Infrastructure.
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
        // Sort queued commands based on rendering layer and then the submission index (for tiles).
        _commands.Sort((a, b) => {
            int layerCompare = a.Layer.CompareTo(b.Layer);
        
            if (layerCompare != 0) 
            {
                return layerCompare;
            }

            return a.SubmissionIndex.CompareTo(b.SubmissionIndex);
        });

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