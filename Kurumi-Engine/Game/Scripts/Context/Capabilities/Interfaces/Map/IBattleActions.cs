using Game.Scripts.Context.Capabilities.Base;

namespace Game.Scripts.Context.Capabilities.Interfaces.Map;

public interface IBattleActions : ICapability 
{
    public void StartBattle(string backgroundMusicName, string battleBackgroundArtName, int enemyFormationId);
}