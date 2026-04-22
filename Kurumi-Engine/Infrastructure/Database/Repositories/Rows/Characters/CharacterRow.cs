namespace Infrastructure.Database.Repositories.Rows.Characters;

public sealed class CharacterRow 
{
    public required int Id { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required string BattleSprite { get; init; }
    public required string FieldSprite { get; init; }
    public required string MenuSprite { get; init; }
}