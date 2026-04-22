namespace Infrastructure.Database.Repositories.Rows.Abilities;

public sealed class AbilityRow 
{
    public required int Id { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }
    public string? ScriptName { get; init; }
    public required int ElementId { get; init; }
    public required int Cost { get; init; }
    public required bool UsesMp { get; init; }
    public required string BattleSpriteAnimationName { get; init; }
}