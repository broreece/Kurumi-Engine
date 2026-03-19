namespace Game.Maps.Actors.Exceptions;

using Engine.Exceptions;

/// <summary>
/// The null script exception. Occurs if the engine attempts to access a script from an actor without a script.
/// </summary>
public class NullScriptException : EngineException {
    /// <summary>
    /// The constructor for the null script exception.
    /// </summary>
    /// <param name="message">The message to be logged.</param>
    public NullScriptException(string message) : base(message, Severity.Error) {}
}