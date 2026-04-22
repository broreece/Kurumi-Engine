using Engine.UI.Components.Base;
using Engine.UI.Layout.Base;

using SFML.Graphics;
using SFML.System;

namespace Engine.UI.Components.Core;

/// <summary>
/// The empty component class used as a parent to a set of children elements.
/// </summary>
public sealed class EmptyComponent : IUIComponent 
{
    public void Apply(UITransform transform) {}

    public void Draw(RenderTarget target) {}

    public Vector2u GetContentSize() => new(0, 0);

    public bool IgnoreParentScale() => true;
}