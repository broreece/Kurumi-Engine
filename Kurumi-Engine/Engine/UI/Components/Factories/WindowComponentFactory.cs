using Engine.Assets.Base;
using Engine.Assets.Core;
using Engine.UI.Components.Core;
using Engine.UI.Data.Style;

namespace Engine.UI.Components.Factories;

public sealed class WindowComponentFactory
{
    private readonly AssetRegistry _assetRegistry;

    public WindowComponentFactory(AssetRegistry assetRegistry)
    {
        _assetRegistry = assetRegistry;
    }

    public WindowComponent Create(WindowStyle windowStyle)
    {
        return new WindowComponent(_assetRegistry.GetTexture(AssetType.Windows, windowStyle.WindowArt));
    }
}