namespace Scenes.Battle.Interfaces;

using Game.Entities.PlayableCharacter;

/// <summary>
/// The party accessor interface, contains a function that accesses the entire party.
/// </summary>
public interface IPartyAccessor : IPartyMemberAccessor {
    /// <summary>
    /// Returns an array of playable characters.
    /// </summary>
    /// <returns>The array of playable characters.</returns>
    public PlayableCharacter[] GetPartyMembers();

    /// <summary>
    /// Returns an array of the party members battle sprites.
    /// </summary>
    /// <returns>The array of party member battle sprites.</returns>
    public int[] GetPartyBattleSprites();
}