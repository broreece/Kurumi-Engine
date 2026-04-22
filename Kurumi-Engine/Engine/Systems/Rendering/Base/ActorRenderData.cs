using Data.Runtime.Actors.Core;

using SFML.Graphics;

namespace Engine.Systems.Rendering.Base;

public sealed class ActorRenderData 
{
    public required int Width { get; init; }
    public required int Height { get; init; }

    public required Texture Texture { get; init; }
    
    public required Actor Actor { get; init; }
}