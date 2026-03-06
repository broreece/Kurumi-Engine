namespace Utils.Exceptions;

/// <summary>
/// Custom exception class thrown if no party data is found.
/// </summary>
public sealed class MissingPartyDataException : Exception {
    /// <summary>
    /// The constructor for the missing party data exception.
    /// </summary>
    public MissingPartyDataException() {
    }

    /// <summary>
    /// The constructor for the missing party data  exception.
    /// </summary>
    /// <param name="message">A message that can be passed for debugging.</param>
    public MissingPartyDataException(string message)
        : base(message) {
    }
}