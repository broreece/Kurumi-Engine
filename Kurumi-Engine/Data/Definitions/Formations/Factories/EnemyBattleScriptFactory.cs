using Data.Definitions.Formations.Core;

namespace Data.Definitions.Formations.Factories;

public sealed class EnemyBattleScriptFactory 
{
    public EnemyBattleScript Create(int id, int target, int startTurn, int frequency, string scriptName) 
    {
        return new EnemyBattleScript()
        {
            Id = id, 
            Target = target, 
            StartTurn = startTurn, 
            Frequency = frequency, 
            ScriptName = scriptName
        };
    }
}