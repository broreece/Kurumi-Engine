namespace Infrastructure.Database.Repositories.Rows.Entities;

public sealed class StatRow 
{
    public required int Id { get; init; }
    public required string Name { get; init; }
    public required string ShortName { get; init; }
}