// Data.
using Data.Runtime.Formations.Core;

namespace Data.Runtime.Formations.Base;

/// <summary>
/// Used to store information regarding a battle start request. Stored in game objects.
/// </summary>
public sealed class BattleStartRequest 
{
    // Formation property is used for formation encounters, enemy formation ID property is used for scripted battles.
    public int EnemyFormationId { get; init; }

    public Formation? Formation { get; init; }
}