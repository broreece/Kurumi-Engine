using Config.Runtime.Battle;
using Data.Runtime.Formations.Core;
using Engine.Assets.Base;
using Engine.Assets.Core;
using Engine.Systems.Rendering.Base;
using Engine.Systems.Rendering.Core;
using Infrastructure.Rendering.Core;

namespace Engine.Systems.Rendering.Factories;

public sealed class EnemyRendererFactory
{
    private readonly AssetRegistry _assetRegistry;

    private readonly RenderSystem _renderSystem;

    private readonly EnemyBattleSpriteConfig _enemyBattleSpriteConfig;

    public EnemyRendererFactory(
        AssetRegistry assetRegistry, 
        RenderSystem renderSystem, 
        EnemyBattleSpriteConfig enemyBattleSpriteConfig)
    {
        _assetRegistry = assetRegistry;
        _renderSystem = renderSystem;
        _enemyBattleSpriteConfig = enemyBattleSpriteConfig;
    }

    public EnemyRenderer Create(Formation formation) 
    {
        var enemyRenderData = new List<EnemyRenderData>();
        foreach (var storedEntityData in formation.StoredEntityData) 
        {
            var enemyTexture = _assetRegistry.GetTexture(
                AssetType.EnemyBattleSprites, 
                storedEntityData.Entity.SpriteName
            );
            var xLocation = storedEntityData.XLocation;
            var yLocation = storedEntityData.YLocation;
            enemyRenderData.Add(new EnemyRenderData() 
            { 
                XLocation = xLocation, 
                YLocation = yLocation, 
                Texture = enemyTexture 
            });
        }

        return new EnemyRenderer(_renderSystem, enemyRenderData, _enemyBattleSpriteConfig);
    }
}