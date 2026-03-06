namespace Game.Entities.Base;

using Game.Entities.Skills;

/// <summary>
/// The entity element class. Elements that directly affect an entity's stats.
/// AKA Equipment, Status.
/// </summary>
public abstract class EntityElement {
    /// <summary>
    /// Constructor for the abstract entity element object. 
    /// </summary>
    /// <param name="turnEffectSpriteId">The turn effect sprite of the status.</param>
    /// <param name="accuracyModifier">The accuracy modifier for the entity element.</param>
    /// <param name="evasionModifier">The evasion modifier for the entity element.</param>
    /// <param name="stats">The stats for the entity element.</param>
    /// <param name="elements">The elements for the entity element.</param>
    /// <param name="turnScript">The turn script for the entity element.</param>
    /// <param name="sealedSkills">The list of sealed skills of the entity element.</param>
    /// <param name="sealedAbilities">The list of sealed abilities of the entity element.</param>
    /// <param name="addedAbilities">The list of added abilities of the entity element.</param>
    protected EntityElement(int turnEffectSpriteId, int accuracyModifier, int evasionModifier, int[] stats, 
        int[] elements, string turnScript, List<Skill> sealedSkills, List<Ability> sealedAbilities, List<Ability> addedAbilities) {
        this.turnEffectSpriteId = turnEffectSpriteId;
        this.accuracyModifier = accuracyModifier;
        this.evasionModifier = evasionModifier;
        this.stats = stats;
        this.elements = elements;
        this.turnScript = turnScript;
        this.sealedSkills = sealedSkills;
        this.sealedAbilities = sealedAbilities;
        this.addedAbilities = addedAbilities;
    }

    /// <summary>
    /// Getter for the elements current turn effect sprite.
    /// </summary>
    /// <returns>The turn effect sprite of the status.</returns>
    public int GetTurnEffectSpriteId() {
        return turnEffectSpriteId;
    }


    /// <summary>
    /// Getter for the entity element's accuracy modifier.
    /// </summary>
    /// <returns>The accuracy modifier of the element.</returns>
    public int GetAccuracyModifier() {
        return accuracyModifier;
    }

    /// <summary>
    /// Getter for the entity element's evasion modifier.
    /// </summary>
    /// <returns>The evasion modifier of the element.</returns>
     public int GetEvasionModifier() {
        return evasionModifier;
    }

    /// <summary>
    /// Getter for the entity element's stats.
    /// </summary>
    /// <returns>The stats of the element.</returns>
    public int[] GetStats() {
        return stats;
    }

    /// <summary>
    /// Getter for the entity element's elements.
    /// </summary>
    /// <returns>The elements of the element.</returns>
    public int[] GetElements() {
        return elements;
    }

    /// <summary>
    /// Getter for the entity element's turn effect.
    /// </summary>
    /// <returns>The turn effect of the element.</returns>
    public string GetTurnScript() {
        return turnScript;
    }

    /// <summary>
    /// Getter for the entity element's list of sealed skills.
    /// </summary>
    /// <returns>The sealed skills of the element.</returns>
    public List<Skill> GetSealedSkills() {
        return sealedSkills;
    }

    /// <summary>
    /// Getter for the entity element's list of sealed abilities.
    /// </summary>
    /// <returns>The sealed abilities of the element.</returns>
    public List<Ability> GetSealedAbilities() {
        return sealedAbilities;
    }

    /// <summary>
    /// Getter for the entity element's list of added abilities.
    /// </summary>
    /// <returns>The added abilities of the element.</returns>
    public List<Ability> GetAddedAbilities() {
        return addedAbilities;
    }

    private readonly int turnEffectSpriteId, accuracyModifier, evasionModifier;
    private readonly int[] stats, elements;
    private readonly string turnScript;
    private readonly List<Skill> sealedSkills;
    private readonly List<Ability> sealedAbilities, addedAbilities;
}