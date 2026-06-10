// Config.
using Config.Runtime.Defaults;

// Engine.
using Engine.Assets.Core;

// Game.
using Game.UI.Overlays.Core;

namespace Game.UI.Overlays.Factories;

public sealed class DialogueWithNameBoxOverlayFactory
{
    private readonly AssetRegistry _assetRegistry;

    private readonly NameBoxDefaults _nameBoxDefaults;
    private readonly TextWindowDefaults _textWindowDefaults;

    public DialogueWithNameBoxOverlayFactory(
        AssetRegistry assetRegistry, 
        NameBoxDefaults nameBoxDefaults, 
        TextWindowDefaults textWindowDefaults
    )
    {
        _assetRegistry = assetRegistry;
        _nameBoxDefaults = nameBoxDefaults;
        _textWindowDefaults = textWindowDefaults;
    }

    public DialogueWithNameBoxOverlay Create(IReadOnlyList<string> pages, string name)
    {
        return new DialogueWithNameBoxOverlay(_assetRegistry, _nameBoxDefaults, _textWindowDefaults, pages, name);
    }
}