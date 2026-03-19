namespace Engine.ScriptManager.Exceptions;

using Engine.Exceptions;

/// <summary>
/// The script step exception, occurs when loading or creating script steps.
/// </summary>
public sealed class ScriptStepException : EngineException {
    /// <summary>
    /// The constructor for the script step exception.
    /// </summary>
    /// <param name="message">The message to be logged.</param>
    public ScriptStepException(string message) : base(message, Severity.Error) {}
}