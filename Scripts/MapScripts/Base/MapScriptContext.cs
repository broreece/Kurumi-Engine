namespace Scripts.MapScripts.Base;

using Engine.Runtime.Core;
using Scripts.Base;
using States.Map.Core;

/// <summary>
/// Map script context class, contains additional functions that utilize map scene and state.
/// </summary>
public sealed class MapScriptContext : SceneScriptContext {
    /// <summary>
    /// Constructor for the map script context class.
    /// </summary>
    /// <param name="gameContext">The game's context object.</param>
    /// <param name="mapScript">The map script that owns the context.</param>
    public MapScriptContext(GameContext gameContext, MapScript mapScript) : base(gameContext, mapScript) {}

    /// <summary>
    /// Function used to continue a script following from a previous script step.
    /// </summary>
    /// <param name="previousStep">The last executed scene step.</param>
    public override void ContinueScript(ScriptStep previousStep) {
        ScriptStep? nextStep = previousStep.GetNextStep();
        MapScript mapScript = (MapScript) script;
        if (nextStep != null) {
            mapScript.SetScriptStep(nextStep);
            mapScript.Activate(gameContext);
        }
        else {
            // Reset the map script current step if it's finished.
            mapScript.SetScriptStep(mapScript.GetHead());
        }
    }

    /// <summary>
    /// Force move party function that forces the party to move on the map scene.
    /// </summary>
    /// <param name="keepDirection">Determines if the party will maintain a direction.</param>
    /// <param name="path">The path that the party will take.</param>
    public void ForceMoveParty(bool keepDirection, List<int> path) {
        // TODO: (ASE-01) Add a new parameter in the function and in the script for the force move party script step.
        GetMapState()?.StartForceMoveParty(keepDirection, path, gameContext.GetCharacterMovementSpeed());
    }

    /// <summary>
    /// Force move script function that forces a specified actor to move on the map scene. 
    /// </summary>
    /// <param name="keepDirection">If the actor's direction is maintained.</param>
    /// <param name="lockMovement">If the parties movement is locked during the actor's movement.</param>
    /// <param name="scriptX">The actor's X location.</param>
    /// <param name="scriptY">The actor's Y location.</param>
    /// <param name="path">The path the actor follows.</param>
    /// <exception cref="MapScriptMissingException">Error thrown if a map script is missing.</exception>
    public void ForceMoveActor(bool keepDirection, bool lockMovement, bool instant, int scriptX, int scriptY,
        List<int> path) {
        GetMapState()?.ForceMoveActor(keepDirection, lockMovement, instant, scriptX, scriptY, path);
    }

    /// <summary>
    /// Function used to execute the load new battle method in game context.
    /// </summary>
    /// <param name="backgroundMusicId">The battle background music ID.</param>
    /// <param name="battleBackgroundArtId">The battle background art ID.</param>
    /// <param name="enemyFormationId">The enemy formation used in the battle.</param>
    public void StartBattle(int backgroundMusicId, int battleBackgroundArtId, int enemyFormationId) {
        gameContext.LoadNewBattle(backgroundMusicId, battleBackgroundArtId, enemyFormationId);
    }

    /// <summary>
    /// Private function used to load the map state from the game context.
    /// </summary>
    /// <returns>The state currently set as active.</returns>
    private MapState? GetMapState() {
        return (MapState?) gameContext.GetCurrentState();
    }
}