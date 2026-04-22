using Game.Scripts.Base;
using Game.Scripts.Context.Capabilities.Interfaces.Universal;
using Game.Scripts.Context.Core;

namespace Game.Scripts.Steps.Universal;

public sealed class ChangeFlag : ScriptStep 
{
    private readonly int _flagIndex;
    private readonly bool _newValue;

    public ChangeFlag(int flagIndex, bool newValue) : base() 
    {
        _flagIndex = flagIndex;
        _newValue = newValue;
    }

    public override void Activate(ScriptContext scriptContext) 
    {
        IGameStateActions gameStateActions = scriptContext.GetCapability<IGameStateActions>();
        gameStateActions.ChangeFlag(_flagIndex, _newValue);
    }
}