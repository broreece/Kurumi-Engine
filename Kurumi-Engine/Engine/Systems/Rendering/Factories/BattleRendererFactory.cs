using Engine.Assets.Core;
using Engine.Systems.Rendering.Core;
using Infrastructure.Rendering.Core;

namespace Engine.Systems.Rendering.Factories;

public sealed class BattleRendererFactory
{
    private readonly AssetRegistry _assetRegistry;

    private readonly RenderSystem _renderSystem;

    public BattleRendererFactory(AssetRegistry assetRegistry, RenderSystem renderSystem)
    {
        _assetRegistry = assetRegistry;
        _renderSystem = renderSystem;
    }

    public BattleRenderer Create() 
    {
        return new BattleRenderer(_renderSystem);
    }
}