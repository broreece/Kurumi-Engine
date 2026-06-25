namespace Engine.State.States.Battle.Base;

/// <summary>
/// Used to determine the target of attacks that do not have a fixed target. 
/// </summary>
public enum BattleTargets 
{
    RandomPartyMember = -1,
    RandomEnemy = -2,
    AllPartyMembers = -3,
    AllEnemies = -4,
    AllPartyAndAllEnemies = -5
}