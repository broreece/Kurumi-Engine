namespace Scripts.MapScripts.MapScriptSteps;

using Scripts.Base;
using Scripts.MapScripts.Base;

/// <summary>
/// The start battle scene map script step.
/// </summary>
public sealed class StartBattle : ScriptStep {
    /// <summary>
    /// Constructor for the start battle scene script.
    /// </summary>
    /// <param name="backgroundMusicId">The battle background music ID.</param>
    /// <param name="battleBackgroundArtId">The battle background art ID.</param>
    /// <param name="enemyFormationId">The enemy formation used in the battle.</param>
    public StartBattle(int backgroundMusicId, int battleBackgroundArtId, int enemyFormationId) {
        this.backgroundMusicId = backgroundMusicId;
        this.battleBackgroundArtId = battleBackgroundArtId;
        this.enemyFormationId = enemyFormationId;
    }

    /// <summary>
    /// The activation function for the start battle script.
    /// </summary>
    /// <param name="scriptContext">The context of the script.</param>
    public override void Activate(ScriptContext scriptContext) {
        MapScriptContext mapScriptContext = (MapScriptContext) scriptContext;
        mapScriptContext.StartBattle(backgroundMusicId, battleBackgroundArtId, enemyFormationId);
    }

    /// <summary>
    /// Getter for the battles background music ID.
    /// </summary>
    /// <returns>The battle music ID.</returns>
    public int GetBackgroundMusicId() {
        return backgroundMusicId;
    }

    /// <summary>
    /// Getter for the battles background art ID.
    /// </summary>
    /// <returns>The battle background art ID.</returns>
    public int GetBattleBackgroundArtId() {
        return battleBackgroundArtId;
    }

    /// <summary>
    /// Getter for the enemy formation ID.
    /// </summary>
    /// <returns>The enemy formations ID.</returns>
    public int GetEnemyFormationId() {
        return enemyFormationId;
    }

    private readonly int backgroundMusicId, battleBackgroundArtId, enemyFormationId;
}
