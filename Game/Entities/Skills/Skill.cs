namespace Game.Entities.Skills;

/// <summary>
/// The skill class. Contains a list of abilities.
/// </summary>
public class Skill : CapabilityDefinition {
    /// <summary>
    /// Constructor for the skill class.
    /// </summary>
    /// <param name="name">The name of the skill.</param>
    public Skill(int id, string name) : base(id, name) {
        abilities = [];
    }

    /// <summary>
    /// Function that adds an ability to the skill.
    /// </summary>
    /// <param name="ability">The new ability to be added to the skill.</param>
    public void AddAbility(Ability ability) {
        abilities.Add(ability);
    }

    /// <summary>
    /// Getter for the skills abilities.
    /// </summary>
    /// <returns>The abilities the skill has.</returns>
    public List<Ability> GetAbilities() {
        return abilities;
    }

    private readonly List<Ability> abilities;
}