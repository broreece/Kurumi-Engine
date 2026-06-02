// External libraries.
using SFML.Graphics;

namespace Engine.Systems.Rendering.Base;

public readonly struct ActorRenderData 
{
    public required int Width { get; init; }
    public required int Height { get; init; }

    public required Texture Texture { get; init; }
    
    public required IActorAppearance Actor { get; init; }
}