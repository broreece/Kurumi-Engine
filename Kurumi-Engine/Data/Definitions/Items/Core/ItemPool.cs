namespace Data.Definitions.Items.Core;

public sealed class ItemPool 
{
    public required int Id { get; init; }
    public required IReadOnlyList<int> ItemIds { get; init; }
}