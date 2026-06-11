// Config.
using Config.Runtime.Defaults;

// Engine.
using Engine.Assets.Core;

// Game.
using Game.UI.Overlays.Core;

namespace Game.UI.Overlays.Factories;

public sealed class DialogueOverlayFactory
{
    private readonly AssetRegistry _assetRegistry;
    private readonly TextWindowDefaults _textWindowDefaults;

    public DialogueOverlayFactory(AssetRegistry assetRegistry, TextWindowDefaults textWindowDefaults)
    {
        _assetRegistry = assetRegistry;
        _textWindowDefaults = textWindowDefaults;
    }

    public DialogueOverlay Create(IReadOnlyList<string> pages)
    {
        return new DialogueOverlay(_assetRegistry, _textWindowDefaults, pages);
    }
}