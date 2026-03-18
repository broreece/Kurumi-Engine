namespace Registry.Actors;

using Game.Map.Actors.Base;

/// <summary>
/// The actor info registry, contains data about the actors.
/// </summary>
public sealed class ActorInfoRegistry {
    /// <summary>
    /// Constructor for the actor info registry.
    /// </summary>
    /// <param name="actorInfo">The actor information array.</param>
    public ActorInfoRegistry(ActorInfo[] actorInfo) {
        this.actorInfo = actorInfo;
    }

    /// <summary>
    /// Getter for a specific actor information in the array.
    /// </summary>
    /// <param name="index">The index of the specific actor information.</param>
    /// <returns>The specific actor information.</returns>
    public ActorInfo GetActorInfo(int index) {
        return actorInfo[index];
    }

    private readonly ActorInfo[] actorInfo;
}