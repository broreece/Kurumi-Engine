namespace Engine.Runtime.Exceptions;

using Engine.Exceptions;

/// <summary>
/// Custom exception class thrown if the engine tries to pop an empty UI stack.
/// </summary>
public sealed class PopEmptyUIStackException : EngineException {
    /// <summary>
    /// The constructor for the pop empty UI stack exception.
    /// </summary>
    /// <param name="message">A message that can be passed for debugging.</param>
    public PopEmptyUIStackException(string message)
        : base(message, Severity.Warning) {
    }
}