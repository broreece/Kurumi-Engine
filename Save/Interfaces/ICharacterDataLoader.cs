namespace Save.Interfaces;

using Game.Entities.PlayableCharacter;

/// <summary>
/// Interface used to be able to load playable character static data.
/// </summary>
public interface ICharacterDataLoader {
    /// <summary>
    /// Function that loads all playable characters in the database, does not load stats, equipment etc, that is to be adjusted
    /// in save files.
    /// </summary>
    /// <returns>The base of playable characters stored in the database.</returns>
    public PlayableCharacter[] LoadPlayableCharacters();
}