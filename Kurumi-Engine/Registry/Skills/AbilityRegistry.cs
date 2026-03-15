namespace Registry.Skills;

using Game.Entities.Skills;

/// <summary>
/// The ability data registry, contains data about the abilities.
/// </summary>
public sealed class AbilityRegistry {
    /// <summary>
    /// Constructor for the ability data registry.
    /// </summary>
    /// <param name="abilities">The abilities array.</param>
    public AbilityRegistry(Ability[] abilities) {
        this.abilities = abilities;
    }

    /// <summary>
    /// Getter for the abilities data.
    /// </summary>
    /// <returns>The array of the game's abilities.</returns>
    public Ability[] GetAbilities() {
        return abilities;
    }

    /// <summary>
    /// Getter for a specific ability in the abilities array.
    /// </summary>
    /// <param name="index">The index of the desired ability.</param>
    /// <returns>The specified ability.</returns>
    public Ability GetAbility(int index) {
        return abilities[index];
    }

    private readonly Ability[] abilities;
}