namespace Game.Entities.Skills;

/// <summary>
/// Abstract capability definition class, inherited by only skills and abilities.
/// </summary>
public abstract class CapabilityDefinition {
    /// <summary>
    /// Constructor for the capability definition abstract class.
    /// </summary>
    /// <param name="id">The id of the element's base kept in the database.</param>
    /// <param name="name">The name of the element.</param>
    protected CapabilityDefinition(int id, string name) {
        this.id = id;
        this.name = name;
    }

    /// <summary>
    /// Getter for the elements id.
    /// </summary>
    /// <returns>The id of the element.</returns>
    public int GetId() {
        return id;
    }

    /// <summary>
    /// Getter for the elements name.
    /// </summary>
    /// <returns>The name of the element.</returns>
    public string GetName() {
        return name;
    }

    private readonly int id;
    private readonly string name;
}