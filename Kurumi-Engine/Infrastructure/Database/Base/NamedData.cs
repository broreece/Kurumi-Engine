namespace Infrastructure.Database.Base;

/// <summary>
/// Used to store a string alongside an ID.
/// </summary>
public sealed class NamedData 
{
    public required int Id { get; init; }
    public required string Name { get; init; }
}