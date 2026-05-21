using Engine.UI.Elements;
using Engine.UI.Layout.Base;
using Engine.UI.Layout.Core;

using Infrastructure.Rendering.Base;
using Infrastructure.Rendering.Core;

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
    /// <param name="displaySize">The size of the virtual game window.</param>
    /// <param name="windowSize">The size of the real game window, used for text.</param>
    public void Render(UIElement root, RenderSystem renderSystem, Vector2u displaySize, Vector2u windowSize) 
    {
        RenderElement(root, renderSystem, displaySize, windowSize, new Vector2f(0, 0));
    }

    /// <summary>
    /// The recursive render element function. Loops through the UI elements adjusting position based on each UI 
    /// element's location to ensure correct placement.
    /// </summary>
    /// <param name="element">The UI element being rendered.</param>
    /// <param name="renderSystem">The render system.</param>
    /// <param name="displaySize">The size of the virtual game window.</param>
    /// <param name="windowSize">The size of the real game window, used for text.</param>
    /// <param name="parentPosition">The recursively changing parent position of the UI element updates based on
    /// local offset.</param>
    private void RenderElement(
        UIElement element, 
        RenderSystem renderSystem, 
        Vector2u displaySize, 
        Vector2u windowSize, 
        Vector2f parentPosition) 
    {
        // Calculate the transform.
        var component = element.UIComponent;
        bool useVirtualDisplay = component.UseVirtualDisplay();

        Vector2u contentSize = component.GetContentSize();

        UITransform layoutTransform = _layoutSystem.Calculate(
            element.Layout,
            displaySize,
            contentSize
        );

        // Final virtual position.
        Vector2f finalPosition = parentPosition + layoutTransform.Position + element.LocalOffset;

        // Convert to screen-space if required.
        Vector2f appliedPosition = useVirtualDisplay ? finalPosition : 
            VirtualToScreenPosition(finalPosition, displaySize, windowSize );

        // Text/screen-space elements should not inherit virtual scaling.
        Vector2f appliedScale = useVirtualDisplay ? layoutTransform.Scale : new Vector2f(1f, 1f);

        // Apply final transform.
        component.Apply(new UITransform()
        {
            Position = appliedPosition,
            Scale = appliedScale
        });

        var drawable = component.GetDrawable();

        if (drawable != null)
        {
            var view = useVirtualDisplay ? new View(new FloatRect(
                0,
                0,
                displaySize.X,
                displaySize.Y
            )) : new View(new FloatRect(
                0,
                0,
                windowSize.X,
                windowSize.Y
            ));

            renderSystem.Submit(new RenderCommand()
            {
                Layer = element.RenderLayer, 
                SubmissionIndex = 0, 
                States = RenderStates.Default, 
                Drawable = drawable, 
                View = view
            });
        }

        // Recurse into children.
        foreach (var child in element.Children)
        {
            RenderElement(
                child,
                renderSystem,
                displaySize,
                windowSize,
                finalPosition
            );
        }
    }

    /// <summary>
    /// Function used to transform a virtual screen location to a real screen location. Used for rendering components
    /// where they do not utilize the virtual screen.
    /// </summary>
    /// <param name="virtualPosition">The virtual screen location of the component.</param>
    /// <param name="displaySize">The size of the virtual display.</param>
    /// <param name="windowSize">The size of the real window.</param>
    /// <returns></returns>
    private static Vector2f VirtualToScreenPosition(
        Vector2f virtualPosition,
        Vector2u displaySize,
        Vector2u windowSize)
    {
        float scaleX = (float) windowSize.X / displaySize.X;
        float scaleY = (float) windowSize.Y / displaySize.Y;

        return new Vector2f(
            virtualPosition.X * scaleX,
            virtualPosition.Y * scaleY
        );
    }
}