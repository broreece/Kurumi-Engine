namespace Data.Models.Formations.Core;

public sealed class EnemyModel 
{
    public required int Id { get; set; }
    public required List<int> Statuses { get; set; } = [];
}