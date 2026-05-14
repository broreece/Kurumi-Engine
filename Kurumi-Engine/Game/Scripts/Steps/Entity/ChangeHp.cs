using Data.Runtime.Entities.Base;
using Game.Scripts.Base;
using Game.Scripts.Context.Capabilities.Interfaces.Entity;
using Game.Scripts.Context.Core;
using Game.Scripts.Context.Variables.Base;

namespace Game.Scripts.Steps.Entity;

public sealed class ChangeHp : ScriptStep 
{
    private readonly bool _reduceHp, _canKo;
    private readonly string _formula;

    public ChangeHp(bool reduceHp, bool canKo, string formula) : base() 
    {
        _reduceHp = reduceHp;
        _canKo = canKo;
        _formula = formula;
    }

    public override void Activate(ScriptContext scriptContext) 
    {
        IHpMpActions battleActions = scriptContext.GetCapability<IHpMpActions>();
        EntityIndex user = scriptContext.GetVariable(ScriptVariables.User);
        EntityIndex target = scriptContext.GetVariable(ScriptVariables.Target);
        battleActions.ApplyHealthChange(user, target, _reduceHp, _canKo, _formula);
    }
}
