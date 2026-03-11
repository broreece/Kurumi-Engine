namespace Scripts.Base;

using Engine.Runtime;
using Engine.Systems;
using Game.Entities.PlayableCharacter;
using Game.Entities.Status;
using Game.Party;

/// <summary>
/// Script context class, contains parameters to be used in the activator function for each script sub class.
/// </summary>
public class ScriptContext {
    /// <summary>
    /// Constructor for the script context class.
    /// </summary>
    /// <param name="gameContext">The game's context object.</param>
    public ScriptContext(GameContext gameContext) {
        this.gameContext = gameContext;
    }

    /// <summary>
    /// Function used to load a new map scene / state and map object.
    /// </summary>
    public void LoadNewMap() {
        gameContext.LoadNewMap();
    }

    /// <summary>
    /// Function that sets a game flag within the game variables object.
    /// </summary>
    /// <param name="flagIndex">The flag index.</param>
    /// <param name="newValue">The new value of the flag.</param>
    public void SetGameFlag(int flagIndex, bool newValue) {
        gameContext.SetGameFlag(flagIndex, newValue);
    }

    /// <summary>
    /// Function that returns a flag value within the game variables object.
    /// </summary>
    /// <param name="flagIndex">The flag index.</param>
    /// <returns>The specified flags state.</returns>
    public bool GetGameFlag(int flagIndex) {
        return gameContext.GetGameFlag(flagIndex);
    }

    /// <summary>
    /// Getter for the party instance.
    /// </summary>
    /// <returns>The party.</returns>
    public Party GetParty() {
        return gameContext.GetParty();
    }

    /// <summary>
    /// Getter for the party instance's party members.
    /// </summary>
    /// <returns>The array of party members.</returns>
    public PlayableCharacter[] GetPartyMembers() {
        return gameContext.GetPartyMembers();
    }

    /// <summary>
    /// Getter for the status resolver system.
    /// </summary>
    /// <returns>The status resolver system.</returns>
    public StatusResolver GetStatusResolver() {
        return gameContext.GetStatusResolver();
    }

    /// <summary>
    /// Function that returns a specific status from the status registry.
    /// </summary>
    /// <param name="statusId">The ID of the desired status.</param>
    /// <returns>The status object.</returns>
    public Status GetStatus(int statusId) {
        return gameContext.GetStatus(statusId);
    }
    
    protected readonly GameContext gameContext;
}