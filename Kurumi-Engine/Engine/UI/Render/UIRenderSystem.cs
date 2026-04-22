using Engine.UI.Elements;
using Engine.UI.Layout.Base;
using Engine.UI.Layout.Core;
using Utils.Maths;

using SFML.Graphics;
using SFML.System;

namespace Enigne.UI.Render;

public sealed class UIRenderSystem 
{
    private readonly UILayoutSystem _layoutSystem;

    public UIRenderSystem(UILayoutSystem layoutSystem) 
    {
        _layoutSystem = layoutSystem;
    }

    /// <summary>
    /// Renders the UI element on the provided render target, starts recursive render element function.
    /// </summary>
    /// <param name="root">The UI element root object.</param>
    /// <param name="target">The render target being rendered on.</param>
    /// <param name="windowSize">The size of the game window.</param>
    public void Render(UIElement root, RenderTarget target, Vector2u windowSize) 
    {
        RenderElement(root, target, windowSize, new Vector2f(0, 0), new Vector2f(1, 1));
    }

    /// <summary>
    /// The recursive render element function. Loops through the UI elements adjusting position based on each UI 
    /// element's location to ensure correct placement.
    /// </summary>
    /// <param name="element">The UI element being rendered.</param>
    /// <param name="target">The render target being rendered on.</param>
    /// <param name="windowSize">The size of the game window.</param>
    /// <param name="parentPosition">The recursively changing parent position of the UI element updates based on
    /// local offset.</param>
    /// <param name="parentScale">The scale applied from the parent UI element.</param>
    private void RenderElement(
        UIElement element, 
        RenderTarget target, 
        Vector2u windowSize, 
        Vector2f parentPosition,
        Vector2f parentScale) 
    {
        // Calculate the transform.
        Vector2u contentSize = element.UIComponent.GetContentSize();
        UITransform layoutTransform = _layoutSystem.Calculate(element.Layout, windowSize, contentSize);

        // Set position to be equal to the parent position, add the current elements position and the current elements
        // offset.
        Vector2f finalPosition = parentPosition + layoutTransform.Position + element.LocalOffset;

        // If the element ignores the parent scaling do not apply parent scale.
        Vector2f finalScale = element.UIComponent.IgnoreParentScale()
            ? layoutTransform.Scale
            : VectorMultiplication.Multiple(parentScale, layoutTransform.Scale);


        // Apply the final tansform and draw.
        UITransform finalTransform = new() { Position = finalPosition, Scale = finalScale };
        element.UIComponent.Apply(finalTransform);
        element.UIComponent.Draw(target);

        // Recurse into children.
        foreach (var child in element.Children) {
            RenderElement(child, target, windowSize, finalPosition, finalScale);
        }
    }
}