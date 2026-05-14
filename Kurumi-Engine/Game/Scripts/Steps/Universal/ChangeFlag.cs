using Game.Scripts.Base;
using Game.Scripts.Context.Capabilities.Interfaces.Universal;
using Game.Scripts.Context.Core;

namespace Game.Scripts.Steps.Universal;

public sealed class ChangeFlag : ScriptStep 
{
    private readonly string _flagKey;
    private readonly bool _newValue;

    public ChangeFlag(string flagKey, bool newValue) : base() 
    {
        _flagKey = flagKey;
        _newValue = newValue;
    }

    public override void Activate(ScriptContext scriptContext) 
    {
        IGameStateActions gameStateActions = scriptContext.GetCapability<IGameStateActions>();
        gameStateActions.ChangeFlag(_flagKey, _newValue);
    }
}