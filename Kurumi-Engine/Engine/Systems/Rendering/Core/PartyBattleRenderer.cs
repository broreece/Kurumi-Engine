using Config.Runtime.Battle;
using Engine.Systems.Rendering.Base;
using Infrastructure.Rendering.Base;
using Infrastructure.Rendering.Core;

using SFML.Graphics;
using SFML.System;

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
    
    public void Update(View view, bool targetSelector, int selectedCharacterIndex)
    {
        var currentCharacterIndex = 0;
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
                    ),
                    Position = new Vector2f(
                        // TODO: Swap to scaled width here.
                        _characterBattleSpriteConfig.PartyXPlacement 
                            + (_characterBattleSpriteConfig.Width * partyMemberRender.Index),
                        _characterBattleSpriteConfig.PartyYPlacement
                    )
                };

                // Send to render list.
                RenderStates renderState = selectedCharacterIndex == currentCharacterIndex && targetSelector ? 
                    new(BlendMode.Add) : RenderStates.Default;
                _renderSystem.Submit(new RenderCommand() 
                {
                    Layer = RenderLayer.PartyBattleLayer, 
                    Drawable = sprite, 
                    States = renderState,
                    View = view
                });

                currentCharacterIndex ++;
            }
        }
    }
}