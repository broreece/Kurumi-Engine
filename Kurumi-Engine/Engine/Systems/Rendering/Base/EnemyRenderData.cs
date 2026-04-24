using SFML.Graphics;

namespace Engine.Systems.Rendering.Base;

public readonly struct EnemyRenderData 
{
    public required int XLocation { get; init; }
    public required int YLocation { get; init; }

    public required Texture Texture { get; init; }
}