using Game.Scripts.Base;
using Game.Scripts.Context.Capabilities.Interfaces.Universal;
using Game.Scripts.Context.Core;

namespace Game.Scripts.Steps.Universal;

public sealed class ChoiceBoxWithText : ConditionalScriptStep 
{
    private readonly IReadOnlyList<string> _choices;
    private readonly string _text;

    public ChoiceBoxWithText(IReadOnlyList<string> choices, string text, string? nextIfFalse) : base(nextIfFalse) 
    {
        _choices = choices;
        _text = text;
    }
    
    public override void Activate(ScriptContext scriptContext) 
    {
        IUIActions uiActions = scriptContext.GetCapability<IUIActions>();
        uiActions.OpenTextWindowWithChoice(_choices, _text);
    }
}