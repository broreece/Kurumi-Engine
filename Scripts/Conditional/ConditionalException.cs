namespace Scripts.Conditional;

using Engine.Exceptions;

/// <summary>
/// Custom exception class thrown when conditional logic fails.
/// </summary>
public sealed class ConditionalException : EngineException {
    /// <summary>
    /// The constructor for the conditional exception.
    /// </summary>
    /// <param name="message">A message that can be passed for debugging.</param>
    public ConditionalException(string message)
        : base(message, Severity.Error) {
    }
}