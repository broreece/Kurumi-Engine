using Engine.Assets.Core;
using Engine.UI.Components.Core;
using Engine.UI.Data.Content;
using Engine.UI.Data.Style;

using SFML.Graphics;

namespace Engine.UI.Components.Factories;

public sealed class TextComponentFactory
{
    private readonly AssetRegistry _assetRegistry;

    public TextComponentFactory(AssetRegistry assetRegistry)
    {
        _assetRegistry = assetRegistry;
    }

    public TextComponent Create(TextData textData, TextStyle textStyle)
    {
        var font = _assetRegistry.GetFont(textStyle.FontArt);
        var text = new Text(textData.Text, font, textStyle.FontSize);

        return new TextComponent(text);
    }
}