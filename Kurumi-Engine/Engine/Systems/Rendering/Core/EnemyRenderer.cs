using Config.Runtime.Battle;

using Data.Runtime.Formations.Core;

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

    private readonly Formation _formation;
    private readonly IReadOnlyList<EnemyRenderData> _enemyRenderData;

    private readonly EnemyBattleSpriteConfig _enemyBattleSpriteConfig;

    internal EnemyRenderer(
        RenderSystem renderSystem, 
        Formation formation,
        IReadOnlyList<EnemyRenderData> enemyRenderData,
        EnemyBattleSpriteConfig enemyBattleSpriteConfig)
    {
        _renderSystem = renderSystem;
        _formation = formation;
        _enemyRenderData = enemyRenderData;
        _enemyBattleSpriteConfig = enemyBattleSpriteConfig;
    }

    public void Update(View view, bool targetSelector, int selectedEnemyIndex)
    {
        var currentEnemyIndex = 0;
        foreach (var enemyRenderData in _enemyRenderData)
        {
            if (_formation.GetEntityAt(currentEnemyIndex).CurrentHP > 0)
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

                // Send to render list.
                RenderStates renderState = selectedEnemyIndex == currentEnemyIndex && targetSelector ? 
                    new(BlendMode.Add) : RenderStates.Default;
                _renderSystem.Submit(new RenderCommand() 
                {
                    Layer = RenderLayer.BaseEnemyLayer, 
                    Drawable = enemySprite, 
                    States = renderState,
                    View = view
                });
            }
            currentEnemyIndex ++;
        }
    }
}