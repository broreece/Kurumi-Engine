using Game.Scripts.Base;
using Game.Scripts.Context.Capabilities.Interfaces.Battle;
using Game.Scripts.Context.Core;

namespace Game.Scripts.Steps.Battle;

public sealed class UseAbility : ScriptStep 
{
    private readonly int _abilityIndex;

    public UseAbility(int abilityIndex) : base() 
    {
        _abilityIndex = abilityIndex;
    }

    public override void Activate(ScriptContext scriptContext) 
    {
        IActiveBattleActions activeBattleActions = scriptContext.GetCapability<IActiveBattleActions>();
        activeBattleActions.ActivateAbility(_abilityIndex);
    }
}
