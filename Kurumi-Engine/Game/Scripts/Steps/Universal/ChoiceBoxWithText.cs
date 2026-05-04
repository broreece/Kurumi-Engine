using Game.Scripts.Base;
using Game.Scripts.Context.Capabilities.Interfaces.Universal;
using Game.Scripts.Context.Core;
using Game.UI.Overlays.Core;

namespace Game.Scripts.Steps.Universal;

public sealed class ChoiceBoxWithText : ConditionalScriptStep 
{
    private readonly IReadOnlyList<string> _choices;
    private readonly string _text;

    private ChoiceBoxWithDialogueOverlay? _choiceBox;

    public ChoiceBoxWithText(IReadOnlyList<string> choices, string text, string? nextIfFalse) : base(nextIfFalse) 
    {
        _choices = choices;
        _text = text;
    }
    
    public override void Activate(ScriptContext scriptContext) 
    {
        IUIActions uiActions = scriptContext.GetCapability<IUIActions>();
        _choiceBox = uiActions.OpenTextWindowWithChoice(_choices, _text);
    }

    public override bool Waiting() => _choiceBox != null && !_choiceBox.IsFinished();

    protected override bool IsConditionMet() => _choiceBox!.YesSelected;
}