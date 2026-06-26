// Engine.
using Engine.State.Base;

// Game.
using Game.Scripts.Context.Capabilities.Interfaces.Universal;

using Game.UI.Overlays.Core;
using Game.UI.Overlays.Factories;

// Utility.
using Utils.Finishable;

namespace Game.Scripts.Context.Capabilities.Implementations.Universal.Core;

public sealed class UIActions : IUIActions 
{
    // UI overlay factories.
    private readonly ChoiceBoxWithDialogueOverlayFactory _choiceBoxWithDialogueOverlayFactory;
    private readonly DialogueOverlayFactory _dialogueOverlayFactory;
    private readonly DialogueWithNameBoxOverlayFactory _dialogueWithNameBoxOverlayFactory;
    private readonly GlobalMessageFactory _globalMessageFactory;

    // State context.
    private readonly StateContext _stateContext;

    public UIActions(
        ChoiceBoxWithDialogueOverlayFactory choiceBoxWithDialogueOverlayFactory, 
        DialogueOverlayFactory dialogueOverlayFactory, 
        DialogueWithNameBoxOverlayFactory dialogueWithNameBoxOverlayFactory, 
        GlobalMessageFactory globalMessageFactory, 
        StateContext stateContext
    )
    {
        _choiceBoxWithDialogueOverlayFactory = choiceBoxWithDialogueOverlayFactory;
        _dialogueOverlayFactory = dialogueOverlayFactory;
        _dialogueWithNameBoxOverlayFactory = dialogueWithNameBoxOverlayFactory;
        _globalMessageFactory = globalMessageFactory;

        _stateContext = stateContext;
    }

    public IFinishable OpenBasicTextWindow(IReadOnlyList<string> pages) 
    {
        var dialogueOverlay = _dialogueOverlayFactory.Create(pages);
        _stateContext.PushUIOverlay(dialogueOverlay);
        return dialogueOverlay;
    }

    public IFinishable OpenTextWindowWithNameBox(IReadOnlyList<string> pages, string name) 
    {
        var dialogueOverlay = _dialogueWithNameBoxOverlayFactory.Create(pages, name);
        _stateContext.PushUIOverlay(dialogueOverlay);
        return dialogueOverlay;
    }

    public void OpenGlobalMessage(float timeLimit, string text) 
    {
        var globalMessage = _globalMessageFactory.Create(timeLimit, text);
        _stateContext.PushUIOverlay(globalMessage);
    }

    public ChoiceBoxWithDialogueOverlay OpenTextWindowWithChoice(IReadOnlyList<string> choices, string text) 
    {
        var choiceBoxWithDialogueOverlay = _choiceBoxWithDialogueOverlayFactory.Create(
            choices, 
            text
        );
        _stateContext.PushUIOverlay(choiceBoxWithDialogueOverlay);
        return choiceBoxWithDialogueOverlay;
    }
}