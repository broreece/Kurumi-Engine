namespace Database.Exceptions;

using Engine.Exceptions;

/// <summary>
/// Custom exception class thrown if a database table is not found.
/// </summary>
public sealed class MissingDatabaseTableException : EngineException {
    /// <summary>
    /// The constructor for the missing database table exception.
    /// </summary>
    /// <param name="message">A message that can be passed for debugging.</param>
    public MissingDatabaseTableException(string message)
        : base(message, Severity.Fatal) {
    }
}