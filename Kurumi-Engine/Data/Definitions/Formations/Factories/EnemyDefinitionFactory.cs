using Data.Definitions.Formations.Core;

namespace Data.Definitions.Formations.Factories;

public sealed class EnemyDefinitionFactory 
{
    public EnemyDefinition Create(
        int id, 
        int enemyId, 
        int xLocation, 
        int yLocation, 
        int mainPart, 
        string? onKillScript, 
        IReadOnlyList<int> battleScripts) 
    {
        return new EnemyDefinition()
        {
            Id = id, 
            EnemyId = enemyId, 
            XLocation = xLocation, 
            YLocation = yLocation, 
            MainPart = mainPart, 
            OnKillScript = onKillScript, 
            BattleScripts = battleScripts
        };
    }
}