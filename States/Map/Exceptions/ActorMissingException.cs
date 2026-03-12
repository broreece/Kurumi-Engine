namespace States.Map.Exceptions;

using Engine.Exceptions;

/// <summary>
/// Custom exception class thrown if no actor is found.
/// </summary>
public class ActorMissingException : EngineException {
    /// <summary>
    /// The constructor for the missing actor exception.
    /// </summary>
    /// <param name="message">A message that can be passed for debugging.</param>
    public ActorMissingException(string message)
        : base(message, Severity.Warning) {
    }
}