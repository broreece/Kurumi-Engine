namespace Scenes.Battle.Exceptions;

using Engine.Exceptions;

/// <summary>
/// Custom exception class thrown if a character is null when being requested in the battle scene.
/// </summary>
public sealed class NullBattleCharacterException : EngineException {
    /// <summary>
    /// The constructor for the null battle character exception.
    /// </summary>
    /// <param name="message">A message that can be passed for debugging.</param>
    public NullBattleCharacterException(string message)
        : base(message, Severity.Fatal) {
    }
}