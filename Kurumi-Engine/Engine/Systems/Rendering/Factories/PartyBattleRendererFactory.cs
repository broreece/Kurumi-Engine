// Config.
using Config.Runtime.Battle;

// Data.
using Data.Runtime.Parties.Core;

// Engine.
using Engine.Assets.Base;
using Engine.Assets.Core;

using Engine.State.States.Battle.Text.Core;

using Engine.Systems.Rendering.Base;
using Engine.Systems.Rendering.Core;

// Infrastructure.
using Infrastructure.Rendering.Core;

// External libraries.
using SFML.Graphics;

namespace Engine.Systems.Rendering.Factories;

public sealed class PartyBattleRendererFactory
{
    private readonly AssetRegistry _assetRegistry;

    private readonly RenderSystem _renderSystem;

    private readonly BattleTextConfig _battleTextConfig;
    private readonly CharacterBattleSpriteConfig _characterBattleSpriteConfig;

    public PartyBattleRendererFactory(
        AssetRegistry assetRegistry, 
        RenderSystem renderSystem, 
        BattleTextConfig battleTextConfig, 
        CharacterBattleSpriteConfig characterBattleSpriteConfig
    )
    {
        _assetRegistry = assetRegistry;
        _renderSystem = renderSystem;
        _battleTextConfig = battleTextConfig;
        _characterBattleSpriteConfig = characterBattleSpriteConfig;
    }

    public PartyBattleRenderer Create(Party party, IReadOnlyDictionary<int, BattleText> partyBattleText) 
    {
        // Create and pass custom party render data.
        var partyRenderData = new PartyMemberBattleRenderData[party.Characters.Length];
        int index = 0;
        foreach (var character in party.Characters) 
        {
            if (character != null) 
            {
                partyRenderData[index] = new PartyMemberBattleRenderData() 
                {
                    Index = index,
                    Texture = new Texture(
                        _assetRegistry.GetTexture(
                        AssetType.CharacterBattleSpriteSheets, 
                        character.BattleSpriteName)
                    )
                };
            }
            index ++;
        }

        return new PartyBattleRenderer(
            _renderSystem, 
            partyRenderData, 
            partyBattleText, 
            _assetRegistry.GetFont(_battleTextConfig.BattleFontName), 
            _battleTextConfig, 
            _characterBattleSpriteConfig
        );
    }
}