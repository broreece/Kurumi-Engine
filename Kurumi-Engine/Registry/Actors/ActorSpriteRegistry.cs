namespace Registry.Actors;

using Game.Map.Actors.Base;

/// <summary>
/// The actor sprites registry, contains data about the actor sprites.
/// </summary>
public sealed class ActorSpriteRegistry {
    /// <summary>
    /// Constructor for the actor sprites registry.
    /// </summary>
    /// <param name="actorSprites">The actor sprites array.</param>
    public ActorSpriteRegistry(ActorSprite[] actorSprites) {
        this.actorSprites = actorSprites;
    }

    /// <summary>
    /// Getter for a specific actor sprite in the actor sprite array.
    /// </summary>
    /// <param name="index">The index of the specific actor sprite.</param>
    /// <returns>The specific actor sprite.</returns>
    public ActorSprite GetActorSprite(int index) {
        return actorSprites[index];
    }

    private readonly ActorSprite[] actorSprites;
}