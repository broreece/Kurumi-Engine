using Config.Runtime.Battle;
using Engine.Systems.Rendering.Base;
using Infrastructure.Rendering.Base;
using Infrastructure.Rendering.Core;

using SFML.Graphics;
using SFML.System;

namespace Engine.Systems.Rendering.Core;

/// <summary>
/// System that renders the enemy formation state.
/// </summary>
public sealed class EnemyRenderer 
{
    private readonly RenderSystem _renderSystem;

    private readonly IReadOnlyList<EnemyRenderData> _enemyRenderData;

    private readonly EnemyBattleSpriteConfig _enemyBattleSpriteConfig;

    internal EnemyRenderer(
        RenderSystem renderSystem, 
        IReadOnlyList<EnemyRenderData> enemyRenderData,
        EnemyBattleSpriteConfig enemyBattleSpriteConfig)
    {
        _renderSystem = renderSystem;
        _enemyRenderData = enemyRenderData;
        _enemyBattleSpriteConfig = enemyBattleSpriteConfig;
    }

    public void Update()
    {
        foreach (var enemyRenderData in _enemyRenderData)
        {
            var enemySprite = new Sprite(enemyRenderData.Texture)
            {
                TextureRect = new IntRect(
                    0,
                    0,
                    _enemyBattleSpriteConfig.Width,
                    _enemyBattleSpriteConfig.Height
                ),
                Position = new Vector2f(enemyRenderData.XLocation, enemyRenderData.YLocation)
            };
            _renderSystem.Submit(
                new RenderCommand() 
                {
                    Layer = (int) RenderLayer.BaseEnemyLayer, 
                    Drawable = enemySprite, 
                    States = RenderStates.Default
                }
            );
        }
    }
}