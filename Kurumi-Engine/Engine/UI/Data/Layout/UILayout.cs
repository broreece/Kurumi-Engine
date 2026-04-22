using SFML.System;

namespace Engine.UI.Data.Content.Layout;

/// <summary>
/// Layout contains the strict rules of where the UI element is rendered to and it's size.
/// </summary>
public readonly struct UILayout 
{
    public required Vector2f Position { get; init; } 
    public required Vector2f Size { get; init; }
}
