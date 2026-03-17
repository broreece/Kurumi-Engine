namespace Game.Map.Actors.Base;

/// <summary>
/// The actor sprite class, contains information about an actors sprite ID, width and height.
/// </summary>
public class ActorSprite {
    /// <summary>
    /// Constructor for the actor sprite object.
    /// </summary>
    /// <param name="spriteId"></param>
    /// <param name="width"></param>
    /// <param name="height"></param>
    public ActorSprite(int spriteId, int width, int height) {
        this.spriteId = spriteId;
        this.width = width;
        this.height = height;
    }

    /// <summary>
    /// Getter for the actor sprite's sprite ID.
    /// </summary>
    /// <returns>The sprite ID of the actor.</returns>
    public int GetSpriteId() {
        return spriteId;
    }

    /// <summary>
    /// Getter for the actor sprite's sprite width.
    /// </summary>
    /// <returns>The sprite width of the actor.</returns>
    public int GetWidth() {
        return width;
    }

    /// <summary>
    /// Getter for the actor sprite's sprite height.
    /// </summary>
    /// <returns>The sprite height of the actor.</returns>
    public int GetHeight() {
        return height;
    }

    private readonly int spriteId, width, height;
}