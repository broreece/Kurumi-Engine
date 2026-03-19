namespace Engine.MapManager.Core;

using Game.Maps.Actors.Base;

/// <summary>
/// The stored actor class, used to temporarily store actor information for creating grided actors in the map manager.
/// </summary>
public sealed class StoredActor {
    /// <summary>
    /// The constructor for the stored actor class.
    /// </summary>
    /// <param name="xLocation">The x location of the actor.</param>
    /// <param name="yLocation">The y location of the actor.</param>
    /// <param name="actorInfo">The stored actor's information.</param>
    /// <param name="direction">The direction the actor is facing.</param>
    /// <param name="visible">If the actor is visible.</param>
    public StoredActor(int xLocation, int yLocation, ActorInfo actorInfo, int direction, bool visible) {
        this.xLocation = xLocation;
        this.yLocation = yLocation;
        this.actorInfo = actorInfo;
        this.direction = direction;
        this.visible = visible;
    }

    /// <summary>
    /// Getter for the stored actor's X location.
    /// </summary>
    /// <returns>The X location of the stored actor.</returns>
    public int GetXLocation() {
        return xLocation;
    }

    /// <summary>
    /// Getter for the stored actor's Y location.
    /// </summary>
    /// <returns>The Y location of the stored actor.</returns>
    public int GetYLocation() {
        return yLocation;
    }

    /// <summary>
    /// The stored actor's information.
    /// </summary>
    /// <returns>The stored actor's information.</returns>
    public ActorInfo GetActorInfo() {
        return actorInfo;
    }

    /// <summary>
    /// Getter for the stored actor's facing direction.
    /// </summary>
    /// <returns>The facing direction of the stored actor.</returns>
    public int GetDirection() {
        return direction;
    }

    /// <summary>
    /// Getter for if the stored actor is visible.
    /// </summary>
    /// <returns>If the stored actor is visible.</returns>
    public bool IsVisible() {
        return visible;
    }

    private readonly ActorInfo actorInfo;
    private readonly int xLocation, yLocation, direction;
    private readonly bool visible;
}