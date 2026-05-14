using Config.Runtime.Map;
using Data.Definitions.Entities.Core;
using Data.Runtime.Party.Core;
using Engine.Assets.Base;
using Engine.Assets.Core;
using Engine.Systems.Rendering.Core;
using Infrastructure.Database.Base;
using Infrastructure.Rendering.Core;

namespace Engine.Systems.Rendering.Factories;

public sealed class PartyMapRendererFactory
{
    private readonly AssetRegistry _assetRegistry;

    private readonly RenderSystem _renderSystem;

    private readonly Registry<CharacterDefinition> _characterRegistry;
    private readonly CharacterFieldSpriteConfig _characterFieldSpriteConfig;

    private readonly int _tileWidth;
    private readonly int _tileHeight;

    public PartyMapRendererFactory(
        AssetRegistry assetRegistry,
        RenderSystem renderSystem,
        Registry<CharacterDefinition> characterRegistry,
        CharacterFieldSpriteConfig characterFieldSpriteConfig,
        int tileWidth,
        int tileHeight)
    {
        _assetRegistry = assetRegistry;
        _renderSystem = renderSystem;
        _characterRegistry = characterRegistry;
        _characterFieldSpriteConfig = characterFieldSpriteConfig;
        _tileWidth = tileWidth;
        _tileHeight = tileHeight;
    }

    public PartyMapRenderer Create(Party party)
    {
        string partySpriteFilePath = _characterRegistry.Get(party.PartyModel.PartyMembers[0]).FieldSprite;
        var partyTexture = _assetRegistry.GetTexture(AssetType.CharacterFieldSpriteSheets, partySpriteFilePath);

        return new PartyMapRenderer(
            _renderSystem,
            _characterFieldSpriteConfig,
            party,
            partyTexture,
            _tileWidth,
            _tileHeight);
    }
}