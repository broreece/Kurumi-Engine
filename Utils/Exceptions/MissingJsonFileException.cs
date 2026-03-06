namespace Utils.Exceptions;

/// <summary>
/// Custom exception class thrown if no json file is found.
/// </summary>
public sealed class MissingJsonFileException : Exception {
    /// <summary>
    /// The constructor for the missing json file exception.
    /// </summary>
    public MissingJsonFileException() {
    }

    /// <summary>
    /// The constructor for the missing json file exception.
    /// </summary>
    /// <param name="message">A message that can be passed for debugging.</param>
    public MissingJsonFileException(string message)
        : base(message) {
    }
}