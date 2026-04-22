using Engine.UI.Components.Base;
using Engine.UI.Layout.Base;

using SFML.Graphics;
using SFML.System;

namespace Engine.UI.Components.Core;

public sealed class WindowComponent : IUIComponent 
{
    private readonly Sprite _sprite;

    internal WindowComponent(Sprite sprite) 
    {
        _sprite = sprite;
    }

    public void Apply(UITransform transform) 
    {
        _sprite.Position = transform.Position;
        _sprite.Scale = transform.Scale;
    }

    public void Draw(RenderTarget target) => target.Draw(_sprite);

    public Vector2u GetContentSize() => _sprite.Texture.Size;

    public bool IgnoreParentScale() => false;
}
