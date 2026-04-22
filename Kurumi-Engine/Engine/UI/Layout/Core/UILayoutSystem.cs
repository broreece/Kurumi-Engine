using Engine.UI.Data.Content.Layout;
using Engine.UI.Layout.Base;

using SFML.System;

namespace Engine.UI.Layout.Core;

public sealed class UILayoutSystem 
{
    public UITransform Calculate(UILayout layout, Vector2u windowSize, Vector2u contentSize) 
    {
        // Position is already in pixels.
        Vector2f pixelPosition = layout.Position;

        // Convert percent to pixel size.
        Vector2f pixelSize = new(
            windowSize.X * (layout.Size.X / 100f),
            windowSize.Y * (layout.Size.Y / 100f)
        );

        // Compute scale from content size.
        Vector2f scale = new(
            pixelSize.X / contentSize.X,
            pixelSize.Y / contentSize.Y
        );

        return new UITransform() { Position = pixelPosition, Scale = scale };
    }
}