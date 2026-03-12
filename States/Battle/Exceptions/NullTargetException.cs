namespace States.Battle.Exceptions;

using Engine.Exceptions;

/// <summary>
/// Custom exception class thrown if a target is null in the battle scene.
/// </summary>
public sealed class NullTargetException : EngineException {
    /// <summary>
    /// The constructor for the null target exception.
    /// </summary>
    /// <param name="message">A message that can be passed for debugging.</param>
    public NullTargetException(string message)
        : base(message, Severity.Fatal) {
    }
}