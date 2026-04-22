using Engine.UI.Components.Core;
using Engine.UI.Data.Content;
using Engine.UI.Data.Style;

using SFML.Graphics;

namespace Engine.UI.Components.Factories;

public sealed class TextComponentFactory
{
    private readonly IReadOnlyDictionary<string, string> _fontFileNames;

    public TextComponentFactory(IReadOnlyDictionary<string, string> fontFileNames)
    {
        _fontFileNames = fontFileNames;
    }

    public TextComponent Create(TextData textData, TextStyle textStyle)
    {
        var font = new Font(_fontFileNames[textStyle.FontArt]);
        var text = new Text(textData.Text, font, textStyle.FontSize);

        return new TextComponent(text);
    }
}