// Config.
using Config.Runtime.Battle;

// Engine.
using Engine.State.States.Battle.Base;

using Engine.Systems.Rendering.Base;

// Infrastructure.
using Infrastructure.Rendering.Base;
using Infrastructure.Rendering.Core;

// External libraries.
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
                        _characterBattleSpriteConfig.PartyXPlacement 
                            + (_characterBattleSpriteConfig.Width * partyMemberRender.Index),
                        _characterBattleSpriteConfig.PartyYPlacement
                    ),
                    Scale = new Vector2f(
                        _characterBattleSpriteConfig.WidthScale,
                        _characterBattleSpriteConfig.HeightScale
                    )
                };

                // Apply render state based on if selected.
                // Array of party selection indexes.
                int[] partyHighlightedStates = [
                    (int) BattleTargets.RandomPartyMember, 
                    (int) BattleTargets.AllPartyAndAllEnemies, 
                    (int) BattleTargets.AllPartyMembers
                ];
                RenderStates renderState;
                if (partyHighlightedStates.Contains(selectedCharacterIndex))
                {
                    renderState = new(BlendMode.Add);
                }
                // Check if the individual party member is selected.
                else 
                {
                    renderState = selectedCharacterIndex == currentCharacterIndex && targetSelector ? 
                        new(BlendMode.Add) : RenderStates.Default;
                }

                _renderSystem.Submit(new RenderCommand() 
                {
                    Layer = RenderLayer.PartyBattleLayer, 
                    SubmissionIndex = 0, 
                    Drawable = sprite, 
                    States = renderState,
                    View = view
                });

                currentCharacterIndex ++;
            }
        }
    }
}