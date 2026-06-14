// Game.
using Game.Scripts.Base;
using Game.Scripts.Context.Capabilities.Interfaces.Universal;
using Game.Scripts.Context.Core;

// Utility.
using Utils.Finishable;

namespace Game.Scripts.Steps.Universal;

public sealed class AddItemFromPool : ScriptStep 
{
    private readonly int _poolId;

    private IFinishable? _textWindow;

    public AddItemFromPool(int poolId) : base() 
    {
        _poolId = poolId;
    }

    public override void Activate(ScriptContext scriptContext) 
    {
        IItemActions itemActions = scriptContext.GetCapability<IItemActions>();
        IUIActions uiActions = scriptContext.GetCapability<IUIActions>();
        var itemName = itemActions.AddItemFromPool(_poolId).ToLower();

        var pages = new List<string>
        {
            $"Found a {itemName}"
        };

        _textWindow = uiActions.OpenBasicTextWindow(pages);
    }

    public override bool Waiting() => _textWindow != null && !_textWindow.IsFinished();
}
