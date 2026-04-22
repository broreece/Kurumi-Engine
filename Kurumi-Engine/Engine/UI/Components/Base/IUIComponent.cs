using Engine.UI.Layout.Base;

using SFML.Graphics;
using SFML.System;

namespace Engine.UI.Components.Base;

/// <summary>
/// UI components are individual elements that make up a UI element.
/// </summary>
public interface IUIComponent 
{
    /// <summary>
    /// Function used to apply a UI transform to the component.
    /// </summary>
    /// <param name="transform">The UI transform object.</param>
    public void Apply(UITransform transform);

    /// <summary>
    /// Draws the UI component on the render target provided.
    /// </summary>
    /// <param name="target">The render target provided.</param>
    public void Draw(RenderTarget target);

    public Vector2u GetContentSize();

    /// <summary>
    /// If the UI component should ignore the parent UI components scale.
    /// </summary>
    /// <returns>If the UI component should ignore the parent UI components scale.</returns>
    public bool IgnoreParentScale();
}
