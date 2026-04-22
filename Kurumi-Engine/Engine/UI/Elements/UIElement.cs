using Engine.UI.Components.Base;
using Engine.UI.Data.Content.Layout;

using SFML.System;

namespace Engine.UI.Elements;

/// <summary>
/// Contains the component, layout, local offset provided by parent and children of the UI element.
/// </summary>
public readonly struct UIElement {
    public required IUIComponent UIComponent { get; init; }

    public required UILayout Layout { get; init; }

    public required Vector2f LocalOffset { get; init; }

    public required IReadOnlyList<UIElement> Children { get; init; }
}