namespace States.Map.Exceptions;

using Engine.Exceptions;

/// <summary>
/// Custom exception class thrown if the force move script or script step is not found.
/// </summary>
public class ForceMoveScriptNullException : EngineException {
    /// <summary>
    /// The constructor for the force move script null exception.
    /// </summary>
    /// <param name="message">A message that can be passed for debugging.</param>
    public ForceMoveScriptNullException(string message)
        : base(message, Severity.Error) {
    }
}