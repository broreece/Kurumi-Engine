namespace Data.Definitions.Formations.Core;

public sealed class EnemyDefinition 
{
    public required int Id { get; init; }
    public required int EnemyId { get; init; }
    public required int XLocation { get; init; }
    public required int YLocation { get; init; }
    public required int MainPart { get; init; }

    public required string? OnKillScript { get; init; }

    public required IReadOnlyList<int> BattleScripts { get; init; }
}