// Config.
using Config.Runtime.Battle;

// Engine.
using Engine.State.States.Battle.Base;
using Engine.State.States.Battle.Text.Core;

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

    private readonly IReadOnlyDictionary<int, BattleText> _partyBattleText;
    private readonly Font _battleTextFont;

    private readonly BattleTextConfig _battleTextConfig;
    private readonly CharacterBattleSpriteConfig _characterBattleSpriteConfig;

    internal PartyBattleRenderer(
        RenderSystem renderSystem, 
        PartyMemberBattleRenderData[] partyMemberBattleRenderData, 
        IReadOnlyDictionary<int, BattleText> partyBattleText, 
        Font battleTextFont, 
        BattleTextConfig battleTextConfig, 
        CharacterBattleSpriteConfig characterBattleSpriteConfig
    )
    {
        _renderSystem = renderSystem;
        _partyMemberBattleRenderData = partyMemberBattleRenderData;
        _partyBattleText = partyBattleText;
        _battleTextFont = battleTextFont;
        _battleTextConfig = battleTextConfig;
        _characterBattleSpriteConfig = characterBattleSpriteConfig;
    }
    
    public void Update(View view, bool targetSelector, int selectedCharacterIndex, float deltaTime)
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

        foreach (KeyValuePair<int, BattleText> keyValuePair in _partyBattleText)
        {
            int characterIndex = keyValuePair.Key;
            BattleText battleText = keyValuePair.Value;
            battleText.Update(deltaTime);
            if (!battleText.Finished)
            {
                
                var xLocation = _characterBattleSpriteConfig.PartyXPlacement 
                            + (_characterBattleSpriteConfig.Width * characterIndex)
                            + _battleTextConfig.XOffset;
                var yLocation = _characterBattleSpriteConfig.PartyYPlacement + _battleTextConfig.YOffset;
                var text = new Text(battleText.Text, _battleTextFont, battleText.FontSize) 
                {
                    Position = new Vector2f(xLocation, yLocation)
                };


                _renderSystem.Submit(
                    new RenderCommand()
                    {
                        Layer = RenderLayer.UIText, 
                        SubmissionIndex = 0, 
                        Drawable = text, 
                        States = RenderStates.Default, 
                        View = view 
                    }
                );
            }
        }
    }
}