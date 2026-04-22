namespace Infrastructure.Exceptions.Base;

/// <summary>
/// A type of exception that is thrown exclusively within the engine. Contains a custom severity level.
/// </summary>
public abstract class EngineException : Exception 
{
    public EngineException(string baseMessage) : base(baseMessage) {}

    /// <summary>
    /// Engine exceptions must contain a severity level.
    /// </summary>
    /// <value>The severity level of the exception.</value>
    public abstract ExceptionSeverity Severity { get; }
}