// Game.
using Game.Scripts.Base;
using Game.Scripts.Context.Capabilities.Interfaces.Universal;
using Game.Scripts.Context.Core;

// Utilities.
using Utils.Finishable;

namespace Game.Scripts.Steps.Universal;

public sealed class BasicTextWindowWithNameBox : ScriptStep {
    private readonly IReadOnlyList<string> _pages;
    private readonly string _name;

    private IFinishable? _textWindow;

    public BasicTextWindowWithNameBox(IReadOnlyList<string> pages, string name) : base() 
    {
        _pages = pages;
        _name = name;
    }

    public override void Activate(ScriptContext scriptContext) 
    {
        IUIActions uiActions = scriptContext.GetCapability<IUIActions>();
        _textWindow = uiActions.OpenTextWindowWithNameBox(_pages, _name);
    }

    public override bool Waiting() => _textWindow != null && !_textWindow.IsFinished();
}