using Data.Runtime.Formations.Base;

using Game.Scripts.Base;
using Game.Scripts.Context.Capabilities.Interfaces.Map;
using Game.Scripts.Context.Core;

namespace Game.Scripts.Steps.Map;

public sealed class StartBattle : ScriptStep 
{
    private readonly int _enemyFormationId;

    public StartBattle(int enemyFormationId) : base() 
    {
        _enemyFormationId = enemyFormationId;
    }

    public override void Activate(ScriptContext scriptContext) 
    {
        var battleStartRequest = new BattleStartRequest() 
        {
            EnemyFormationId = _enemyFormationId
        };
        IBattleActions battleActions = scriptContext.GetCapability<IBattleActions>();
        battleActions.StartBattle(battleStartRequest);
    }
}
