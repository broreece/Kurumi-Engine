using Infrastructure.Rendering.Core;

namespace Engine.Systems.Rendering.Core;

/// <summary>
/// System that renders the battle state.
/// </summary>
public sealed class BattleRenderer 
{
    private readonly RenderSystem _renderSystem;

    internal BattleRenderer(RenderSystem renderSystem)
    {
        _renderSystem = renderSystem;
    }

    public void Update()
    {
        
    }
}