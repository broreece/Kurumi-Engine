namespace Game.Core;

/// <summary>
/// presentation data class. Contains forward facing name, description and a sprite id.
/// </summary>
public class PresentationData {
    /// <summary>
    /// Constructor for the presentation data.
    /// </summary>
    /// <param name="name">The name of the entity.</param>
    /// <param name="description">The description of the entity.</param>
    /// <param name="spriteId">The sprite id of the entity.</param>
    public PresentationData(string name, string description, int spriteId) {
        this.name = name;
        this.description = description;
        this.spriteId = spriteId;
    }

    /// <summary>
    /// Getter for the namable elements names.
    /// </summary>
    /// <returns>The name of the savable element.</returns>
    public string GetName() {
        return name;
    }

    /// <summary>
    /// Getter for the elements description.
    /// </summary>
    /// <returns>The description of the element.</returns>
    public string GetDescription() {
        return description;
    }

    
    /// <summary>
    /// Getter for the elements sprite id.
    /// </summary>
    /// <returns>The sprite id of the element.</returns>
    public int GetSpriteId() {
        return spriteId;
    }

    protected readonly string name, description;
    protected readonly int spriteId;
}