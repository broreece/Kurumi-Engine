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
    private readonly DialogueWithNameBoxOverlayFactory _dialogueWithNameBoxOverlayFactory;
    private readonly GlobalMessageFactory _globalMessageFactory;

    // State context.
    private readonly StateContext _stateContext;

    public UIActionsFactory(
        StateContext stateContext, 
        ChoiceBoxWithDialogueOverlayFactory choiceBoxWithDialogueOverlayFactory, 
        DialogueOverlayFactory dialogueOverlayFactory, 
        DialogueWithNameBoxOverlayFactory dialogueWithNameBoxOverlayFactory, 
        GlobalMessageFactory globalMessageFactory
    )
    {
        _choiceBoxWithDialogueOverlayFactory = choiceBoxWithDialogueOverlayFactory;
        _dialogueOverlayFactory = dialogueOverlayFactory;
        _dialogueWithNameBoxOverlayFactory = dialogueWithNameBoxOverlayFactory;
        _globalMessageFactory = globalMessageFactory;

        _stateContext = stateContext;
    }

    public UIActions Create()
    {
        return new UIActions(
            _choiceBoxWithDialogueOverlayFactory, 
            _dialogueOverlayFactory, 
            _dialogueWithNameBoxOverlayFactory, 
            _globalMessageFactory, 
            _stateContext
        );
    }
}