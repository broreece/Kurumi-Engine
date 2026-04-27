using Engine.Systems.Rendering.Base;
using Infrastructure.Rendering.Base;
using Infrastructure.Rendering.Core;

using SFML.Graphics;

namespace Engine.Systems.Rendering.Core;

/// <summary>
/// System that renders the battle state.
/// </summary>
public sealed class BattleRenderer 
{
    private readonly RenderSystem _renderSystem;

    private readonly Sprite _backgroundSprite;

    internal BattleRenderer(RenderSystem renderSystem, Sprite backgroundSprite)
    {
        _renderSystem = renderSystem;
        _backgroundSprite = backgroundSprite;
    }

    public void Update()
    {
        // Send to render list.
        _renderSystem.Submit(
            new RenderCommand() 
            {
                Layer = RenderLayer.BackgroundLayer, 
                Drawable = _backgroundSprite, 
                States = RenderStates.Default
            }
        );
    }
}