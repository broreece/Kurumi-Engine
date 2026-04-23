using Game.Scripts.Core;

namespace Engine.State.States.Battle.Base;

/// <summary>
/// Used to determine the user, target, speed and script of battle actions. 
/// </summary>
public class BattleAction {
    public required int UserIndex { get; init; }
    public required int TargetIndex { get; init; }
    public required int Speed { get; init; }

    public required bool IsEnemy { get; init; }

    public required Script Script { get; init; }
}