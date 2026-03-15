namespace Engine.Exceptions;

/// <summary>
/// Engine exception abstract class.
/// </summary>
public abstract class EngineException : Exception {
    /// <summary>
    /// Constructor for the engine exception class.
    /// </summary>
    /// <param name="baseMessage">The base message of the exception, not including scenes.</param>
    /// <param name="severity">The severity rating of the exception.</param>
    public EngineException(string baseMessage, Severity severity) : base(baseMessage) {
        this.severity = severity;
    }

    /// <summary>
    /// Getter for the severity of the engine exception.
    /// </summary>
    /// <returns></returns>
    public Severity GetSeverity() {
        return severity;
    }

    private readonly Severity severity;
}