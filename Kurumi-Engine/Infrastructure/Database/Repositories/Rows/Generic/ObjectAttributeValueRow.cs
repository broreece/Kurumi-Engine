namespace Infrastructure.Database.Repositories.Rows.Generic;

/// <summary>
/// Contains the values of a single row of entity attribute values from the database. 
/// This can be used for elements, statuses and stats as all store in the same style.
/// </summary>
public sealed class ObjectAttributeValueRow 
{
    public required int ObjectId { get; init; }
    public required int AttributeId { get; init; }
    public required int Value { get; init; }
}