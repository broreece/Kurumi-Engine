namespace Infrastructure.Database.Repositories.Rows.Equipment;

public sealed class EquipmentRow 
{
    public required int Id { get; init; }
    public required int ItemId { get; init; }
    public required int EquipmentType { get; init; }
    public required int EquipmentSlot { get; init; }
    public required int Accuracy { get; init; }
    public required int Evasion { get; init; }
    public string? TurnEffectScript { get; init; }
}