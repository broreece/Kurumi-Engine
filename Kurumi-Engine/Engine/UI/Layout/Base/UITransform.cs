using SFML.System;

namespace Engine.UI.Layout.Base;

/// <summary>
/// Provides a position and scale to enable transformation of UI components.
/// </summary>
public readonly struct UITransform {
    public required Vector2f Position { get; init; } 
    public required Vector2f Scale { get; init; }
}