namespace Scripts.MapScripts.MapScriptSteps;

using Scripts.Base;
using Scripts.MapScripts.Base;

/// <summary>
/// The force move party map scene script step.
/// </summary>
public sealed class ForceMoveParty : ScriptStep {
    /// <summary>
    /// Constructor for the force move party script step.
    /// </summary>
    /// <param name="keepDirection">Determines if the party will maintain a direction.</param>
    /// <param name="path">The path that the party will take.</param>
    public ForceMoveParty(bool keepDirection, List<int> path) {
        this.keepDirection = keepDirection;
        this.path = path;
    }

    /// <summary>
    /// Activates the force move party script step by calling the force move function in the map scene.
    /// </summary>
    /// <param name="scriptContext">The context of the script.</param>
    public override void Activate(ScriptContext scriptContext) {
        MapScriptContext mapScriptContext = (MapScriptContext) scriptContext;
        mapScriptContext.ForceMoveParty(keepDirection, path);
    }
    
    /// <summary>
    /// Getter for if the force move party script will maintain the parties direction during movement.
    /// </summary>
    /// <returns>True: Parties direction will not change. False: Party will change direction.</returns>
    public bool WillKeepDirection() {
        return keepDirection;
    }
    
    /// <summary>
    /// Getter for the force move party path.
    /// </summary>
    /// <returns>The path the party will move.</returns>
    public List<int> GetPath() {
        return path;
    }

    private readonly bool keepDirection;
    private readonly List<int> path;
}