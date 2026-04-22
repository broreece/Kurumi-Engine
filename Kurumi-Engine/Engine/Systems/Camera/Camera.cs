using SFML.Graphics;
using SFML.System;

namespace Engine.Systems.Camera;

/// <summary>
/// Contains a view and functions that alter the view.
/// </summary>
public sealed class Camera 
{
    public View View { get; }

    public Camera(float width, float height) 
    {
        View = new View(new FloatRect(0, 0, width, height));
    }

    public void Follow(float targetX, float targetY, float mapPixelWidth, float mapPixelHeight) 
    {
        Vector2f halfSize = View.Size / 2f;
        float viewWidth = View.Size.X;
        float viewHeight = View.Size.Y;
        float finalX, finalY;

        // X axis.
        if (mapPixelWidth <= viewWidth) 
        {
            finalX = mapPixelWidth / 2f;
        }
        else {
            float minX = halfSize.X;
            float maxX = mapPixelWidth - halfSize.X;
            finalX = Math.Clamp(targetX, minX, maxX);
        }

        // Y axis.
        if (mapPixelHeight <= viewHeight) 
        {
            finalY = mapPixelHeight / 2f;
        }
        else {
            float minY = halfSize.Y;
            float maxY = mapPixelHeight - halfSize.Y;
            finalY = Math.Clamp(targetY, minY, maxY);
        }

        View.Center = new Vector2f(finalX, finalY);
    }
}