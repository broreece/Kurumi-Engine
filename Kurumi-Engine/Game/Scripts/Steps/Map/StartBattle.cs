using Data.Runtime.Formations.Base;
using Game.Scripts.Base;
using Game.Scripts.Context.Capabilities.Interfaces.Map;
using Game.Scripts.Context.Core;

namespace Game.Scripts.Steps.Map;

public sealed class StartBattle : ScriptStep 
{
    private readonly BattleStartRequest _battleStartRequest;

    public StartBattle(string backgroundMusicName, string battleBackgroundArtName, int enemyFormationId) : base() 
    {
        _battleStartRequest = new BattleStartRequest() 
        {
            BackgroundMusicName = backgroundMusicName, 
            BattleBackgroundArtName = battleBackgroundArtName, 
            EnemyFormationId = enemyFormationId
        };
    }

    public override void Activate(ScriptContext scriptContext) 
    {
        IBattleActions battleActions = scriptContext.GetCapability<IBattleActions>();
        battleActions.StartBattle(_battleStartRequest);
    }
}
