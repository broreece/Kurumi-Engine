namespace Registry.Names;

/// <summary>
/// The equipment type name data registry, contains data about the equipment type names.
/// </summary>
public sealed class EquipmentTypeNameRegistry {
    /// <summary>
    /// Constructor for the equipment type name data registry.
    /// </summary>
    /// <param name="equipmentTypeNames">The equipment type names.</param>
    public EquipmentTypeNameRegistry(string[] equipmentTypeNames) {
        this.equipmentTypeNames = equipmentTypeNames;
    }

    /// <summary>
    /// Getter for a specific equipment type name.
    /// </summary>
    /// <param name="index">The index of the desired equipment type name.</param>
    /// <returns>The equipment type name</returns>
    public string GetEquipmentTypeName(int index) {
        return equipmentTypeNames[index];
    }

    private readonly string[] equipmentTypeNames;
}