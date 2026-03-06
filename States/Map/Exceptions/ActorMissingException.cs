namespace States.Map.Exceptions;

/// <summary>
/// Custom exception class thrown if no actor is found.
/// </summary>
public class ActorMissingException : Exception {
    /// <summary>
    /// The constructor for the missing actor exception.
    /// </summary>
    public ActorMissingException() {}

    /// <summary>
    /// The constructor for the missing actor exception.
    /// </summary>
    /// <param name="message">A message that can be passed for debugging.</param>
    public ActorMissingException(string message)
        : base(message) {
    }
}