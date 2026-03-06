namespace UI.Interfaces;

using Game.Entities.PlayableCharacter;
using UI.Core;

/// <summary>
/// The game UI interface, enables access to the party and adding new UI states to the UI state stack.
/// </summary>
public interface IGameUIContext {
    /// <summary>
    /// Function used to enqueue a new UI state.
    /// </summary>
    /// <param name="newUIState">The new state to be added to the end of the queue.</param>
    public void AddUIState(UIState newUIState);

    /// <summary>
    /// Function used to just pop the most recently pushed UI stack object.
    /// </summary>
    public void PopUIStack();

    /// <summary>
    /// Function add the main menu to the UI stack.
    /// </summary>
    public void OpenMainMenu();
    
    /// <summary>
    /// Getter for the party instance's party members.
    /// </summary>
    /// <returns>The array of party members.</returns>
    public PlayableCharacter[] GetPartyMembers();
}