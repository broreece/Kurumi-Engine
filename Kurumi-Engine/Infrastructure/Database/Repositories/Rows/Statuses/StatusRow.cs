namespace Infrastructure.Database.Repositories.Rows.Statuses;

public sealed class StatusRow 
{
    public required int Id { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required string SpriteName { get; init; }
    public required int Priority { get; init; }
    public required int Accuracy { get; init; }
    public required int Evasion { get; init; }
    public required int TurnLength { get; init; }
    public required bool CureAtBattleEnd { get; init; }
    public required bool CanAct { get; init; }
    public string? TurnEffectScript { get; init; }
}