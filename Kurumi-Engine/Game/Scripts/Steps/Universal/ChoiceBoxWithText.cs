using Game.Scripts.Base;
using Game.Scripts.Context.Capabilities.Interfaces.Universal;
using Game.Scripts.Context.Core;

namespace Game.Scripts.Steps.Universal;

public sealed class ChoiceBoxWithText : ConditionalScriptStep 
{
    private readonly string _text;
    private readonly string[] _choices;

    public ChoiceBoxWithText(string text, string[] choices, string? nextIfFalse) : base(nextIfFalse) 
    {
        _text = text;
        _choices = choices;
    }
    
    public override void Activate(ScriptContext scriptContext) 
    {
        IUIActions uiActions = scriptContext.GetCapability<IUIActions>();
        uiActions.OpenTextWindowWithChoice(_text, _choices);
    }
}