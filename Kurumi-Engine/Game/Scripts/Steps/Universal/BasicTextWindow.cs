using Game.Scripts.Base;
using Game.Scripts.Context.Capabilities.Interfaces.Universal;
using Game.Scripts.Context.Core;

namespace Game.Scripts.Steps.Universal;

public sealed class BasicTextWindow : ScriptStep 
{
    private readonly string _text;

    public BasicTextWindow(string text) : base() {
        _text = text;
    }

    public override void Activate(ScriptContext scriptContext) 
    {
        IUIActions uiActions = scriptContext.GetCapability<IUIActions>();
        uiActions.OpenBasicTextWindow(_text);
    }
}
