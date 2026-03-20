namespace Game.Entities.Skills;

using Scripts.EntityScripts.Base;

/// <summary>
/// The ability class.
/// </summary>
public class Ability : CapabilityDefinition {
    /// <summary>
    /// Ability class constructor.
    /// </summary>
    /// <param name="id">The name of the saveable element.</param>
    /// <param name="name">The name of the saveable element.</param>
    /// <param name="description">The abilities description.</param>
    /// <param name="script">The script of the ability.</param>
    /// <param name="element">The abilities element.</param>
    /// <param name="cost">The cost of the ability.</param>
    /// <param name="mpCost">If the ability reduces the users MP or HP.</param>
    /// <param name="spriteId">The sprite id of the ability when used in battle.</param>
    public Ability(int id, string name, string description, EntityScript script, int element, int cost,
        bool mpCost, int spriteId) : base(id, name) {
        this.description = description;
        this.script = script;
        this.element = element;
        this.cost = cost;
        this.mpCost = mpCost;
        this.spriteId = spriteId;
    }

    /// <summary>
    /// Getter for the abilities description.
    /// </summary>
    /// <returns>The description of the ability.</returns>
    public string GetDescription() {
        return description;
    }

    /// <summary>
    /// Getter for the abilities element.
    /// </summary>
    /// <returns>The effect of the element.</returns>
    public int GetElement() {
        return element;
    }

    /// <summary>
    /// Getter for the abilities cost.
    /// </summary>
    /// <returns>The cost of the ability.</returns>
    public int GetCost() {
        return cost;
    }

    /// <summary>
    /// Getter for the abilities sprite id.
    /// </summary>
    /// <returns>The sprite ID of the ability.</returns>
    public int GetSpriteId() {
        return spriteId;
    }

    /// <summary>
    /// Getter for if the ability costs MP (Else HP).
    /// </summary>
    /// <returns>If the ability costs MP or HP.</returns>
    public bool IsMpCost() {
        return mpCost;
    }

    /// <summary>
    /// Getter for the abilities linked entity script.
    /// </summary>
    /// <returns>The entity script linked to the ability.</returns>
    public EntityScript GetScript() {
        return script;
    }

    private readonly string description;
    private readonly int element, cost, spriteId;
    private readonly bool mpCost;
    private readonly EntityScript script;
}
