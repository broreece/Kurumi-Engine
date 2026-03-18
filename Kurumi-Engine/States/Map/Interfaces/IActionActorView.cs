namespace States.Map.Interfaces;

using Game.Map.Actors.Base;
using Game.Map.Elements;
using Utils.Interfaces;

/// <summary>
/// The public action actor view interface. Used when selecting actors on the map state.
/// </summary>
public interface IActionActorView : IScriptAccessor, ICoordinateAccessor {
    /// <summary>
    /// Sets the map elements direction.
    /// </summary>
    /// <param name="newDirection">The new direction of the map element.</param>
    public void SetDirection(Direction newDirection);

    /// <summary>
    /// Returns if the actor can be activated on action.
    /// </summary>
    /// <returns>True: The actor activates when the party interacts with the actor.
    /// False: The actor does not activate when the party interacts with the actor.</returns>
    public bool ActivatesOnAction();

    /// <summary>
    /// Getter for the actor's behaviour.
    /// </summary>
    /// <returns>The behaviour of the actor.</returns>
    public Behaviour GetBehaviour();
}