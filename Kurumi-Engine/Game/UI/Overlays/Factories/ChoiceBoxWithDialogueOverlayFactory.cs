// Config.
using Config.Runtime.Defaults;

// Engine.
using Engine.Assets.Core;

// Game.
using Game.UI.Overlays.Core;

namespace Game.UI.Overlays.Factories;

public sealed class ChoiceBoxWithDialogueOverlayFactory
{
    private readonly AssetRegistry _assetRegistry;

    private readonly TextWindowDefaults _textWindowDefaults;
    private readonly ChoiceBoxDefaults _choiceBoxDefaults;

    public ChoiceBoxWithDialogueOverlayFactory(
        AssetRegistry assetRegistry, 
        TextWindowDefaults textWindowDefaults, 
        ChoiceBoxDefaults choiceBoxDefaults
    )
    {
        _assetRegistry = assetRegistry;
        _textWindowDefaults = textWindowDefaults;
        _choiceBoxDefaults = choiceBoxDefaults;
    }

    public ChoiceBoxWithDialogueOverlay Create(IReadOnlyList<string> choices, string text)
    {
        return new ChoiceBoxWithDialogueOverlay(
            _assetRegistry, 
            _textWindowDefaults, 
            _choiceBoxDefaults, 
            choices, 
            text
        );
    }
}