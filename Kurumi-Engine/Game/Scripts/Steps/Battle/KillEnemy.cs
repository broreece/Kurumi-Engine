using Game.Scripts.Base;
using Game.Scripts.Context.Capabilities.Interfaces.Battle;
using Game.Scripts.Context.Core;

namespace Game.Scripts.Steps.Battle;

public sealed class KillEnemy : ScriptStep 
{
    private readonly int _enemyId;

    public KillEnemy(int enemyId) : base() 
    {
        _enemyId = enemyId;
    }

    public override void Activate(ScriptContext scriptContext) 
    {
        IActiveBattleActions activeBattleActions = scriptContext.GetCapability<IActiveBattleActions>();
        activeBattleActions.KillEnemy(_enemyId);
    }
}