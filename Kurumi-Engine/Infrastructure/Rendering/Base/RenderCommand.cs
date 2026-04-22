using SFML.Graphics;

namespace Infrastructure.Rendering.Base;

/// <summary>
/// Contains the drawable information and the layer number to render onto.
/// </summary>
public readonly struct RenderCommand 
{
    public required int Layer { get; init; }

    public required Drawable Drawable { get; init; }
    
    public required RenderStates States { get; init; }
}