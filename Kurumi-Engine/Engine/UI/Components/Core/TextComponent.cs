using Engine.UI.Components.Base;
using Engine.UI.Layout.Base;

using SFML.Graphics;
using SFML.System;

namespace Engine.UI.Components.Core;

public sealed class TextComponent : IUIComponent 
{
    private readonly Text _text;

    internal TextComponent(Text text) {
        _text = text;
    }

    public void SetText(string newText) => _text.DisplayedString = newText;

    public void Apply(UITransform transform) 
    {
        _text.Position = transform.Position;
        // Text is unaffected by scale but instead only by font size.
        _text.Scale = new Vector2f(1, 1);
    }

    public Drawable? GetDrawable() => _text;

    public Vector2u GetContentSize() 
    {
        FloatRect bounds = _text.GetLocalBounds();
        return new Vector2u((uint) bounds.Width, (uint) bounds.Height);
    }

    public bool IgnoreParentScale() => true;
}
