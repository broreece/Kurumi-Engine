using Data.Runtime.Formations.Base;

using Game.Scripts.Base;
using Game.Scripts.Context.Capabilities.Interfaces.Map;
using Game.Scripts.Context.Core;

namespace Game.Scripts.Steps.Map;

public sealed class StartBattle : ScriptStep 
{
    private readonly string _backgroundMusicName;
    private readonly string _backgroundArtName;

    private readonly int _enemyFormationId;

    public StartBattle(string backgroundMusicName, string backgroundArtName, int enemyFormationId) : base() 
    {
        _backgroundMusicName = backgroundMusicName;
        _backgroundArtName = backgroundArtName;
        _enemyFormationId = enemyFormationId;
    }

    public override void Activate(ScriptContext scriptContext) 
    {
        var battleStartRequest = new BattleStartRequest() 
        {
            BackgroundMusicName = _backgroundMusicName, 
            BattleBackgroundArtName = _backgroundArtName, 
            
            EnemyFormationId = _enemyFormationId
        };
        IBattleActions battleActions = scriptContext.GetCapability<IBattleActions>();
        battleActions.StartBattle(battleStartRequest);
    }
}
