using Engine.Assets.Core;
using Engine.Systems.Rendering.Core;
using Infrastructure.Rendering.Core;

namespace Engine.Systems.Rendering.Factories;

public sealed class EnemyRendererFactory
{
    private readonly AssetRegistry _assetRegistry;

    private readonly RenderSystem _renderSystem;

    public EnemyRendererFactory(AssetRegistry assetRegistry, RenderSystem renderSystem)
    {
        _assetRegistry = assetRegistry;
        _renderSystem = renderSystem;
    }

    public EnemyRenderer Create() 
    {
        return new EnemyRenderer(_renderSystem);
    }
}