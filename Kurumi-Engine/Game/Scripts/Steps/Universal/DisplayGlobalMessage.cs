using Game.Scripts.Base;
using Game.Scripts.Context.Capabilities.Interfaces.Universal;
using Game.Scripts.Context.Core;

namespace Game.Scripts.Steps.Universal;

public sealed class DisplayGlobalMessage : ScriptStep 
{
    private readonly int _timeLimit;
    private readonly string _text;

    public DisplayGlobalMessage(int timeLimit, string text) : base() 
    {
        _timeLimit = timeLimit;
        _text = text;
    }
    public override void Activate(ScriptContext scriptContext) 
    {
        IUIActions uiActions = scriptContext.GetCapability<IUIActions>();
        uiActions.OpenGlobalMessage(_timeLimit, _text);
    }
}
