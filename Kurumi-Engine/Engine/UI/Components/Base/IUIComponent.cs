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
    /// Returns the component in a drawable form, or null if none exists.
    /// </summary>
    public Drawable? GetDrawable();

    public Vector2u GetContentSize();
}
