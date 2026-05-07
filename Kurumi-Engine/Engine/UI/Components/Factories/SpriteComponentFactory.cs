using Engine.Assets.Base;
using Engine.Assets.Core;
using Engine.UI.Components.Core;
using Engine.UI.Data.Style;

namespace Engine.UI.Components.Factories;

public sealed class SpriteComponentFactory
{
    private readonly AssetRegistry _assetRegistry;

    public SpriteComponentFactory(AssetRegistry assetRegistry)
    {
        _assetRegistry = assetRegistry;
    }

    public SpriteComponent Create(AssetType assetType, SpriteStyle spriteStyle)
    {
        return new SpriteComponent(_assetRegistry.GetTexture(assetType, spriteStyle.SpriteArt));
    }
}