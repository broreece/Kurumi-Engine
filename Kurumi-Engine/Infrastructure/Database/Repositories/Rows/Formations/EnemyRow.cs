namespace Infrastructure.Database.Repositories.Rows.Formations;

public sealed class EnemyRow 
{
    public required int Id { get; init; }
    public required int EnemyFormationId { get; init; }
    public required int EnemyId { get; init; }
    public required int XLocation { get; init; }
    public required int YLocation { get; init; }
    public required int MainPart { get; init; }
    public string? OnKillScript { get; init; }
}