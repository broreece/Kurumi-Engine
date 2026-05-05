using Config.Core;
using Config.Runtime.Defaults;
using Config.Runtime.Windows;

using Engine.Assets.Core;
using Engine.State.Base;

using Game.Scripts.Context.Capabilities.Interfaces.Universal;
using Game.UI.Overlays.Core;

using Utils.Interfaces;

namespace Game.Scripts.Context.Capabilities.Implementations.Universal;

public sealed class UIActions : IUIActions 
{
    // State context.
    private readonly StateContext _stateContext;

    // Asset registry.
    private readonly AssetRegistry _assetRegistry;

    // Config.
    private readonly WindowConfig _windowConfig;

    // Defaults.
    private readonly ChoiceBoxDefaults _choiceBoxDefaults;
    private readonly GlobalMessageDefaults _globalMessageDefaults;
    private readonly NameBoxDefaults _nameBoxDefaults;
    private readonly TextWindowDefaults _textWindowDefaults;

    public UIActions(StateContext stateContext, AssetRegistry assetRegistry, ConfigProvider configProvider)
    {
        _stateContext = stateContext;
        _assetRegistry = assetRegistry;
        _windowConfig = configProvider.WindowConfig;
        _choiceBoxDefaults = configProvider.ChoiceBoxDefaults;
        _globalMessageDefaults = configProvider.GlobalMessageDefaults;
        _nameBoxDefaults = configProvider.NameBoxDefaults;
        _textWindowDefaults = configProvider.TextWindowDefaults;
    }

    public IFinishable OpenBasicTextWindow(IReadOnlyList<string> pages) 
    {
        var dialogueOverlay = new DialogueOverlay(_assetRegistry, _textWindowDefaults, pages);
        _stateContext.PushUIOverlay(dialogueOverlay);
        return dialogueOverlay;
    }

    public void OpenGlobalMessage(int timeLimit, string text) 
    {
        // TODO: Implement here.
    }

    public ChoiceBoxWithDialogueOverlay OpenTextWindowWithChoice(IReadOnlyList<string> choices, string text) 
    {
        var choiceBoxWithDialogueOverlay = new ChoiceBoxWithDialogueOverlay(
            _assetRegistry, 
            _textWindowDefaults, 
            _choiceBoxDefaults, 
            choices, 
            text
        );
        _stateContext.PushUIOverlay(choiceBoxWithDialogueOverlay);
        return choiceBoxWithDialogueOverlay;
    }

    public void OpenTextWindowWithNameBox(string text, string name) 
    {
        // TODO: Implement here.
    }
}