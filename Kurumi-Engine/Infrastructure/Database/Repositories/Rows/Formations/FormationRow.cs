namespace Infrastructure.Database.Repositories.Rows.Formations;

public sealed class FormationRow 
{
    public required int Id { get; init; }
    public required string MapName { get; init; }
    public required int ReturnX { get; init; }
    public required int ReturnY { get; init; }
    public required int SearchTimer { get; init; }
    public required int ItemPoolId { get; init; }
    public required int OnFoundActorInfoId { get; init; }
    public required int DefaultActorInfoId { get; init; }
    public string? OnLoseScript { get; init; }
    public string? OnWinScript { get; init; }
}