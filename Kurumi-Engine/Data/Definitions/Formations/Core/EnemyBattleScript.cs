namespace Data.Definitions.Formations.Core;

public sealed class EnemyBattleScript 
{
    public required int Id { get; init; }
    public required int Target { get; init; }
    public required int StartTurn { get; init; }
    public required int Frequency { get; init; }

    public required string ScriptName { get; init; }
}