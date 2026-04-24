using Config.Runtime.Battle;
using Engine.Systems.Rendering.Base;
using Infrastructure.Rendering.Base;
using Infrastructure.Rendering.Core;

using SFML.Graphics;

namespace Engine.Systems.Rendering.Core;

/// <summary>
/// System that renders the party battle state.
/// </summary>
public sealed class PartyBattleRenderer 
{
    private readonly RenderSystem _renderSystem;

    private readonly PartyMemberBattleRenderData[] _partyMemberBattleRenderData;

    private readonly CharacterBattleSpriteConfig _characterBattleSpriteConfig;

    internal PartyBattleRenderer(
        RenderSystem renderSystem, 
        PartyMemberBattleRenderData[] partyMemberBattleRenderData, 
        CharacterBattleSpriteConfig characterBattleSpriteConfig)
    {
        _renderSystem = renderSystem;
        _partyMemberBattleRenderData = partyMemberBattleRenderData;
        _characterBattleSpriteConfig = characterBattleSpriteConfig;
    }
    
    public void Update()
    {
        foreach (var partyMemberRender in _partyMemberBattleRenderData) 
        {
            if (partyMemberRender != null) 
            {
                var sprite = new Sprite(partyMemberRender.Texture)
                {
                    TextureRect = new IntRect(
                        0,
                        0,
                        _characterBattleSpriteConfig.Width,
                        _characterBattleSpriteConfig.Height
                    )
                };
                // TODO: (SPBS-01) Create and set positioning here utilizing the party member render data.
                // Send to render list.
                _renderSystem.Submit(
                    new RenderCommand() 
                    {
                        Layer = (int) RenderLayer.PartyBattleLayer, 
                        Drawable = sprite, 
                        States = RenderStates.Default
                    }
                );
            }
        }
    }
}