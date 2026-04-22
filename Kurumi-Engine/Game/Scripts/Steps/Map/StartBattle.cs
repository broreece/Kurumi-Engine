using Game.Scripts.Base;
using Game.Scripts.Context.Capabilities.Interfaces.Map;
using Game.Scripts.Context.Core;

namespace Game.Scripts.Steps.Map;

public sealed class StartBattle : ScriptStep 
{
    private readonly string _backgroundMusicName, _battleBackgroundArtName;
    private readonly int _enemyFormationId;

    public StartBattle(string backgroundMusicName, string battleBackgroundArtName, int enemyFormationId) : base() 
    {
        _backgroundMusicName = backgroundMusicName;
        _battleBackgroundArtName = battleBackgroundArtName;
        _enemyFormationId = enemyFormationId;
    }

    public override void Activate(ScriptContext scriptContext) 
    {
        IBattleActions battleActions = scriptContext.GetCapability<IBattleActions>();
        battleActions.StartBattle(_backgroundMusicName, _battleBackgroundArtName, _enemyFormationId);
    }
}
