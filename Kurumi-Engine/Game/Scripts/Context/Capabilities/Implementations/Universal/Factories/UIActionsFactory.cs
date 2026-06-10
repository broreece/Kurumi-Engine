// Engine.
using Engine.State.Base;

// Game.
using Game.UI.Overlays.Factories;

namespace Game.Scripts.Context.Capabilities.Implementations.Universal.Core;

public sealed class UIActionsFactory 
{
    // UI overlay factories.
    private readonly ChoiceBoxWithDialogueOverlayFactory _choiceBoxWithDialogueOverlayFactory;
    private readonly DialogueOverlayFactory _dialogueOverlayFactory;
    private readonly GlobalMessageFactory _globalMessageFactory;

    // State context.
    private readonly StateContext _stateContext;

    public UIActionsFactory(
        StateContext stateContext, 
        ChoiceBoxWithDialogueOverlayFactory choiceBoxWithDialogueOverlayFactory, 
        DialogueOverlayFactory dialogueOverlayFactory, 
        GlobalMessageFactory globalMessageFactory
    )
    {
        _choiceBoxWithDialogueOverlayFactory = choiceBoxWithDialogueOverlayFactory;
        _dialogueOverlayFactory = dialogueOverlayFactory;
        _globalMessageFactory = globalMessageFactory;

        _stateContext = stateContext;
    }

    public UIActions Create()
    {
        return new UIActions(
            _choiceBoxWithDialogueOverlayFactory, 
            _dialogueOverlayFactory, 
            _globalMessageFactory, 
            _stateContext
        );
    }
}