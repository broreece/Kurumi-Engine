using Game.Scripts.Base;
using Game.Scripts.Context.Capabilities.Interfaces.Universal;
using Game.Scripts.Context.Core;

namespace Game.Scripts.Steps.Universal;

public sealed class BasicTextWindow : ScriptStep 
{
    private readonly IReadOnlyList<string> _pages;

    public BasicTextWindow(IReadOnlyList<string> pages) : base() {
        _pages = pages;
    }

    public override void Activate(ScriptContext scriptContext) 
    {
        IUIActions uiActions = scriptContext.GetCapability<IUIActions>();
        uiActions.OpenBasicTextWindow(_pages);
    }
}
