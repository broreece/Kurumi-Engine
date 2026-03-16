namespace Registry.Actors;

using Game.Map.Actors.Base;

/// <summary>
/// The actor data registry, contains data about the actors.
/// </summary>
public sealed class ActorRegistry {
    /// <summary>
    /// Constructor for the actor data registry.
    /// </summary>
    /// <param name="actors">The actors array.</param>
    public ActorRegistry(ActorInfo[] actors) {
        this.actors = actors;
    }

    /// <summary>
    /// Getter for a specific actor in the actor array.
    /// </summary>
    /// <param name="index">The index of the specific actor.</param>
    /// <returns>The specific actor.</returns>
    public ActorInfo GetActor(int index) {
        return actors[index];
    }

    private readonly ActorInfo[] actors;
}