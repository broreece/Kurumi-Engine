namespace Scripts.MapScripts.MapScriptSteps;

using Scripts.Base;
using Scripts.MapScripts.Base;

/// <summary>
/// The force move actor map script step.
/// </summary>
public sealed class ForceMoveScript : ScriptStep {
    /// <summary>
    /// Constructor for the force move actor script step.
    /// </summary>
    /// <param name="keepDirection">Determines if the actor will maintain a direction.</param>
    /// <param name="lockMovement">Determines if the parties movement is locked whilst actor moves.</param>
    /// <param name="instant">Determines if the actor will move instantly.</param>
    /// <param name="actorX">The x location of the actor to move.</param>
    /// <param name="actorY">The y location of the actor to move.</param>
    /// <param name="path">The path that the actor will take.</param>
    public ForceMoveScript(bool keepDirection, bool lockMovement, bool instant, int actorX, int actorY, List<int> path) {
        this.keepDirection = keepDirection;
        this.lockMovement = lockMovement;
        this.instant = instant;
        this.actorX = actorX;
        this.actorY = actorY;
        this.path = path;
    }
    
    /// <summary>
    /// Activates the force move script script step by calling the force move function in the map scene.
    /// </summary>
    /// <param name="scriptContext">The context of the script.</param>
    public override void Activate(ScriptContext scriptContext) {
        MapScriptContext mapScriptContext = (MapScriptContext) scriptContext;
        mapScriptContext.ForceMoveActor(keepDirection, lockMovement, instant, actorX, actorY, path);
        // TODO: (FMA-01) We should pause the script untill after the forced movement finishes.
    }
    
    /// <summary>
    /// Getter for if the force move actor script will maintain the actor's direction during movement.
    /// </summary>
    /// <returns>True: Actor's direction will not change. False: Actor will change direction.</returns>
    public bool WillKeepDirection() {
        return keepDirection;
    }
    
    /// <summary>
    /// Getter for if the force move actor script will lock the party from moving.
    /// </summary>
    /// <returns>True: Script will lock the party from moving.</returns>
    public bool WillLockMovement() {
        return lockMovement;
    }
    
    /// <summary>
    /// Getter for if the force move actor script will occur instantly.
    /// </summary>
    /// <returns>True: Move will occur instantly.</returns>
    public bool IsInstant() {
        return instant;
    }
    
    /// <summary>
    /// Getter for the actor that will be moved; X position.
    /// </summary>
    /// <returns>The X location of the actor that will move.</returns>
    public int GetScriptX() {
        return actorX;
    }
    
    /// <summary>
    /// Getter for the actor that will be moved; Y position.
    /// </summary>
    /// <returns>The Y location of the actor that will move.</returns>
    public int GetActorY() {
        return actorY;
    }
    
    /// <summary>
    /// Getter for the force move script path.
    /// </summary>
    /// <returns>The path the script will move.</returns>
    public List<int> GetPath() {
        return path;
    }

    private readonly bool keepDirection, lockMovement, instant;
    private readonly int actorX, actorY;
    private readonly List<int> path;
}