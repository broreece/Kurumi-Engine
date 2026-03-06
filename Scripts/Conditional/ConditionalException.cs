namespace Scripts.Conditional;

/// <summary>
/// Custom exception class thrown when conditional logic fails.
/// </summary>
public sealed class ConditionalException : Exception {
    /// <summary>
    /// The constructor for the conditional exception.
    /// </summary>
    public ConditionalException() {}

    /// <summary>
    /// The constructor for the conditional exception.
    /// </summary>
    /// <param name="message">A message that can be passed for debugging.</param>
    public ConditionalException(string message)
        : base(message) {
    }
}