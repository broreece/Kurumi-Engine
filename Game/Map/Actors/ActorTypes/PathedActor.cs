namespace Game.Map.Actors.ActorTypes;

using Game.Map.ActorControllers;
using Game.Map.Actors.Base;

/// <summary>
/// Pathed actors are an object that holds an actor and a path.
/// </summary>
public sealed class PathedActor : ActorHandler {
    /// <summary>
    /// Constructor for the pathed actor class.
    /// </summary>
    /// <param name="actor">The existing actor object.</param>
    public PathedActor(Actor actor) : base(actor) {
        // Assign actor value and copy script.
        string scriptText = actor.GetScript();

        // Check if value is int, if so keep looping.
        List<int> path = [];
        while (scriptText.Contains(',') && int.TryParse(scriptText[..scriptText.IndexOf(',')], out int nextStep)) {
            path.Add(nextStep);
            scriptText = scriptText[(scriptText.IndexOf(',') + 1)..];
        }

        // Add base controller.
        actor.PushController(new PathedActorController(actor.GetMovementSpeed(), actor.GetXLocation(), actor.GetYLocation(), 
            forcedMovement: false, path));
    }
}