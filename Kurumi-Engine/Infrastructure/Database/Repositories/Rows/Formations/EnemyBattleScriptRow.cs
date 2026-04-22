namespace Infrastructure.Database.Repositories.Rows.Formations;

public sealed class EnemyBattleScriptRow 
{
    public required int Id { get; init; }
    public required int EnemyFormationEnemyId { get; init; }
    public required string ScriptName { get; init; }
    public required int Target { get; init; }
    public required int StartTurn { get; init; }
    public required int Frequency { get; init; }
}