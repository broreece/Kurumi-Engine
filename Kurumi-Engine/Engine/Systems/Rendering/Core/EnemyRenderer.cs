using Infrastructure.Rendering.Core;

namespace Engine.Systems.Rendering.Core;

/// <summary>
/// System that renders the enemy formation state.
/// </summary>
public sealed class EnemyRenderer 
{
    private readonly RenderSystem _renderSystem;

    internal EnemyRenderer(RenderSystem renderSystem)
    {
        _renderSystem = renderSystem;
    }

    public void Update()
    {
        
    }
}