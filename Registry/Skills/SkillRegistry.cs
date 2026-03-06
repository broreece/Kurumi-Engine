namespace Registry.Skills;

using Game.Entities.Skills;

/// <summary>
/// The skill data registry, contains data about the skills.
/// </summary>
public sealed class SkillRegistry {
    /// <summary>
    /// Constructor for the skill data registry.
    /// </summary>
    /// <param name="skills">The skills array.</param>
    public SkillRegistry(Skill[] skills) {
        this.skills = skills;
    }

    /// <summary>
    /// Getter for the skills data.
    /// </summary>
    /// <returns>The array of the game's skills.</returns>
    public Skill[] GetSkills() {
        return skills;
    }

    /// <summary>
    /// Getter for a specified skill in the skills array.
    /// </summary>
    /// <param name="index">The index of the desired skill in the array.</param>
    /// <returns>The specified skill from the array.</returns>
    public Skill GetSkill(int index) {
        return skills[index];
    }

    private readonly Skill[] skills;
}