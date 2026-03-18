namespace Scenes.Map.Interfaces;

/// <summary>
/// The public actor scene interface, used to restrict access to the actors methods used in the map scene.
/// </summary>
public interface IActorView {
    /// <summary>
    /// Sets the map element's current animation frame.
    /// </summary>
    /// <param name="newAnimationFrame">The new current animation frame of the map element.</param>
    public void SetCurrentAnimationFrame(int animationFrame);

    /// <summary>
    /// Getter for the field sprite id of the actor.
    /// </summary>
    /// <returns>The field sprite id of the actor.</returns>
    public int GetFieldSpriteId();

    /// <summary>
    /// Getter for the width of the actor.
    /// </summary>
    /// <returns>The field sprite width of the actor.</returns>
    public int GetWidth();

    /// <summary>
    /// Getter for the height of the actor.
    /// </summary>
    /// <returns>The field sprite height of the actor.</returns>
    public int GetHeight();

    /// <summary>
    /// Gets the map element's previous X location.
    /// </summary>
    /// <returns>The map elements previous X coordinate.</returns>
    public int GetOldXLocation();

    /// <summary>
    /// Gets the map element's X location.
    /// </summary>
    /// <returns>The map elements X coordinate.</returns>
    public int GetXLocation();

    /// <summary>
    /// Gets the map element's previous Y location.
    /// </summary>
    /// <returns>The map elements previous Y coordinate.</returns>
    public int GetOldYLocation();

    /// <summary>
    /// Gets the map element's Y location.
    /// </summary>
    /// <returns>The map elements Y coordinate.</returns>
    public int GetYLocation();

    /// <summary>
    /// Gets the map element's current animation frame.
    /// </summary>
    /// <returns>The map elements current animation frame.</returns>
    public int GetCurrentAnimationFrame();

    /// <summary>
    /// Getter for the map elements direction.
    /// </summary>
    /// <returns>The direction the map element is facing.</returns>
    public int GetDirection();

    /// <summary>
    /// Returns if the actor's field sprite appears below the party.
    /// </summary>
    /// <returns>True: The field sprite will be drawn on the map scene before the party.
    /// False: The field sprite is drawn after the party, will appear above.</returns>
    public bool IsBelowParty();
}