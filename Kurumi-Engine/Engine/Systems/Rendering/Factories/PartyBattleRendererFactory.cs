using Data.Runtime.Party.Core;
using Engine.Assets.Base;
using Engine.Assets.Core;
using Engine.Systems.Rendering.Base;
using Engine.Systems.Rendering.Core;
using Infrastructure.Rendering.Core;

using SFML.Graphics;

namespace Engine.Systems.Rendering.Factories;

public sealed class PartyBattleRendererFactory
{
    private readonly AssetRegistry _assetRegistry;

    private readonly RenderSystem _renderSystem;

    public PartyBattleRendererFactory(AssetRegistry assetRegistry, RenderSystem renderSystem)
    {
        _assetRegistry = assetRegistry;
        _renderSystem = renderSystem;
    }

    public PartyBattleRenderer Create(Party party) 
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
                        character.GetBattleSpriteName())
                    )
                };
            }
            index ++;
        }

        return new PartyBattleRenderer(_renderSystem, partyRenderData);
    }
}