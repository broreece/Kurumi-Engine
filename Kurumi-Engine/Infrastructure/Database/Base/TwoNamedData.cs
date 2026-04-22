namespace Infrastructure.Database.Base;

/// <summary>
/// Used to store two strings alongside an ID.
/// </summary>
public sealed class TwoNamedData 
{
    public required int Id { get; init; }
    public required string FirstName { get; init; }
    public required string SecondName { get; init; }
}