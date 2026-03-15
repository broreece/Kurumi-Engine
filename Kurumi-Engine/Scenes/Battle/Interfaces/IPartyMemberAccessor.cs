namespace Scenes.Battle.Interfaces;

using Game.Entities.PlayableCharacter;

/// <summary>
/// The party member accessor interface, contains a method for accessing a specific party member.
/// </summary>
public interface IPartyMemberAccessor {
    /// <summary>
    /// Returns a playable character at a given character index.
    /// </summary>
    /// <param name="characterIndex">The index within the party to get the playable character from.</param>
    /// <returns>The playable character at the specified index.</returns>
    public PlayableCharacter GetPartyMember(int characterIndex);
}