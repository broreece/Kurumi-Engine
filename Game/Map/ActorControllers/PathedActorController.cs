namespace Game.Map.ActorControllers;

using Scripts.Base;
using States.Map.Interfaces;

/// <summary>
/// The pathed actor controller class, handles pathed actors.
/// </summary>
public sealed class PathedActorController : ActorController {
    /// <summary>
    /// The constructor for pathed actor controllers.
    /// </summary>
    /// <param name="interval">The interval at which the actor moves.</param>
    /// <param name="xLocation">The stored x location of the actor.</param>
    /// <param name="yLocation">The stored y location of the actor.</param>
    /// <param name="forcedMovement">If this controller is a forced movement.</param>
    /// <param name="actorPath">The list of movements the actor takes.</param>
    /// TODO: (FMAS-02) Change this architect.
    /// <param name="forceMoveScript">The possible continuable script the pathed actor has to execute when the path is finished.</param>
    /// <param name="currentForceMoveStep">The possible step of the script that resulted in this pathed movement.</param>
    public PathedActorController(int interval, int xLocation, int yLocation, bool forcedMovement, List<int> actorPath,
        IContinuableScript? forceMoveScript = null, ScriptStep? currentForceMoveStep = null) 
        : base(interval, xLocation, yLocation) {
        followPathIndex = 0;
        finished = false;
        this.forcedMovement = forcedMovement;
        this.actorPath = actorPath;
        // TODO: (FMAS-02) Change this architect.
        this.forceMoveScript = forceMoveScript;
        this.currentForceMoveStep = currentForceMoveStep;
    }

    /// <summary>
    /// The overriden execute move function that also increments the path index.
    /// </summary>
    /// <param name="newX">The new x location of the actor.</param>
    /// <param name="newY">The new y location of the actor.</param>
    public override void ExecuteMove(int newX, int newY) {
        // Reset path if finished.
        if (followPathIndex >= actorPath.Count - 1) {
            finished = forcedMovement;
            // TODO: (FMAS-02) Change this architect.
            if (finished) {
                if (currentForceMoveStep == null) {
                    throw new Exception();
                }
                forceMoveScript?.ContinueScript(currentForceMoveStep);
            }
            followPathIndex = 0;
        } else {
            followPathIndex ++;
        }
        base.ExecuteMove(newX, newY);
    }

    /// <summary>
    /// The overriden get move function used in pathed actors.
    /// </summary>
    /// <returns>The next move in the path.</returns>
    public override int GetMove() {
        // Get next move.
        int nextStep = actorPath[followPathIndex];

        duration = 0;
        return nextStep;
    }

    /// <summary>
    /// Function that returns if the actor controller has finished it's movements and should be popped from the actor controller stack.
    /// </summary>
    /// <returns>True if the controller has finished it's action, false otherwise.</returns>
    public override bool IsFinished() {
        return finished;
    }

    private int followPathIndex;
    private readonly bool forcedMovement;
    private bool finished;
    private readonly List<int> actorPath;

    // TODO: (FMAS-02) Change this architect.
    // Stored variables if this is a forced movement from a script.
    private IContinuableScript? forceMoveScript;
    private ScriptStep? currentForceMoveStep;
}