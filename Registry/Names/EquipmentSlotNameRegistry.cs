namespace Registry.Names;

/// <summary>
/// The equipment slot name data registry, contains data about the equipment slot names.
/// </summary>
public sealed class EquipmentSlotNameRegistry {
    /// <summary>
    /// Constructor for the equipment slot name data registry.
    /// </summary>
    /// <param name="equipmentSlotNames">The equipment slot names array.</param>
    public EquipmentSlotNameRegistry(string[] equipmentSlotNames) {
        this.equipmentSlotNames = equipmentSlotNames;
    }

    /// <summary>
    /// Getter for the equipment slot name data.
    /// </summary>
    /// <returns>The 2D array of the game's equipment slot names.</returns>
    public string[] GetEquipmentSlotNames() {
        return equipmentSlotNames;
    }

    /// <summary>
    /// Getter for a specific equipment slot name.
    /// </summary>
    /// <param name="index">The index of the desired equipment slot name.</param>
    /// <returns>The equipment slot name at the specified index.</returns>
    public string GetEquipmentSlotName(int index) {
        return equipmentSlotNames[index];
    }

    private readonly string[] equipmentSlotNames;
}