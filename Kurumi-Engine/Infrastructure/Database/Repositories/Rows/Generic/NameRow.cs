namespace Infrastructure.Database.Repositories.Rows.Generic;

/// <summary>
/// Contains the values of a single row of names from the database.
/// </summary>
public sealed class NameRow 
{
    public required int Id { get; init; }
    public required string Name { get; init; }
}