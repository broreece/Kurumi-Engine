using Game.Scripts.Base;
using Game.Scripts.Context.Capabilities.Interfaces.Universal;
using Game.Scripts.Context.Core;

using Utils.Finishable;

namespace Game.Scripts.Steps.Universal;

public sealed class BasicTextWindow : ScriptStep 
{
    private readonly IReadOnlyList<string> _pages;

    private IFinishable? _textWindow;

    public BasicTextWindow(IReadOnlyList<string> pages) : base() {
        _pages = pages;
    }

    public override void Activate(ScriptContext scriptContext) 
    {
        IUIActions uiActions = scriptContext.GetCapability<IUIActions>();
        _textWindow = uiActions.OpenBasicTextWindow(_pages);
    }

    public override bool Waiting() => _textWindow != null && !_textWindow.IsFinished();
}
