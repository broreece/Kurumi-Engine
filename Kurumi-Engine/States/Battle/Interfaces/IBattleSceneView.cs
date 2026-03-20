namespace States.Battle.Interfaces;

using Utils.Interfaces;

/// <summary>
/// The public battle scene view interface, used to control visibility between scene and state.
/// </summary>
public interface IBattleSceneView {
    /// <summary>
    /// Function that updates character sprites based on if the enemy/party member is knocked out or selected.
    /// </summary>
    /// <param name="enemyHpValues">The HP values of the enemy.</param>
    public void UpdateCharacterSprites(int[] enemyHpValues);

    /// <summary>
    /// Function used to update the information window to display new health, mp and statuses.
    /// </summary>
    /// <param name="ICharacterStatsAccessor">The array of character stats passed from the party.</param>
    public void UpdateInfoWindow(ICharacterStatsAccessor[] characters);

    /// <summary>
    /// Function used to update the damage display text based on if the damage is applied to a party member and the 
    /// sprite index.
    /// </summary>
    /// <param name="partyDamage">If the damage was applied to a party member.</param>
    /// <param name="spriteIndex">The index of the sprite within the correct sprite array.</param>
    /// <param name="hpChange">The HP change to be displayed.</param>
    public void UpdateDamageText(bool partyDamage, int spriteIndex, int hpChange);

    /// <summary>
    /// Function used to increment the selection window choice.
    /// </summary>
    public void IncrementSelectionWindowChoice();

    /// <summary>
    /// Function used to reduce the selection window choice.
    /// </summary>
    public void ReduceSelectionWindowChoice();

    /// <summary>
    /// Function that sets the current selection window choice in the battle scene.
    /// </summary>
    /// <param name="newCurrentChoice">The new current choice.</param>
    public void SetCurrentChoice(int newCurrentChoice);

    /// <summary>
    /// Function that returns the current choice on the selection window.
    /// </summary>
    /// <returns>The current choice of the battle scene.</returns>
    public int GetCurrentChoice();

    /// <summary>
    /// Function that returns the length of the choices array.
    /// </summary>
    /// <returns>The length of the choices array.</returns>
    public int GetChoicesLength();
}