namespace Utils.Exceptions;

using Engine.Exceptions;

/// <summary>
/// Custom exception class thrown if a input map is not found.
/// </summary>
public sealed class InputMapNotSetException : EngineException {
    /// <summary>
    /// The constructor for the input map not set exception.
    /// </summary>
    /// <param name="message">A message that can be passed for debugging.</param>
    public InputMapNotSetException(string message)
        : base(message, Severity.Fatal) {
    }
}