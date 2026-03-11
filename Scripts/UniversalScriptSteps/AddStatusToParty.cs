namespace Scripts.UniversalScriptSteps;

using Engine.Runtime.Core;
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
        PlayableCharacter[] partyMembers = scriptContext.GetPartyMembers();
        StatusResolver statusResolver = scriptContext.GetStatusResolver();
        Status status = scriptContext.GetStatus(statusId);
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
