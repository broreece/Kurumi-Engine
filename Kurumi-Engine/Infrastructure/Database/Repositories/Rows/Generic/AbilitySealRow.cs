namespace Infrastructure.Database.Repositories.Rows.Generic;

/// <summary>
/// Contains the values of a single row of a source, an ability reference and if the ability reference is sealed.
/// </summary>
public sealed class AbilitySealRow 
{
    public required int SourceId { get; init; }
    public required int AbilityRefId { get; init; }
    public required bool Sealed { get; init; }
}