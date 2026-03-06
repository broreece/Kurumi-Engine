namespace Game.Entities.Skills;

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
    /// <param name="effect">The effect of the ability.</param>
    /// <param name="element">The abilities element.</param>
    /// <param name="cost">The cost of the ability.</param>
    /// <param name="mpCost">If the ability reduces the users MP or HP.</param>
    /// <param name="spriteId">The sprite id of the ability when used in battle.</param>
    public Ability(int id, string name, string description, string effect, int element, int cost,
        bool mpCost, int spriteId) : base(id, name) {
        this.description = description;
        this.effect = effect;
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
    /// Getter for the abilities effect.
    /// </summary>
    /// <returns>The effect of the ability.</returns>
    public string GetEffect() {
        return effect;
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

    private readonly string description, effect;
    private readonly int element, cost, spriteId;
    private readonly bool mpCost;
}
