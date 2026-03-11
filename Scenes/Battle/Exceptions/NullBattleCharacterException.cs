namespace Scenes.Battle.Exceptions;

/// <summary>
/// Custom exception class thrown if a character is null when being requested in the battle scene.
/// </summary>
public sealed class NullBattleCharacterException : Exception {
    /// <summary>
    /// The constructor for the null battle character exception.
    /// </summary>
    public NullBattleCharacterException() {
    }

    /// <summary>
    /// The constructor for the null battle character exception.
    /// </summary>
    /// <param name="message">A message that can be passed for debugging.</param>
    public NullBattleCharacterException(string message)
        : base(message) {
    }
}