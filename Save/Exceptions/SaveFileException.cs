namespace Save.Exceptions;

using Engine.Exceptions;

/// <summary>
/// Custom exception class thrown if any exception is when a save file is being loaded.
/// </summary>
public sealed class SaveFileException : EngineException {
    /// <summary>
    /// The constructor for the save file exception.
    /// </summary>
    /// <param name="message">A message that can be passed for debugging.</param>
    public SaveFileException(string message)
        : base(message, Severity.Error) {
    }
}