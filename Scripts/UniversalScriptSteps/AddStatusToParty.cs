namespace Scripts.UniversalScriptSteps;

using Engine.Runtime;
using Engine.Systems;
using Scripts.Base;
using Game.Entities.PlayableCharacter;
using Game.Entities.Status;

/// <summary>
/// The add status to party script step.
/// </summary>
public sealed class AddStatusToParty : ScriptStep {
    /// <summary>
    /// Constructor for the add status to party script step.
    /// </summary>
    /// <param name="statusId">The id of the status that will be inflicted on the party.</param>
    public AddStatusToParty(int statusId) {
        this.statusId = statusId;
    }

    /// <summary>
    /// The activator function for the add status to party script step.
    /// </summary>
    /// <param name="scriptContext">The context of the script.</param>
    public override void Activate(ScriptContext scriptContext) {
        // TODO: (RGCA-01) Change function used here for getparty.getpartymembers, just use game context function.
        GameContext gameContext = scriptContext.GetGameContext();
        PlayableCharacter[] partyMembers = gameContext.GetParty().GetPartyMembers();
        StatusResolver statusResolver = gameContext.GetStatusResolver();
        // TODO: (RGCA-01) Same here use a game context function.
        Status status = gameContext.GetStatusRegistry().GetStatus(statusId);
        foreach (PlayableCharacter character in partyMembers) {
            statusResolver.ApplyStatus(character, status);
        }
    }
    
    /// <summary>
    /// Getter for the status id the script step will inflict on the party.
    /// </summary>
    /// <returns>The status id that the party will be inflicted with.</returns>
    public int GetStatusId() {
        return statusId;
    }

    private readonly int statusId;
}
