namespace Infrastructure.Database.Repositories.Rows.Items;

public sealed class ItemRow 
{
    public required int Id { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }
    public string? Script { get; init; }
    public required bool UsableInBattle { get; init; }
    public required bool UsableInMenu { get; init; }
    public required bool TargetsParty { get; init; }
    public required bool TargetsEnemy { get; init; }
    public required bool TargetsAll { get; init; }
    public required bool ConsumeOnUse { get; init; }
    public required string SpriteName { get; init; }
    public required int Price { get; init; }
    public required int Weight { get; init; }
}