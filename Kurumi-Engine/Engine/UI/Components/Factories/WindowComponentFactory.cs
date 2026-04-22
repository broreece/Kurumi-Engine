using Engine.UI.Components.Core;
using Engine.UI.Data.Style;
using SFML.Graphics;

namespace Engine.UI.Components.Factories;

public sealed class WindowComponentFactory
{
    private readonly IReadOnlyDictionary<string, string> _windowFileNames;

    public WindowComponentFactory(IReadOnlyDictionary<string, string> windowFileNames)
    {
        _windowFileNames = windowFileNames;
    }

    public WindowComponent Create(WindowStyle windowStyle)
    {
        var sprite = new Sprite(new Texture(_windowFileNames[windowStyle.WindowArt]));

        return new WindowComponent(sprite);
    }
}