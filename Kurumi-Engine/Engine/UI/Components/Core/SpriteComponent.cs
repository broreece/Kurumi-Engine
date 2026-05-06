using Engine.UI.Components.Base;
using Engine.UI.Layout.Base;

using SFML.Graphics;
using SFML.System;

namespace Engine.UI.Components.Core;

public sealed class SpriteComponent : IUIComponent 
{
    private readonly Sprite _sprite;

    internal SpriteComponent(Texture texture) 
    {
        _sprite = new Sprite(texture);
    }

    public void Apply(UITransform transform) 
    {
        _sprite.Position = transform.Position;
        _sprite.Scale = transform.Scale;
    }

    public Drawable? GetDrawable() => _sprite;

    public Vector2u GetContentSize() => _sprite.Texture.Size;
}
