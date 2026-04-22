using Data.Definitions.Modifiers.Base;

namespace Data.Definitions.Items.Core;

public sealed class Equipment 
{
    public required int Id { get; init; }
    public required int ItemId { get; init; }
    public required int EquipmentSlotId { get; init; }
    public required int EquipmentTypeId { get; init; }

    public required IReadOnlyDictionary<ModifierType, IEntityModifier> EntityModifiers { get; init; }
}