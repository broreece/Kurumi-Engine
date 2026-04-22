using Game.Scripts.Base;
using Game.Scripts.Context.Capabilities.Interfaces.Universal;
using Game.Scripts.Context.Core;

namespace Game.Scripts.Steps.Universal;

public sealed class ShowWindowWithTextAndNamebox : ScriptStep {
    private readonly string _text, _name;

    public ShowWindowWithTextAndNamebox(string text, string name) : base() 
    {
        _text = text;
        _name = name;
    }

    public override void Activate(ScriptContext scriptContext) 
    {
        IUIActions uiActions = scriptContext.GetCapability<IUIActions>();
        uiActions.OpenTextWindowWithNameBox(_text, _name);
    }
}