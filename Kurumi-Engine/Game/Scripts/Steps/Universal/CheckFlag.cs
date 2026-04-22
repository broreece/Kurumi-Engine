using Game.Scripts.Base;
using Game.Scripts.Context.Capabilities.Interfaces.Universal;
using Game.Scripts.Context.Core;

namespace Game.Scripts.Steps.Universal;

public sealed class CheckFlag : ConditionalScriptStep 
{
    private readonly int _flagIndex;
    private readonly bool _value;

    public CheckFlag(int flagIndex, bool value, string? nextIfFalse) : base(nextIfFalse) 
    {
        _flagIndex = flagIndex;
        _value = value;
    }
    
    public override void Activate(ScriptContext scriptContext) 
    {
        IGameStateActions gameStateActions = scriptContext.GetCapability<IGameStateActions>();
        SetConditionMet(gameStateActions.GetGameFlag(_flagIndex) == _value);
    }

}