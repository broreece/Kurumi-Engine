namespace Registry.Items;

using Game.Items;

/// <summary>
/// The equipment data registry, contains data about the equipment.
/// </summary>
public sealed class EquipmentRegistry {
    /// <summary>
    /// Constructor for the equipment data registry.
    /// </summary>
    /// <param name="equipments">The equipment array.</param>
    public EquipmentRegistry(Equipment[] equipment) {
        this.equipment = equipment;
    }

    /// <summary>
    /// Getter for the equipment data.
    /// </summary>
    /// <returns>The array of the game's equipment.</returns>
    public Equipment[] GetEquipment() {
        return equipment;
    }

    /// <summary>
    /// Getter for a specific equipment.
    /// </summary>
    /// <param name="index">The index of the desired equipment.</param>
    /// <returns>A specified equipment object.</returns>
    public Equipment GetEquipment(int index) {
        return equipment[index];
    }

    private readonly Equipment[] equipment;
}