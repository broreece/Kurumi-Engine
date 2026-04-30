using Engine.UI.Elements;
using Engine.UI.Layout.Base;
using Engine.UI.Layout.Core;

using Infrastructure.Rendering.Base;
using Infrastructure.Rendering.Core;

using Utils.Maths;

using SFML.Graphics;
using SFML.System;

namespace Engine.UI.Render;

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
    /// <param name="renderSystem">The render system.</param>
    /// <param name="windowSize">The size of the game window.</param>
    public void Render(UIElement root, RenderSystem renderSystem, Vector2u windowSize) 
    {
        RenderElement(root, renderSystem, windowSize, new Vector2f(0, 0), new Vector2f(1, 1));
    }

    /// <summary>
    /// The recursive render element function. Loops through the UI elements adjusting position based on each UI 
    /// element's location to ensure correct placement.
    /// </summary>
    /// <param name="element">The UI element being rendered.</param>
    /// <param name="renderSystem">The render system.</param>
    /// <param name="windowSize">The size of the game window.</param>
    /// <param name="parentPosition">The recursively changing parent position of the UI element updates based on
    /// local offset.</param>
    /// <param name="parentScale">The scale applied from the parent UI element.</param>
    private void RenderElement(
        UIElement element, 
        RenderSystem renderSystem, 
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
        Vector2f safeScale = new(
            float.IsFinite(layoutTransform.Scale.X) ? layoutTransform.Scale.X : 1f,
            float.IsFinite(layoutTransform.Scale.Y) ? layoutTransform.Scale.Y : 1f
        );
        Vector2f finalScale = element.UIComponent.IgnoreParentScale()
            ? safeScale
            : VectorMultiplication.Multiple(parentScale, safeScale);

        // Apply the final tansform and draw.
        UITransform finalTransform = new() { Position = finalPosition, Scale = finalScale };
        element.UIComponent.Apply(finalTransform);
        
        var drawable = element.UIComponent.GetDrawable();
        if (drawable != null) {
            renderSystem.Submit(new RenderCommand() 
            { 
                Layer = element.RenderLayer, 
                States = RenderStates.Default, 
                Drawable = drawable,
                View = new View(new FloatRect(0, 0, windowSize.X, windowSize.Y))
            });
        }

        // Recurse into children.
        foreach (var child in element.Children) {
            RenderElement(child, renderSystem, windowSize, finalPosition, finalScale);
        }
    }
}